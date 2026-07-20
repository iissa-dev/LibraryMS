import apiClient from "../../../api/apiClient";
import type {
  ClientReservationDto,
  PagedResult,
  PaginationParams,
} from "../../../types";

const controllerName = "/Reservations/";

export type reserveParams = {
  reserveId: number;
  clientId: number;
};
export type ReservationType = PaginationParams & { clientId?: number };
export const reserveService = {
  getAll: (
    params: ReservationType,
  ): Promise<PagedResult<ClientReservationDto[]>> =>
    apiClient.get(`${controllerName}`, { params }),

  reserve: (bookId: number, clientId: number): Promise<void> =>
    apiClient.post(`${controllerName}reserve`, {
      bookId,
      clientId,
    }),
  cancel: (params: reserveParams): Promise<void> =>
    apiClient.put(`${controllerName}cancel`, { ...params }),
  fulfuill: (data: reserveParams): Promise<void> =>
    apiClient.put(`${controllerName}fulfill`, {
      ...data,
    }),
};
