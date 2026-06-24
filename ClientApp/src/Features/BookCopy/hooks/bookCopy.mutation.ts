import { useQuery } from "@tanstack/react-query";
import type {
  PagedResult,
  ResponseBookCopiesDto,
  ProblemDetails,
} from "../../../types";
import {
  bookCopyService,
  type bookCopyParams,
} from "../services/bookCopyService";

const copyKey = "bookCopies";

export const useBookCopies = (params: bookCopyParams) => {
  return useQuery<PagedResult<ResponseBookCopiesDto[]>, ProblemDetails>({
    queryKey: [copyKey, params],
    queryFn: () => bookCopyService.bookCopies(params),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 5,
  });
};
