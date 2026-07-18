import { useQuery } from "@tanstack/react-query";
import { fineService, type fineParams } from "../services/fineService";
import type { FineDetailes, PagedResult, ProblemDetails } from "../../../types";
import useGenericMutation from "../../../hooks/useGenericMutation";

const fineKey = "fines";

export const useGetFines = (params: fineParams) => {
  return useQuery<PagedResult<FineDetailes[]>, ProblemDetails>({
    queryKey: [fineKey, params],
    queryFn: () => fineService.getAll(params),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 5,
  });
};

export const usePayFine = () => {
  const mutation = useGenericMutation<number, void>(
    (fineId) => fineService.payFine(fineId),
    [fineKey],
    "Pay Fine Successfully",
  );

  return mutation;
};
