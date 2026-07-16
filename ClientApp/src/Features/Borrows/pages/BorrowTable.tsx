import { PackageMinus } from "lucide-react";
import MainTable from "../../../components/MainTable";
import type { returnType } from "../services/borrowService";
import { useState } from "react";
import { usePopup } from "../../../components/Popup";
import { useBorrowDetails, useReturnBorrow } from "../hooks/borrow.mutation";
import { PopupType } from "../../../types/popup.types";

const BorrowTable = ({ clientId }: { clientId?: number }) => {
  const { confirm, Modal } = usePopup();
  const returnMutation = useReturnBorrow();

  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);
  const { data: borrows, isLoading } = useBorrowDetails({
    data: {
      pageNumber,
      pageSize: pageSize,
      clientId: isNaN(Number(clientId)) ? undefined : Number(clientId),
    },
  });
  if (isLoading) {
    return <div>Loading...</div>;
  }

  const mapping = Object.values(borrows?.items ?? {}).map((item) => {
    return {
      copyId: item.copyId,
      borrowId: item.borrowId,
      bookTitle: item.book.title,
      borrower: item.borrower.clientName,
      issueDate: item.borrowDate,
      dueDate: item.dueDate,
      status: item.status,
    };
  });

  const handleReturnBook = async (data: any) => {
    const ok = await confirm(
      "Are you sure you want to return the book?",
      "Return",
      PopupType.INFO,
    );

    if (!ok) return;

    returnMutation.mutate({
      borrowingId: data.borrowId,
      copyId: data.copyId,
    });
  };

  
  return (
    <div>
      <div>{/*Filter*/}</div>

      <div>
        <MainTable
          tableHeader={[
            "copy Id",
            "Book Id",
            "Book Details",
            "borrower",
            "issue date",
            "due date",
            "status",
          ]}
          actions={[
            {
              Icon: PackageMinus,
              action: (data: returnType) => {
                handleReturnBook(data);
              },
            },
          ]}
          tableData={mapping}
          showId={false}
          pageNumber={pageNumber}
          totalPages={borrows?.totalPages ?? 0}
          totalEntries={borrows?.totalCount ?? 0}
          hasNextPage={borrows?.hasNextPage ?? false}
          hasPreviousPage={borrows?.hasPreviousPage ?? false}
          setPageNumber={(value) => setPageNumber(value)}
        />
      </div>
      <Modal />
    </div>
  );
};

export default BorrowTable;
