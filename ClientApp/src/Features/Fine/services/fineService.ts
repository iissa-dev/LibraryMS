import apiClient from "../../../api/apiClient";
import type {
  FineDetailes,
  PagedResult,
  PaginationParams,
} from "../../../types";

const controllerName = "/fines/";
export type fineParams = PaginationParams & { clientId?: number };

export const fineService = {
  getAll: (params: fineParams): Promise<PagedResult<FineDetailes[]>> =>
    apiClient.get(`${controllerName}`, { params }),
  payFine: (fineId: number): Promise<void> =>
    apiClient.put(`${controllerName}${fineId}/pay`),
};
