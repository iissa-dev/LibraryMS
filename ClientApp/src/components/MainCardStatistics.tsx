import type { LucideProps } from "lucide-react";

export type StatisticsParams = {
  title: string;
  StatisticsValue: number;
  Description: string;
  Color: string;
  Icon: React.ForwardRefExoticComponent<
    Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>
  >;
};

export const MainCardStatistics = ({
  title,
  StatisticsValue,
  Description,
  Color,
  Icon,
}: StatisticsParams) => {
  const style = `${Color === "primary" ? "bg-primary/20" : Color === "green" ? "bg-green/20" : "bg-red/20"} p-3 rounded-md flex items-center justify-center text-${Color}`;
  return (
    <div className="main-card flex md:items-center gap-4 justify-between min-w-50">
      {/* info */}
      <div>
        <p className="text-text-secondary font-semibold capitalize mb-1 text-sm">
          {title}
        </p>
        <p className="text-primary text-5xl font-bold mb-1 font-sans">
          {StatisticsValue}
        </p>
        <p className={`text-sm text-${Color}`}>{Description}</p>
      </div>

      {/* icon */}
      <div className={`${style}`}>
        <Icon size={32} />
      </div>
    </div>
  );
};

export default MainCardStatistics;
