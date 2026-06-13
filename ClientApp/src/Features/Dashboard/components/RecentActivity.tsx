import { ArrowRightLeft, Handshake, UserPlus } from "lucide-react";

// temp
const RECENT_ACTIVITY_LIST = [
  {
    actionName: "Issa Atieh returned The Great Gatsby",
    icon: ArrowRightLeft,
    duration: "2 minutes ago",
  },
  {
    actionName: "David Chen registered as a new member",
    icon: UserPlus,
    duration: "45 minutes ago",
  },
  {
    actionName: "Elena Rodriguez borrowed 1984",
    icon: Handshake,
    duration: "23 minutes ago",
  },
];

const RecentActivity = () => {
  return (
    <div className="main-card md:col-span-2">
      {/* header */}
      <div className="flex justify-between items-center border-b py-4 border-border">
        <h3 className="text-primary font-bold text-xl">Recent Activity</h3>
        <span className="text-sm text-text-secondary font-bold">View All</span>
      </div>

      {/* recent activity info */}
      <div className="p-2">
        {/* box */}
        {RECENT_ACTIVITY_LIST.map((item, i) => (
          <div key={i} className="flex gap-4 items-center p-2">
            {/* icon */}
            <div className="bg-primary/30 text-primary p-1 rounded-sm">
              <item.icon />
            </div>
            {/* action */}
            <div>
              <p className="text-[16px] text-text-secondary">
                {item.actionName}
              </p>
              <span className="text-[12px] text-neutral block">
                {item.duration}
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RecentActivity;
