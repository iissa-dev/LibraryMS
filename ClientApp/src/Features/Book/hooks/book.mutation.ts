import { useQuery } from "@tanstack/react-query";
import type {
  ProblemDetails,
  PagedResult,
  ResponseBookDto,
  PaginationParams,
  CreateBookDto,
} from "../../../types";
import { bookService } from "../services/bookService";
import useGenericMutation from "../../../hooks/useGenericMutation";

const bookKey = "books";
export const useGetBooks = (params: PaginationParams) => {
  return useQuery<PagedResult<ResponseBookDto[]>, ProblemDetails>({
    queryKey: [bookKey, params],
    queryFn: () => bookService.books(params),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 30,
  });
};

export const useDeleteBook = () => {
  const mutaion = useGenericMutation<number, boolean>(
    (id) => bookService.delete(id),
    [bookKey],
    "Book has been deleted successfully.",
  );

  return mutaion;
};

export const useAddBook = (onClose: () => void) => {
  const mutaion = useGenericMutation<FormData, void>(
    (data) => bookService.add(data),
    [bookKey],
    "Book has been added successfully.",
    onClose,
  );

  return mutaion;
};
