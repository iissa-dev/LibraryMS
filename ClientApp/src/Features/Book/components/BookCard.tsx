import { Pen, Trash2 } from "lucide-react";
import { useNavigate } from "react-router-dom";
import type { ResponseBookDto } from "../../../types";
import { API_BASE_URL } from "../../../api/apiClient";

type BookCardProps = {
  book: ResponseBookDto;
  onDelete?: (id: number) => void;
};

const BookCard = ({ book, onDelete }: BookCardProps) => {
  const navigation = useNavigate();

  const handleEditClick = () => {
    navigation(`/bookManagement/edit/${book.id}`, {
      state: { currentBook: book },
    });
  };

  const handleViewHistory = () => {
    navigation(`/bookManagement/view/${book.id}`, {
      state: { currentBook: book },
    });
  };
  return (
    <div className="w-full flex flex-col bg-white border border-border shadow-sm rounded-card overflow-hidden transition-all duration-300 hover:shadow-md">
      {/* Image */}
      <div className="relative w-full h-64 bg-neutral/10 overflow-hidden">
        <span
          className={`absolute top-4 right-4 z-10 text-[12px] p-1 rounded-md ${book.isDeleted ? " text-red bg-red/30" : "text-green bg-green/30"}`}
        >
          {book.isDeleted ? "Deleted" : "Existing"}
        </span>
        <img
          className="w-full h-full object-cover transition-transform duration-500 hover:scale-105"
          src={`${API_BASE_URL}${book.bookImageUrl}`}
          alt={"Image not found"}
          onError={(e) => {
            (e.target as HTMLImageElement).src = "";
          }}
        />
      </div>

      {/* Details section */}
      <div className="p-4 flex flex-col flex-1 justify-between">
        <div className="mb-4">
          <p
            className="font-bold text-secondary text-base truncate mb-1 cursor-pointer hover:text-primary transition-colors"
            title={book.title}
            onClick={handleViewHistory}
          >
            {book.title}
          </p>
          <p className="text-text-secondary text-xs mb-1 truncate">
            By{" "}
            {book.authors && book.authors.length > 0
              ? book.authors.map((a) => a.fullName).join(", ")
              : "Unknown Author"}
          </p>
          <p className="text-text-secondary/70 text-[11px] font-mono">
            ISBN: {book.isbn}
          </p>
        </div>

        {/* Actions */}
        <div className="text-[12px] flex items-center justify-between pt-3 border-t border-border/60">
          <div className="flex items-center gap-3">
            <button
              onClick={handleEditClick}
              className="cursor-pointer text-text-secondary hover:text-blue-600 transition-colors duration-200"
              type="button"
              title="Edit Book"
            >
              <Pen size={14} />
            </button>
            <button
              onClick={() => onDelete && onDelete(book.id)}
              className="cursor-pointer text-text-secondary hover:text-red-600 transition-colors duration-200"
              type="button"
              title="Delete Book"
            >
              <Trash2 size={14} />
            </button>
          </div>

          <button
            onClick={handleViewHistory}
            className="cursor-pointer text-primary font-semibold hover:underline bg-primary/5 px-2 py-1 rounded transition-all"
            type="button"
          >
            View History
          </button>
        </div>
      </div>
    </div>
  );
};

export default BookCard;
