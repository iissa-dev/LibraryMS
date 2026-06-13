// import dayjs from "dayjs";
import type {  ProblemDetails } from "../types/index";
import axios from "axios";
// import { jwtDecode } from "jwt-decode";

export const API_BASE_URL = import.meta.env.VITE_API_URL;
const baseURL = `${API_BASE_URL}/api`;
// let refreshPromise: Promise<TokenResult> | null = null;

const apiClient = axios.create({
  baseURL,
  withCredentials: false,
});

// apiClient.interceptors.request.use(async (req) => {
//   const stored = localStorage.getItem("authToken");

//   if (!stored) return req;

//   let authToken: TokenResult;
//   try {
//     authToken = JSON.parse(stored);
//   } catch {
//     localStorage.removeItem("authToken");
//     return req;
//   }

//   if (!authToken.accessToken) return req;

//   const decoded: { exp?: number } = jwtDecode(authToken.accessToken);
//   const isExpired = dayjs().isAfter(dayjs.unix(decoded.exp ?? 0));

//   if (!isExpired) {
//     req.headers.Authorization = `Bearer ${authToken.accessToken}`;
//     return req;
//   }

//   // Refresh token
//   if (!refreshPromise) {
//     refreshPromise = axios
//       .post<TokenResult>(
//         `${baseURL}/Auth/refresh`,
//         {},
//         { withCredentials: true },
//       )
//       .then((res) => {
//         localStorage.setItem("authToken", JSON.stringify(res.data));
//         return res.data;
//       })
//       .finally(() => {
//         refreshPromise = null;
//       });
//   }

//   const newToken = await refreshPromise;

//   req.headers.Authorization = `Bearer ${newToken.accessToken}`;
//   return req;
// });

apiClient.interceptors.response.use(
  (response) => {
    if (response.status < 200 || response.status >= 300) {
      return Promise.reject(response);
    }
    const data = response.data;
    if (data && typeof data === "object" && "isSuccess" in data) {
      if (data.isSuccess) return data.data;
      else throw data.error;
    }
    return response;
  },
  (error) => {
    let customizedError: ProblemDetails = {
      title: "Internal server error",
      status: "500",
      detail: "Cannt connection to saver, try later",
      errors: {},
    };

    if (error.response) {
      const responseData = error.response.data;

      // handle ProblemDetails
      if (
        responseData &&
        typeof responseData === "object" &&
        "title" in responseData
      ) {
        const validationMessages = Object.values(responseData.errors)
          .flat()
          .join(" | ");

        customizedError.detail = validationMessages || customizedError.detail;
      } else if (
        // handle Result pattern
        responseData &&
        typeof responseData === "object" &&
        "isSuccess" in responseData
      ) {
        customizedError = {
          title: "Application Error",
          status: error.response.status.toString(),
          detail: responseData.error || "Operation failed",
          errors: {},
        };
      }
    } else if (error.request) {
      customizedError.title = "Network error";
      customizedError.detail = "No internet connection or server is shutdown";
      customizedError.status = "0";
      customizedError.errors = {};
    }

    return Promise.reject(customizedError);
  },
);

export default apiClient;
