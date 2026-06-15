import { useQuery } from "@tanstack/react-query";
import type {
  ProblemDetails,
  PagedResult,
  ResponseBookDto,
  PaginationParams,
} from "../../../types";
import { bookService } from "../services/bookService";
import useGenericMutation from "../../../hooks/useGenericMutation";

const bookKey = "books";
export type BooksParams = PaginationParams & {
  searchByTitle?: string;
  searchByGenre?: number;
  deletedData?: boolean;
};

export const useGetBooks = (params: BooksParams) => {
  return useQuery<PagedResult<ResponseBookDto[]>, ProblemDetails>({
    queryKey: [bookKey, params],
    queryFn: () => bookService.books(params),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 30,
  });
};

export const useGetBookById = (id: number, options?: any) => {
  return useQuery<ResponseBookDto, ProblemDetails>({
    queryKey: ["book-by-id"],
    queryFn: () => bookService.getById(id),
    staleTime: Infinity,
    gcTime: 1000 * 60 * 30,
    ...options,
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

export const useUpdateBook = (id: number, onClose: () => void) => {
  const mutation = useGenericMutation<FormData, void>(
    (data) => bookService.update(id, data),
    [bookKey],
    "Book has been updated successfully.",
    onClose,
  );

  return mutation;
};
