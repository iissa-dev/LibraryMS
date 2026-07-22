import { useState } from "react";
import MainSearch from "../../../components/MainSearch";
import { useParams } from "react-router-dom";
import BorrowTable from "../../Borrows/pages/BorrowTable";
import { useGetClientById } from "../hooks/client.mutation";
import ReservationsTable from "../../Reservations/components/ReservationsTable";
import FineTable from "../../Fine/components/FineTable";
import { useAuth } from "../../../hooks/useAuth";
import ClientInfoCard from "../../../components/PersonInfoCard";

type TapsName = "borrowing" | "reservation" | "fine";
const Members = () => {
  const { token } = useAuth();
  const { clientId } = useParams();

  const [search, setSearch] = useState(
    clientId ?? token?.clientId?.toString() ?? "",
  );
  const ClientId = clientId
    ? Number(clientId)
    : search
      ? Number(search)
      : token?.clientId
        ? token.clientId
        : undefined;
  const enabledSearch = search ? true : clientId ? true : false;

  const [activeTap, setActiveTap] = useState<TapsName>("borrowing");
  const { data: clientInfo } = useGetClientById(ClientId ?? 0, enabledSearch);
  return (
    <div>
      <MainSearch
        search={search}
        setSearch={setSearch}
        placeholder={"Client Id"}
        disabled={token?.clientId !== null}
      />

      {/* User Card */}
      {clientInfo && (
        <div className="main-card my-6 p-4">
          <ClientInfoCard clientInfo={clientInfo} />
        </div>
      )}

      <div className="my-6">
        {/* Taps */}
        <div className="text-text-secondary flex gap-4 items-center text-sm mb-6 select-none">
          <span
            onClick={() => setActiveTap("borrowing")}
            className={`cursor-pointer  transition-all duration-300 ${
              activeTap === "borrowing"
                ? "text-primary border-b-2 border-primary font-bold"
                : "hover:text-primary"
            }`}
          >
            Borrwoing History
          </span>
          <span
            onClick={() => setActiveTap("reservation")}
            className={`cursor-pointer  transition-all duration-300 ${
              activeTap === "reservation"
                ? "text-primary border-b-2 border-primary font-bold"
                : "hover:text-primary"
            }`}
          >
            Active Reservations
          </span>
          <span
            onClick={() => setActiveTap("fine")}
            className={`cursor-pointer  transition-all duration-300 ${
              activeTap === "fine"
                ? "text-primary border-b-2 border-primary font-bold"
                : "hover:text-primary"
            }`}
          >
            Detailed Fines Log
          </span>
        </div>

        <div>
          {activeTap === "borrowing" && (
            <div>
              <BorrowTable clientId={ClientId} />
            </div>
          )}
        </div>
        <div>
          {activeTap === "reservation" && (
            <div>
              <ReservationsTable clientId={ClientId} />
            </div>
          )}
        </div>
        <div>{activeTap === "fine" && <FineTable clientId={ClientId} />}</div>
      </div>
    </div>
  );
};

export default Members;
