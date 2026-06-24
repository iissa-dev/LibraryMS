import apiClient from "../../../api/apiClient";
import type {
  PagedResult,
  PaginationParams,
  ResponseBookCopiesDto,
} from "../../../types";

const controllerName = "/Books/";

export type bookCopyParams = PaginationParams & {
  bookId?: number;
  filterByStatus?: number | null;
};

export const bookCopyService = {
  bookCopies: (
    params: bookCopyParams,
  ): Promise<PagedResult<ResponseBookCopiesDto[]>> =>
    apiClient.get(`${controllerName}copies`, { params, withCredentials: true }),
};
