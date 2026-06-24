import type { TokenResult } from "../types";

const AUTH_TOKEN_KEY = "authToken";

export interface StoredToken {
  accessToken: string;
  refreshTokenExpirationDate: string;
}

export function getStoredToken(): TokenResult | null {
  const raw = localStorage.getItem(AUTH_TOKEN_KEY);

  if (!raw) {
    return null;
  }

  try {
    return JSON.parse(raw) as TokenResult;
  } catch {
    localStorage.removeItem(AUTH_TOKEN_KEY);
    return null;
  }
}

export function getAccessToken(): string | null {
  return getStoredToken()?.accessToken ?? null;
}

export function saveToken(token: TokenResult) {
  localStorage.setItem(AUTH_TOKEN_KEY, JSON.stringify(token));
}

export function handleGlobalLogout() {
  localStorage.removeItem(AUTH_TOKEN_KEY);

  if (window.location.pathname !== "/login") {
    window.location.replace("/login");
  }
}

export function clearAuth() {
  localStorage.removeItem(AUTH_TOKEN_KEY);
}
