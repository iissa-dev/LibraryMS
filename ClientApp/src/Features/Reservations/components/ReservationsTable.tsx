import { useState } from "react";
import MainTable from "../../../components/MainTable";
import {
  useCancelReserve,
  useGetReservationsByClientId,
} from "../hooks/reserve.mutation";
import { BookMinus, BookPlus } from "lucide-react";
import { usePopup } from "../../../components/Popup";
import { PopupType } from "../../../types/popup.types";
import type { ClientReservationDto } from "../../../types";
import BorrowModal from "../../../components/BorrowModal";

const ReservationsTable = ({ clientId }: { clientId?: number }) => {
  const [pageNumber, setPageNumber] = useState(1);
  const { confirm, Modal } = usePopup();
  const { data: reservationsData } = useGetReservationsByClientId({
    params: {
      pageNumber: pageNumber,
      pageSize: 10,
      clientId: clientId ? Number(clientId) : undefined,
    },
  });
  const [openFulfill, setOpenFulfill] = useState(false);
  const [currentBorrowInfo, setCurrentBorrowInfo] =
    useState<ClientReservationDto | null>(null);
  const cancelMutation = useCancelReserve();
  const handleCancelBorrow = async (id: number) => {
    const ok = await confirm(
      "Are you sure you want to cancel the borrow?",
      "Cancel",
      PopupType.INFO,
    );
    if (!ok || !id) return;

    cancelMutation.mutate({ clientId: Number(clientId), reserveId: id });
  };
  return (
    <div>
      <MainTable
        tableHeader={[
          "reservavtionId",
          "bookId",
          "bookCopyId",
          "Book title",
          "reservation Date",
          "Status Name",
          "Queue",
        ]}
        actions={[
          {
            Icon: BookMinus,
            action: (data: ClientReservationDto) => {
              handleCancelBorrow(data.reservationId);
            },
          },
          {
            Icon: BookPlus,
            action: (data: ClientReservationDto) => {
              setOpenFulfill(true);
              setCurrentBorrowInfo(data);
            },
            allowdRoles: ["Admin", "Employee"],
          },
        ]}
        tableData={reservationsData?.items ?? []}
        showId={false}
        pageNumber={reservationsData?.pageNumber ?? 1}
        totalPages={reservationsData?.totalPages ?? 0}
        totalEntries={reservationsData?.totalCount ?? 0}
        hasNextPage={reservationsData?.hasNextPage ?? false}
        hasPreviousPage={reservationsData?.hasPreviousPage ?? false}
        setPageNumber={setPageNumber}
      />
      <Modal />
      {openFulfill && (
        <BorrowModal
          bookTitle={currentBorrowInfo?.bookTitle ?? ""}
          serialNumber={currentBorrowInfo?.bookId.toString() ?? ""}
          modalMode={"fulfill"}
          onClose={() => setOpenFulfill(false)}
          reserveId={currentBorrowInfo?.reservationId}
        />
      )}
    </div>
  );
};

export default ReservationsTable;
