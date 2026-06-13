import apiClient from "../../../api/apiClient";
import type {
  CreateBookDto,
  PagedResult,
  PaginationParams,
  ResponseBookDto,
} from "../../../types";

const controllerName = "/Books/";

export const bookService = {
  books: (params: PaginationParams): Promise<PagedResult<ResponseBookDto[]>> =>
    apiClient.get(`${controllerName}`, { params }),
  delete: (id: number): Promise<boolean> =>
    apiClient.delete(`${controllerName}${id}`),
  add: (params: FormData): Promise<void> =>
    apiClient.post(`${controllerName}`, params, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }),
};
