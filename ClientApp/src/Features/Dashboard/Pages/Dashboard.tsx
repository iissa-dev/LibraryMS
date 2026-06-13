import CardStatisticsList from "../../../components/CardStatisticsList";
import MainPageTitle from "../../../components/MainPageTitle";
import RecentActivity from "../components/RecentActivity";
import SystemHealth from "../components/SystemHealth";
import UpComingDueDates from "../components/UpComingDueDates";

const Dashboard = () => {
  return (
    <div>
      <MainPageTitle
        Title="Dashboard"
        Description="Organize and manage the library's Statistics"
      />
      <div className="mt-6">
        <CardStatisticsList />
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <RecentActivity />
          <UpComingDueDates />
        </div>
        <SystemHealth />
      </div>
    </div>
  );
};

export default Dashboard;
