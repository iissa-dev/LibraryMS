import { ArrowLeft, ArrowRight, type LucideProps } from "lucide-react";
import type { ForwardRefExoticComponent, RefAttributes } from "react";

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
      return {
        style: "bg-green/30 rounded-2xl px-2 py-1 text-green text-[12px]",
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
          <thead>
            <tr>
              {tableHeader &&
                tableHeader.map((h, i) => {
                  if (hanleShowId(h)) return null;

                  return (
                    <th
                      key={i}
                      className="px-6 py-4 text-xs font-bold uppercase tracking-widest bg-border"
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
          <tbody className="border-t-transparent border-border border">
            {tableData &&
              tableData.map((row, i) => (
                <tr key={i} className="text-center border-b border-border">
                  {Object.entries(row).map(([key, cell], j) => {
                    if (hanleShowId(key)) return null;
                    const status = getStatusStyle(String(cell));

                    return (
                      <td key={j} className="px-6 py-4 text-[12px] font-medium">
                        {key.toLowerCase().includes("status") ? (
                          <span className={`${status?.style}`}>
                            {status?.label}
                          </span>
                        ) : (
                          <span>{cell}</span>
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
                  Copies not found!
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
          <span>{totalEntries}</span> copies
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
