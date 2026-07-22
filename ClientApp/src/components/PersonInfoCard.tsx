import type { ClientResponseDto } from "../types";

const ClientInfoCard = ({ clientInfo }: { clientInfo: ClientResponseDto }) => {
  return (
    <div>
      <div className="flex flex-col md:flex-row gap-4 items-center">
        <div className="md:w-40 md:h-45 bg-neutral object-cover border-4 border-border md:rounded-none rounded-full w-20 h-20">
          {true && <div></div>}
          {false && (
            <div className="text-text-secondary flex items-center justify-center h-full">
              Image Not Found
            </div>
          )}
        </div>
        {/* info */}

        <div className="text-text flex flex-col md:flex-row justify-between md:items-center w-full">
          <div className="text-center">
            <h2 className="text-2xl md:text-4xl font-bold capitalize">
              {clientInfo?.firstName + " " + clientInfo?.lastName}
            </h2>
            <p className="text-text-secondary text-[12px] mb-4">
              Member Library Id: <span>{clientInfo?.libraryCardNumber}</span>
            </p>
          </div>
          <div>
            <p className="text-text-secondary text-[12px] mb-4">
              Country: <span>{clientInfo?.country}</span>
            </p>
            <p className="text-text-secondary text-[12px] mb-4">
              Address: <span>{clientInfo?.address}</span>
            </p>
            <p className="text-text-secondary text-[12px] mb-4">
              Created At: <span>{clientInfo?.createdOn.split("T")[0]}</span>
            </p>
            <span
              className={`text-[12px] py-1 px-2 rounded-2xl ${clientInfo?.createdOn ? "bg-green/10 text-green" : "bg-red/10 text-red"}`}
            >
              {clientInfo?.createdOn ? "Active" : "Unactive"}
            </span>
          </div>
          <div>
            
          </div>
        </div>
      </div>
    </div>
  );
};

export default ClientInfoCard;
