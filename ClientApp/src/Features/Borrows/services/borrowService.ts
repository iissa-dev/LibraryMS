import apiClient from "../../../api/apiClient";
import type {
  BorrowDetails,
  PagedResult,
  PaginationParams,
} from "../../../types";

export type borrowType = {
  clientId: number;
  copyId: number;
};
const controllerName = "/Borrows/";

export type returnType = {
  borrowingId: number;
  copyId: number;
};

export type BorrowParams = PaginationParams & { clientId?: number };
export const borrowService = {
  borrow: (params: borrowType): Promise<void> =>
    apiClient.post(`${controllerName}borrow`, params),
  getFullBorrowDetails: (
    params: BorrowParams,
  ): Promise<PagedResult<BorrowDetails[]>> =>
    apiClient.get(`${controllerName}get-full-borrow-details`, {
      params: {
        pageNumber: params.pageNumber,
        pageSize: params.pageSize,
        clientId: params?.clientId,
      },
    }),
  returnBorrow: (params: returnType): Promise<void> =>
    apiClient.post(`${controllerName}return`, params),
};
