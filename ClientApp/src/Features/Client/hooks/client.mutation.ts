import { useQuery } from "@tanstack/react-query";
import useGenericMutation from "../../../hooks/useGenericMutation";
import type {
  ClientResponseDto,
  ProblemDetails,
  RegisterDto,
} from "../../../types";
import { clientService } from "../services/clientService";

type Params = {
  method: () => void;
};
export const useRegister = ({ method }: Params) => {
  const mutation = useGenericMutation<RegisterDto, void>(
    (data) => clientService.register(data),
    ["clients"],
    "New Account Created",
    method,
  );
  return mutation;
};

export const useGetClientById = (id: number, enabled: boolean) => {
  return useQuery<ClientResponseDto, ProblemDetails>({
    queryKey: ["get-client-profile"],
    queryFn: () => clientService.getByClientId(id),
    enabled: enabled,
    retry: false,
  });
};
