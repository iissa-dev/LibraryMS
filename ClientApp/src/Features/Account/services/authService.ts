import apiClient from "../../../api/apiClient";
import type { LoginDto, Result, TokenResult } from "../../../types";

const controllerName = "/Auth/";

export const authService = {
  login: (params: LoginDto): Promise<TokenResult> =>
    apiClient.post(`${controllerName}login`, params),
  logout: (): Promise<Result> => apiClient.put(`${controllerName}logout`),
};
