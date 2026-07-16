import { BookPlus, PackagePlus } from "lucide-react";
import { useState } from "react";
import { useParams } from "react-router-dom";
import MainTable from "../../../components/MainTable";
import { CopyStatus } from "../../../constants/bookCopyStatus.constant";
import type { ResponseBookCopiesDto } from "../../../types";
import BorrowModal from "../../../components/BorrowModal";
import { useBookCopies } from "../hooks/bookCopy.mutation";

const BookCopyTable = () => {
  const { bookId } = useParams();
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [copyStatus, setCopyStatus] = useState<number | undefined>(undefined);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState<"borrow" | "reserve">("borrow");
  const [selectedCopy, setSelectedCopy] =
    useState<ResponseBookCopiesDto | null>(null);

  const { data, isLoading } = useBookCopies({
    pageNumber: pageNumber,
    pageSize: pageSize,
    bookId: Number.isNaN(Number(bookId)) ? undefined : Number(bookId),
    filterByStatus: copyStatus,
  });

  const handleSelectPageSize = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPageNumber(1);
    setPageSize(Number(e.target.value));
  };

  const handleSelectStatus = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = e.target.value;
    setCopyStatus(selectedValue === "" ? undefined : Number(selectedValue));
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }
  return (
    <div>
      {/* Filter */}
      <div className="main-card flex justify-between items-center mb-6 text-text">
        {/* PageSize */}
        <div>
          show{" "}
          <select
            className="bg-background-secondary p-1 rounded-sm mx-2 outline-none"
            onChange={handleSelectPageSize}
            id="page-size"
            value={pageSize}
          >
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={15}>15</option>
          </select>{" "}
          entries
        </div>
        {/* Filter By Status */}
        <div>
          <select
            value={copyStatus ?? ""}
            id="copy-status"
            className="search-input"
            onChange={handleSelectStatus}
          >
            <option value="">All Copies</option>
            {CopyStatus.map((status, i) => (
              <option key={`${status.name}-${i}`} value={status.value}>
                {status.name}
              </option>
            ))}
          </select>
        </div>
      </div>
      {/* Table */}
      <MainTable
        tableHeader={["Title", "ISBN", "Serial Number", "Status"]}
        tableData={data?.items ?? []}
        actions={[
          {
            Icon: PackagePlus,
            action: (data: ResponseBookCopiesDto) => {
              setIsModalOpen(true);
              setModalMode("borrow");
              setSelectedCopy({
                bookCopyId: data.bookCopyId,
                title: data.title,
                serialNumber: data.serialNumber,
                bookId: data.bookId,
                isbn: data.isbn,
                status: data.status,
              });
            },
          },
          {
            Icon: BookPlus,
            action: (data: ResponseBookCopiesDto) => {
              setIsModalOpen(true);
              setModalMode("reserve");
              setSelectedCopy({
                bookCopyId: data.bookCopyId,
                title: data.title,
                serialNumber: data.serialNumber,
                bookId: data.bookId,
                isbn: data.isbn,
                status: data.status,
              });
            },
          },
        ]}
        showId={false}
        pageNumber={pageNumber}
        totalPages={data?.totalPages ?? 0}
        totalEntries={data?.totalCount ?? 0}
        setPageNumber={setPageNumber}
        hasNextPage={data?.hasNextPage ?? false}
        hasPreviousPage={data?.hasPreviousPage ?? false}
      />
      {isModalOpen && (
        <BorrowModal
          modalMode={modalMode}
          onClose={() => setIsModalOpen(false)}
          bookTitle={selectedCopy?.title ?? ""}
          serialNumber={selectedCopy?.serialNumber ?? ""}
          bookCopyId={selectedCopy?.bookCopyId ?? 0}
          bookId={selectedCopy?.bookId ?? 0}
        />
      )}
    </div>
  );
};

export default BookCopyTable;
