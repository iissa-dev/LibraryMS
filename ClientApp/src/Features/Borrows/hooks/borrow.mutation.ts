import { useQuery } from "@tanstack/react-query";
import useGenericMutation from "../../../hooks/useGenericMutation";
import {
  borrowService,
  type returnType,
  type BorrowParams,
  type borrowType,
} from "../services/borrowService";
import type {
  ProblemDetails,
  BorrowDetails,
  PagedResult,
} from "../../../types";

type params = {
  onClose: () => void;
};
const borrowKey = "borrows";
export const useCreateBorrow = ({ onClose }: params) => {
  const mutation = useGenericMutation<borrowType, void>(
    (data) => borrowService.borrow(data),
    ["bookCopies"],
    "The loan process was successful.",
    onClose,
  );

  return mutation;
};

export const useBorrowDetails = ({ data }: { data: BorrowParams }) => {
  return useQuery<PagedResult<BorrowDetails[]>, ProblemDetails>({
    queryKey: [borrowKey, data],
    queryFn: () => borrowService.getFullBorrowDetails(data),
    gcTime: 1000 * 60 * 5,
    staleTime: Infinity,
  });
};

export const useReturnBorrow = () => {
  const mutation = useGenericMutation<returnType, void>(
    (data) => borrowService.returnBorrow(data),
    [borrowKey],
    "Return Book Successfully",
  );

  return mutation;
};
