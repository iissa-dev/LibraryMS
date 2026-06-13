import apiClient from "../../../api/apiClient";
import type {
  AuthorResponseDto,
  PagedResult,
  PaginationParams,
} from "../../../types";

const controllerName = "/Authors/";

export const authorService = {
  getAll: (
    params: PaginationParams,
  ): Promise<PagedResult<AuthorResponseDto[]>> =>
    apiClient.get(`${controllerName}`, { params }),
};
