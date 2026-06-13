import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { ProblemDetails } from "../types";
import toast from "react-hot-toast";

const useGenericMutation = <TInput, TOutput>(
  mutationFn: (data: TInput) => Promise<TOutput>,
  queryKey: string[],
  successMessage: string,
  onClose?: () => void,
) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn,
    onSuccess: async () => {
      toast.success(successMessage);
      await Promise.all(
        queryKey.map((key) =>
          queryClient.invalidateQueries({ queryKey: [key] }),
        ),
      );
      if (onClose) onClose();
    },
    onError: async (error: any) => {
      const apiError = error as ProblemDetails;
      const errorMsg = apiError.detail || "An unexpected error occurred";
      toast.error(errorMsg);
    },
  });
};

export default useGenericMutation;
