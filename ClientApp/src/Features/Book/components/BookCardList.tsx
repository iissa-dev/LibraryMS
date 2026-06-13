import BookCard from "./BookCard";
import { useDeleteBook, useGetBooks } from "../hooks/book.mutation";
import toast from "react-hot-toast";
import { useEffect } from "react";
import { usePopup } from "../../../components/Popup";
import { PopupType } from "../../../types/popup.types";
import type { PaginationParams } from "../../../types";

const BookCardList = ({ pageNumber, pageSize }: PaginationParams) => {
  const {
    data: pagedBooks,
    isLoading,
    isError,
    error,
  } = useGetBooks({ pageNumber, pageSize });

  const { confirm, Modal } = usePopup();

  const deleteMutation = useDeleteBook();

  useEffect(() => {
    if (isError && error?.detail) {
      toast.error(error.detail);
    }
  }, [isError, error]);

  if (isLoading) {
    return <div className="p-4 text-center">Loading...</div>;
  }

  const books = pagedBooks?.items || [];

  const handleDeleteBook = async (id: number) => {
    const ok = await confirm(
      "Are you sure you want to delete this book?",
      "Delete",
      PopupType.WARNING,
    );
    if (!ok) return;

    deleteMutation.mutate(id);
  };
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 p-4">
      {books.length === 0 ? (
        <div className="col-span-full text-center text-gray-500">
          No books found.
        </div>
      ) : (
        books.map((book) => (
          <BookCard key={book.id} book={book} onDelete={handleDeleteBook} />
        ))
      )}
      <Modal />
    </div>
  );
};

export default BookCardList;
