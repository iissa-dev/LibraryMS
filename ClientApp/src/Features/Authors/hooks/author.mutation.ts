import { useQuery } from "@tanstack/react-query";
import { authorService } from "../services/authorService";
import type {
  AuthorResponseDto,
  PagedResult,
  ProblemDetails,
} from "../../../types";

const AuthorKey = "authors";
export const useAuhtors = () => {
  return useQuery<PagedResult<AuthorResponseDto[]>, ProblemDetails>({
    queryKey: [AuthorKey],
    queryFn: () => authorService.getAll({ pageNumber: 1, pageSize: 100 }),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 30,
  });
};
