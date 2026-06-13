import { Book, BookDashed } from "lucide-react";
import { useState } from "react";
import { MainCardStatistics } from "./MainCardStatistics";

const CardStatisticsList = () => {
  // Will send data as params
  // Temp
  const [mainStatistics] = useState([
    {
      id: "total-books",
      title: "Total books",
      StatisticsValue: 12.45,
      Description: "+12% from last month",
      color: "primary",
      icon: Book,
    },
    {
      id: "total-books-2",
      title: "total Books 2",
      StatisticsValue: 12.45,
      Description: "+12% from last month",
      color: "green",
      icon: BookDashed,
    },
    {
      id: "total-books-3",
      title: "total Books 3",
      StatisticsValue: 12.45,
      Description: "+12% from last month",
      color: "red",
      icon: BookDashed,
    },
  ]);

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
      {mainStatistics &&
        mainStatistics.map((item) => (
          <MainCardStatistics
            key={item.id}
            title={item.title}
            Description={item.Description}
            StatisticsValue={item.StatisticsValue}
            Color={item.color}
            Icon={item.icon}
          />
        ))}
    </div>
  );
};

export default CardStatisticsList;
