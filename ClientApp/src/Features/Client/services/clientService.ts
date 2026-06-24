import apiClient from "../../../api/apiClient";
import type { ClientResponseDto, RegisterDto } from "../../../types";

const controllerName = "/Clients/";

export const clientService = {
  register: (params: RegisterDto): Promise<void> =>
    apiClient.post(`${controllerName}`, params),
  getByClientId: (id: number): Promise<ClientResponseDto> =>
    apiClient.get(`${controllerName}get-client-profile/${id}`),
};
