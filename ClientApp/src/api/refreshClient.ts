import axios from "axios";
import { saveToken } from "./auth";

export const refreshClient = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
  withCredentials: true,
});

let refreshPromise: Promise<string> | null = null;

export async function refreshAccessToken(): Promise<string> {
  if (!refreshPromise) {
    refreshPromise = refreshClient
      .post("/Auth/Refresh")
      .then((response) => {
        const result = response.data;

        if (result.isFailure || !result.data) {
          throw new Error("Refresh failed");
        }

        saveToken(result.data);
        return result.data.accessToken as string;
      })
      .finally(() => {
        refreshPromise = null;
      });
  }
  return refreshPromise;
}
