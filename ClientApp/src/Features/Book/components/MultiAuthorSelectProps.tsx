import { useEffect, useState } from "react";
import { X, Search } from "lucide-react";
import type { AuthorResponseDto } from "../../../types";
import useClickOutside from "../../../hooks/useClickOutside";
import { useAuhtors } from "../../Authors/hooks/author.mutation";
import toast from "react-hot-toast";

type MultiAuthorSelectProps = {
  selectedAuthors: AuthorResponseDto[];
  setSelectedAuthors: (authors: AuthorResponseDto[]) => void;
  disabled: boolean;
};

const MultiAuthorSelect = ({
  selectedAuthors,
  setSelectedAuthors,
  disabled = false,
}: MultiAuthorSelectProps) => {
  const { data, isLoading, isError, error } = useAuhtors();
  const [search, setSearch] = useState("");
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useClickOutside(() => setIsOpen(false));

  useEffect(() => {
    if (isError && error?.detail) {
      toast.error(error.detail);
    }
  }, [isError, error]);

  if (isLoading) {
    return (
      <div className="p-2 text-sm text-text-secondary">Loading authors...</div>
    );
  }

  const allAuthors: AuthorResponseDto[] = data?.items || [];

  const filteredAuthors = allAuthors.filter((author) => {
    const matchesSearch = author.fullName
      .toLowerCase()
      .includes(search.toLowerCase());

    const isNotSelected = !selectedAuthors.some(
      (selected) => selected.id === author.id,
    );

    return matchesSearch && isNotSelected;
  });

  const handleSelectAuthor = (author: AuthorResponseDto) => {
    if (disabled) return;
    setSelectedAuthors([...selectedAuthors, author]);
    setSearch("");
    setIsOpen(false);
  };

  const handleRemoveAuthor = (authorId: number) => {
    if (disabled) return;
    setSelectedAuthors(selectedAuthors.filter((a) => a.id !== authorId));
  };

  return (
    <div className="w-full relative" ref={dropdownRef}>
      <label className="block text-text-secondary text-sm font-medium mb-2">
        Authors
      </label>

      {/* current authors */}
      <div className="flex flex-wrap gap-2 mb-2">
        {selectedAuthors.map((author) => (
          <span
            key={author.id}
            className="flex items-center gap-1.5 bg-primary/10 text-primary border border-primary/20 px-2.5 py-1 rounded-md text-xs font-semibold"
          >
            {author.fullName}
            <button
              type="button"
              onClick={() => {
                if (disabled) return;
                handleRemoveAuthor(author.id);
              }}
              disabled={disabled}
              className="hover:bg-primary/20 p-0.5 rounded-full text-primary/70 hover:text-primary cursor-pointer"
            >
              <X size={12} />
            </button>
          </span>
        ))}
      </div>

      {/* search */}
      <div className="relative">
        <span className="absolute left-3 top-1/2 -translate-y-1/2 text-text-secondary/50">
          <Search size={16} />
        </span>
        <input
          type="text"
          value={search}
          onChange={(e) => {
            setSearch(e.target.value);
            setIsOpen(true);
          }}
          onFocus={() => {
            if (disabled) {
              return;
            }
            setIsOpen(true);
          }}
          placeholder="Search and add authors..."
          className="search-input pl-10"
          readOnly={disabled}
        />
      </div>

      {/* Dropdown list */}
      {isOpen && (
        <div className="absolute -top-20 z-30 mt-1 w-full bg-neutral border border-border/20 rounded-md shadow-xl max-h-48 overflow-y-auto p-1 flex flex-col gap-0.5">
          {filteredAuthors.length > 0 ? (
            filteredAuthors.map((author) => (
              <button
                key={author.id}
                type="button"
                onClick={() => handleSelectAuthor(author)}
                className="w-full text-left px-3 py-2 text-sm rounded-md text-text/80 hover:bg-text/5 hover:text-text transition-all cursor-pointer"
              >
                {author.fullName}
              </button>
            ))
          ) : (
            <div className="text-text-secondary/60 text-xs p-3 text-center">
              No authors found
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default MultiAuthorSelect;
