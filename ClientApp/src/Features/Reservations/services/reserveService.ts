import apiClient from "../../../api/apiClient";
import type {
  ClientReservationDto,
  PagedResult,
  PaginationParams,
} from "../../../types";

const controllerName = "/Reservations/";

export type fulfillType = {
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
  cancel: (reserveId: number): Promise<void> =>
    apiClient.put(`${controllerName}${reserveId}/cancel`),
  fulfuill: (data: fulfillType): Promise<void> =>
    apiClient.put(`${controllerName}fulfill`, {
      ...data,
    }),
};
