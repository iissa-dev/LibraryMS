import type { ProblemDetails, Result } from "../types/index";
import axios, {
  AxiosError,
  type AxiosResponse,
  type InternalAxiosRequestConfig,
} from "axios";
import { getAccessToken, handleGlobalLogout } from "./auth";
import { refreshAccessToken } from "./refreshClient";

export const API_BASE_URL = import.meta.env.VITE_API_URL;
const baseURL = `${API_BASE_URL}/api`;

const apiClient = axios.create({
  baseURL,
  withCredentials: true,
});

// ==========================================
// 1. Request Interceptor
// ==========================================
apiClient.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = getAccessToken();
  if (token) config.headers.Authorization = `Bearer ${token}`;

  return config;
});

// ==========================================
// 2. Response Interceptor
// ==========================================
apiClient.interceptors.response.use(
  (response: AxiosResponse) => {
    const body = response.data;
    // Handle global API response using the Result Pattern
    if (body && typeof body === "object" && "isSuccess" in body) {
      if (body.isSuccess) return body.data; // Return raw un-wrapped data on success

      // Forward application failures to the error block below
      return Promise.reject({
        title: "Application Error",
        detail: body.error ?? "Operation failed",
        status: 400,
      });
    }
    return body;
  },

  async (error: AxiosError<any>) => {
    const originalRequest = error.config as
      | (InternalAxiosRequestConfig & { _retry?: boolean })
      | undefined;

    if (
      error.response?.status === 401 &&
      originalRequest &&
      !originalRequest._retry
    ) {
      originalRequest._retry = true;

      try {
        const newToken = await refreshAccessToken();

        originalRequest.headers.Authorization = `Bearer ${newToken}`;
        return apiClient(originalRequest);
      } catch (error) {
        handleGlobalLogout();
        return Promise.reject({
          title: "Session Expired",
          detail: "Your session has expired. Please log in again.",
          status: 401,
        });
      }
    }

    if (
      error.response?.data &&
      typeof error.response?.data === "object" &&
      "title" in error.response?.data
    ) {
      const problem = error.response?.data as ProblemDetails;

      const validationErrors = Object.values(problem.errors ?? {})
        .flat()
        .join(" | ");
      const errorDetail =
        validationErrors || problem.detail || "Something went wrong.";

      return Promise.reject({
        title: problem?.title ?? "Unexpected Error",
        detail: errorDetail,
        status: problem?.status ?? error.response?.status ?? 500,
      });
    }

    if (
      error.response?.data &&
      typeof error.response.data === "object" &&
      "isSuccess" in error.response.data
    ) {
      const problem = error.response?.data as Result;
      return Promise.reject({
        title: "Application Error",
        detail: problem.error || "Operation failed",
        status: error.response.status || 400,
      });
    }

    return Promise.reject({
      title: "Network Error",
      detail: error.message || "Cannot connect to server.",
      status: error.response?.status || 0,
    });
  },
);

export default apiClient;
