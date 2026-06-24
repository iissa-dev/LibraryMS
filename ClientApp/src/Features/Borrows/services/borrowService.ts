import apiClient from "../../../api/apiClient";

export type borrowType = {
  clientId: number;
  copyId: number;
};
const controllerName = "/Borrows/";
export const borrowService = {
  borrow: (params: borrowType): Promise<void> =>
    apiClient.post(`${controllerName}borrow`, params),
};
