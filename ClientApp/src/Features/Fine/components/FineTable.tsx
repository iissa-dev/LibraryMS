import { useState } from "react";
import MainTable from "../../../components/MainTable";
import { useGetFines, usePayFine } from "../hooks/fine.mutation";
import { BanknoteArrowUp } from "lucide-react";
import type { FineDetailes } from "../../../types";
import { usePopup } from "../../../components/Popup";
import { PopupType } from "../../../types/popup.types";

const FineTable = ({ clientId }: { clientId?: number }) => {
  const { confirm, Modal } = usePopup();
  const [pageNumber, setPageNumber] = useState(1);

  const { data: fines, isLoading } = useGetFines({
    pageNumber: pageNumber,
    pageSize: 10,
    clientId: clientId,
  });

  const payMutation = usePayFine();
  if (isLoading) {
    return <div>loading..</div>;
  }

  const tableDataMapping = Object.values(fines?.items ?? []).map((item) => {
    return {
      fineId: item.fineId,
      borrower: item.borrower.clientName,
      borrowingDate: item.borrowingDate,
      returnDate: item.returnDate,
      reason: item.reason,
      fineAmount: item.fineAmount.toString() + "$",
      paymentStatus: item.paymentStatus,
    };
  });

  const handlePay = async (fineId: number) => {
    const ok = await confirm(
      "Are you sure you want to pay this bill?",
      "Pay",
      PopupType.WARNING,
    );
    if (!ok) return;

    payMutation.mutate(fineId);
  };
  return (
    <div>
      <MainTable
        tableHeader={[
          "FineId",
          "borrower",
          "borrowing Date",
          "return date",
          "reason",
          "Amount",
          "paymentStatus",
        ]}
        actions={[
          {
            Icon: BanknoteArrowUp,
            action: (data: FineDetailes) => {
              handlePay(data.fineId);
            },
            allowdRoles: ["Admin", "Employee"],
          },
        ]}
        tableData={tableDataMapping}
        showId={false}
        pageNumber={pageNumber}
        totalPages={fines?.totalPages ?? 0}
        totalEntries={fines?.totalCount ?? 0}
        hasNextPage={fines?.hasNextPage ?? false}
        hasPreviousPage={fines?.hasPreviousPage ?? false}
        setPageNumber={(value) => setPageNumber(value)}
      />
      <Modal />
    </div>
  );
};

export default FineTable;
