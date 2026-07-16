import { useState } from "react";
import MainSearch from "../../../components/MainSearch";
import { useParams } from "react-router-dom";
import BorrowTable from "../../Borrows/pages/BorrowTable";
import { useGetClientById } from "../hooks/client.mutation";
import ReservationsTable from "../../Reservations/components/ReservationsTable";

type TapsName = "borrowing" | "reservation" | "fine";
const Members = () => {
  const { clientId } = useParams();

  const [search, setSearch] = useState(clientId ?? "");
  const enabledSearch = search ? true : clientId ? true : false;

  const [activeTap, setActiveTap] = useState<TapsName>("borrowing");
  const { data: clientInfo } = useGetClientById(
    clientId ? Number(clientId) : search ? Number(search) : 0,
    enabledSearch,
  );
  return (
    <div>
      <MainSearch
        search={search}
        setSearch={setSearch}
        placeholder={"Client Id"}
      />

      {/* User Card */}
      {clientInfo && (
        <div className="main-card my-6 p-4">
          <div className="flex gap-4 items-center">
            <div className="w-40 h-45 bg-neutral object-cover border-4 border-border">
              {true && <div></div>}
              {false && (
                <div className="text-text-secondary flex items-center justify-center h-full">
                  Image Not Found
                </div>
              )}
            </div>
            {/* info */}
            <div className="text-text">
              <h2 className="text-4xl font-bold capitalize">
                {clientInfo?.firstName + " " + clientInfo?.lastName}
              </h2>
              <p className="text-text-secondary text-[12px] mb-4">
                Member Library Id: <span>{clientInfo?.libraryCardNumber}</span>
              </p>
              <span
                className={`text-[12px] py-1 px-2 rounded-2xl ${clientInfo?.createdOn ? "bg-green/10 text-green" : "bg-red/10 text-red"}`}
              >
                {clientInfo?.createdOn ? "Active" : "Unactive"}
              </span>
            </div>
          </div>
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
              <BorrowTable
                clientId={
                  clientId
                    ? Number(clientId)
                    : search
                      ? Number(search)
                      : undefined
                }
              />
            </div>
          )}
        </div>
        <div>
          {activeTap === "reservation" && (
            <div>
              <ReservationsTable
                clientId={
                  clientId ? Number(clientId) : search ? Number(search) : 0
                }
              />
            </div>
          )}
        </div>
        <div>{activeTap === "fine" && <div>Borrowing History</div>}</div>
      </div>
    </div>
  );
};

export default Members;
