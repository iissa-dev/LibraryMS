import BookCard from "./BookCard";
import { useDeleteBook, useGetBooks } from "../hooks/book.mutation";
import toast from "react-hot-toast";
import { useEffect, useState } from "react";
import { usePopup } from "../../../components/Popup";
import { PopupType } from "../../../types/popup.types";

type Params = {
  searchByTitle?: string;
  searchByGenre?: number;
  deletedData?: boolean;
  pageNumber: number;
  setPageNumber: React.Dispatch<React.SetStateAction<number>>;
};
const BookCardList = ({
  searchByGenre,
  searchByTitle,
  deletedData,
  pageNumber,
  setPageNumber,
}: Params) => {
  const [pageSize] = useState(4);
  const {
    data: pagedBooks,
    isLoading,
    isError,
    error,
  } = useGetBooks({
    pageNumber,
    pageSize,
    searchByGenre,
    searchByTitle,
    deletedData,
  });

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

  const handleNext = () => {
    if (pagedBooks?.hasNextPage) {
      setPageNumber((prev) => prev + 1);
    }
  };
  const handlePreviouse = () => {
    if (pagedBooks?.hasPreviousPage) {
      setPageNumber((prev) => prev - 1);
    }
  };

  return (
    <>
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
      </div>
      <Modal />
      {/* Pagenations */}
      <div className="flex justify-between items-center flex-col md:flex-row p-4 mt-6 border-t border-border">
        <div className="text-[12px] text-text-secondary mb-4 md:mb-0">
          {
            <p>
              Showing <span>{pagedBooks?.pageNumber}</span> to{" "}
              <span>{pagedBooks?.totalPages}</span> of{" "}
              <span>{pagedBooks?.totalCount}</span> books
            </p>
          }
        </div>
        <div className="flex gap-4 items-center">
          <input
            className={`main-button ${pagedBooks?.hasPreviousPage ? "" : "cursor-not-allowed"}`}
            type="button"
            value={"Previous"}
            onClick={handlePreviouse}
            disabled={!pagedBooks?.hasPreviousPage}
          />
          <input
            className={`main-button ${pagedBooks?.hasNextPage ? "" : "cursor-not-allowed"}`}
            type="button"
            value={"Next"}
            onClick={handleNext}
            disabled={!pagedBooks?.hasNextPage}
          />
        </div>
      </div>
    </>
  );
};

export default BookCardList;
