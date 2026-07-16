import { ArrowRight, BookOpenCheckIcon, Search, X } from "lucide-react";
import { useGetClientById } from "../Features/Client/hooks/client.mutation";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useQueryClient } from "@tanstack/react-query";
import { useCreateBorrow } from "../Features/Borrows/hooks/borrow.mutation";
import {
  useFullfillReserve,
  useReserve,
} from "../Features/Reservations/hooks/reserve.mutation";

type BorrowModalProps = {
  bookCopyId?: number;
  bookId?: number;
  reserveId?: number;
  bookTitle: string;
  serialNumber: string;
  modalMode: "borrow" | "reserve" | "fulfill";
  onClose: () => void;
};

const BorrowModal = ({
  bookCopyId,
  bookId,
  reserveId,
  bookTitle,
  serialNumber,
  modalMode,
  onClose,
}: BorrowModalProps) => {
  const [clientId, setClientId] = useState<number>(0);
  const [isSearching, setIsSearching] = useState<boolean>(false);
  const queryClient = useQueryClient();
  const {
    data: clientInfo,
    isFetching,
    isError,
    error,
  } = useGetClientById(clientId, isSearching);

  const handleClientId = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = isNaN(Number(e.target.value)) ? 0 : Number(e.target.value);
    setClientId(value);

    if (isSearching) setIsSearching(false);
  };

  const handleSearchById = () => {
    if (clientId > 0 || !isSearching) {
      setIsSearching(true);
    }
  };

  const handleCloseAndReset = () => {
    setClientId(0);
    setIsSearching(false);
    queryClient.removeQueries({ queryKey: ["get-client-profile"] });
    onClose();
  };

  const borrowMutation = useCreateBorrow({ onClose: handleCloseAndReset });
  const reserveMutation = useReserve({ onClose: handleCloseAndReset });
  const fulfill = useFullfillReserve();

  useEffect(() => {
    if (isError && error) {
      toast.error(`${error.detail}`);
    }
  }, [isError]);

  const handleBorrow = () => {
    if (clientId > 0 && bookCopyId && bookCopyId > 0) {
      borrowMutation.mutate({ clientId: clientId, copyId: bookCopyId });
    }
  };

  const handleReserve = () => {
    if (clientId > 0 && bookId && bookId > 0) {
      reserveMutation.mutate({ bookId: bookId, clientId: clientId });
    }
  };

  const handleFulfill = () => {
    if (clientId > 0 && reserveId) {
      fulfill.mutate({ reserveId, clientId });
    }
  };
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 backdrop-blur-sm">
      <div className="bg-background-secondary rounded-xl shadow-xl border border-border p-6 w-full max-w-md relative">
        <button
          onClick={handleCloseAndReset}
          className="absolute top-4 right-4 text-text-secondary hover:text-primary transition-colors cursor-pointer"
        >
          <X size={20} />
        </button>
        <h3 className="text-lg font-bold text-secondary">
          {modalMode === "borrow" && "Borrow Book Copy"}
          {modalMode === "reserve" && "Reserve Book Copy"}
        </h3>
        <p className="text-sm text-text-secondary mb-4 border-b border-border pb-2">
          Assign a physical copy to a registered member.
        </p>
        <div className="bg-background flex items-center gap-4 p-2">
          <div className="bg-primary/50 text-primary p-2 ">
            <BookOpenCheckIcon size={30} />
          </div>
          <div>
            <p className="uppercase text-primary font-semibold">
              Active Inventory
            </p>
            <p className="font-bold text-text">{bookTitle}</p>
            <p className="text-text-secondary text-[12px]">{serialNumber}</p>
          </div>
        </div>
        <div className="mt-6">
          <label
            htmlFor="member-search"
            className="text-[12px] text-text-secondary"
          >
            MEMBER SEARCH
          </label>
          <div className="flex items-center gap-2">
            <input
              type="text"
              id="member-search"
              className="search-input"
              placeholder="Search"
              onChange={handleClientId}
            />
            <div
              className="bg-primary text-background p-2 rounded-md"
              onClick={handleSearchById}
            >
              <Search size={15} className="cursor-pointer" />
            </div>
          </div>
        </div>
        {/* Dynamic Member Data Section */}
        <div className="mt-6 p-3 bg-background/50 rounded-lg border border-border min-h-16.25 flex flex-col justify-center">
          {isFetching ? (
            <p className="text-sm text-text-secondary text-center animate-pulse">
              Searching for member...
            </p>
          ) : isError ? (
            <p className="text-sm text-red text-center font-semibold">
              {error.detail}
            </p>
          ) : clientInfo ? (
            <div className="flex flex-col gap-1 animate-in fade-in duration-200">
              <p className="text-xs uppercase text-green font-bold">
                Member Confirmed
              </p>
              <p className="font-semibold text-secondary">
                {clientInfo.firstName + " " + clientInfo.lastName}
              </p>
              <p className="text-[11px] text-text-secondary">
                Library Number: {clientInfo.libraryCardNumber}
              </p>
            </div>
          ) : (
            <p className="text-sm text-text-secondary text-center italic">
              Enter ID and click search to append member data.
            </p>
          )}
        </div>

        {/* Info */}
        <div className="mt-6 border-b border-border pb-2">
          <p className="text-text-secondary text-[12px]">
            Maximum loan duration for studnets 14 days.
          </p>
        </div>
        <div className="mt-6 flex flex-row-reverse gap-4">
          {modalMode === "borrow" && (
            <button
              onClick={handleBorrow}
              type="button"
              className="main-button flex gap-1 items-center text-sm"
            >
              Confirm Borrow <ArrowRight size={15} className="font-bold" />
            </button>
          )}
          {modalMode === "reserve" && (
            <button
              onClick={handleReserve}
              type="button"
              className="main-button flex gap-1 items-center text-sm"
            >
              Confirm Reserve <ArrowRight size={15} className="font-bold" />
            </button>
          )}
          {modalMode === "fulfill" && (
            <button
              onClick={handleFulfill}
              type="button"
              className="main-button flex gap-1 items-center text-sm"
            >
              Fulfill <ArrowRight size={15} className="font-bold" />
            </button>
          )}
          <input
            onClick={handleCloseAndReset}
            type="button"
            value={"Cancel"}
            className="secondary-button text-sm"
          />
        </div>
      </div>
    </div>
  );
};

export default BorrowModal;
