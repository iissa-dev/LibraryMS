import { useQuery } from "@tanstack/react-query";
import type {
  ClientReservationDto,
  PagedResult,
  ProblemDetails,
} from "../../../types";
import {
  reserveService,
  type reserveParams,
  type ReservationType,
} from "../services/reserveService";
import useGenericMutation from "../../../hooks/useGenericMutation";

const reserveKey = "reservations";

export const useGetReservationsByClientId = ({
  params,
}: {
  params: ReservationType;
}) => {
  return useQuery<PagedResult<ClientReservationDto[]>, ProblemDetails>({
    queryKey: [reserveKey, params],
    queryFn: () => reserveService.getAll(params),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 5,
  });
};

type reserveType = {
  bookId: number;
  clientId: number;
};

export const useReserve = ({ onClose }: { onClose: () => void }) => {
  const mutation = useGenericMutation<reserveType, void>(
    (data) => reserveService.reserve(data.bookId, data.clientId),
    [reserveKey],
    "Reserve Success",
    onClose,
  );

  return mutation;
};

export const useCancelReserve = () => {
  const mutation = useGenericMutation<reserveParams, void>(
    (reserveId) => reserveService.cancel(reserveId),
    [reserveKey],
    "Reserve Cancel",
  );

  return mutation;
};

export const useFullfillReserve = () => {
  const mutation = useGenericMutation<reserveParams, void>(
    (data) => reserveService.fulfuill(data),
    [reserveKey],
    "Fulfill Reserve Successfully",
  );

  return mutation;
};
