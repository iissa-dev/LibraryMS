import { ArrowLeft, ArrowRight, type LucideProps } from "lucide-react";
import { type ForwardRefExoticComponent, type RefAttributes } from "react";

type actionParams = {
  Icon: ForwardRefExoticComponent<
    Omit<LucideProps, "ref"> & RefAttributes<SVGSVGElement>
  >;
  action: (data?: any) => void;
};
type tableParams<T> = {
  tableHeader: string[];
  tableData: T[];
  showId: boolean;
  pageNumber: number;
  totalPages: number;
  totalEntries: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  actions?: actionParams[];
  setPageNumber: React.Dispatch<React.SetStateAction<number>>;
};

function getStatusStyle(value: string) {
  const status = value.toLowerCase();

  switch (status) {
    case "available":
    case "completed":
    case "paid":
      return {
        style: "bg-green/30 rounded-2xl px-2 py-1 text-green text-[12px]",
        label: status.charAt(0).toUpperCase() + status.slice(1),
      };

    case "unpaid":
      return {
        style: "bg-red/30 rounded-2xl px-2 py-1 text-red text-[12px]",
        label: status.charAt(0).toUpperCase() + status.slice(1),
      };
    default:
      return {
        style:
          "bg-text-secondary/30 rounded-2xl px-2 py-1 text-secondary text-[12px]",
        label: value,
      };
  }
}

function formatData(key: string, value: string) {
  if (
    key.toLowerCase().includes("created") ||
    key.toLowerCase().includes("date")
  ) {
    return new Date(value).toLocaleDateString("en-GB", {
      day: "2-digit",
      month: "short",
      year: "numeric",
    });
  }

  return value;
}
const MainTable = <T extends Record<string, any>>({
  tableHeader,
  tableData,
  actions,
  showId,
  pageNumber,
  totalPages,
  totalEntries,
  hasNextPage,
  hasPreviousPage,
  setPageNumber,
}: tableParams<T>) => {
  const hasActions = actions;
  const totalColumns = hasActions ? tableHeader.length + 1 : tableHeader.length;
  const hanleShowId = (str: string) => {
    return !showId && str.toLowerCase().includes("id");
  };

  const handleNextPage = () => {
    if (hasNextPage) setPageNumber((prev) => prev + 1);
  };
  const handlePrevious = () => {
    if (hasPreviousPage) setPageNumber((prev) => prev - 1);
  };

  return (
    <div className=" flex flex-col overflow-hidden">
      {/* Table part */}
      <div className="overflow-x-scroll rounded-md">
        <table className="w-full">
          {/* Header */}
          <thead className="text-text">
            <tr>
              {tableHeader &&
                tableHeader.map((h, i) => {
                  if (hanleShowId(h)) return null;

                  return (
                    <th
                      key={i}
                      className="px-6 py-4 text-xs font-bold uppercase tracking-widest bg-border "
                    >
                      {h}
                    </th>
                  );
                })}
              {hasActions && (
                <th className="px-6 py-4 text-xs font-bold uppercase tracking-widest bg-border">
                  Action
                </th>
              )}
            </tr>
          </thead>

          {/* Body */}
          <tbody className="border-t-transparent border-border border text-text text-nowrap">
            {tableData &&
              tableData.map((row, i) => (
                <tr
                  key={i}
                  className="text-center border-b border-border transition-all duration-300 hover:bg-background-secondary"
                >
                  {Object.entries(row).map(([key, cell], j) => {
                    if (hanleShowId(key)) return null;
                    const status = getStatusStyle(String(cell));
                    const value = formatData(key, cell);
                    return (
                      <td key={j} className="px-6 py-4 text-[12px] font-medium">
                        {key.toLowerCase().includes("status") ? (
                          <span className={`${status?.style}`}>
                            {status?.label}
                          </span>
                        ) : (
                          <span>{value}</span>
                        )}
                      </td>
                    );
                  })}
                  <td>
                    {hasActions &&
                      actions.map((action, i) => (
                        <action.Icon
                          key={i}
                          size={18}
                          className="inline mr-2 cursor-pointer"
                          onClick={() => action.action(row)}
                        />
                      ))}
                  </td>
                </tr>
              ))}

            {tableData.length === 0 && (
              <tr className="text-center text-text-secondary">
                <td className="p-2" colSpan={totalColumns}>
                  Entities not found!
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      {/* Pagenation part */}
      <div className="flex justify-between items-center">
        <p className="px-6 py-4 text-[12px] font-medium text-text-secondary">
          Showing <span>{pageNumber}</span> to <span>{totalPages}</span> of{" "}
          <span>{totalEntries}</span> entities
        </p>
        <div>
          <button
            onClick={handlePrevious}
            className={`main-button mr-2 ${!hasPreviousPage && "cursor-not-allowed"}`}
          >
            <ArrowLeft />
          </button>
          <button
            onClick={handleNextPage}
            className={`main-button ${!hasNextPage && "cursor-not-allowed"}`}
          >
            <ArrowRight />
          </button>
        </div>
      </div>
    </div>
  );
};

export default MainTable;
