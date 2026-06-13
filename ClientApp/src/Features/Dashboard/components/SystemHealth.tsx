const SystemHealth = () => {
  return (
    <div className="mt-6 main-card">
      {/* header */}
      <div className="flex justify-between items-center border-b py-4 border-border mb-2">
        <h3 className="text-primary font-bold text-xl">
          System Health & Notices
        </h3>
      </div>
      {/* info */}
      <div className="flex md:items-center justify-between gap-2 md:flex-row flex-col">
        {/* Database connection */}
        <div className="flex justify-between items-center md:block">
          <div className="flex gap-2 items-center">
            <span className="w-2.5 h-2.5 bg-green rounded-full animate-pulse"></span>
            <p className="font-medium text-secondary">Database Connection</p>
          </div>
          <span className="block text-green text-[10px]">Online</span>
        </div>
        {/* Server Connection */}
        <div className="flex justify-between items-center md:block">
          <div className="flex gap-2 items-center">
            <span className="w-2.5 h-2.5 bg-green rounded-full animate-pulse"></span>
            <p className="font-medium text-secondary">Server Connection</p>
          </div>
          <span className="block text-green text-[10px]">Online</span>
        </div>
        {/* BackUp Date */}
        <div className="flex justify-between items-center md:block">
          <div className="flex gap-2 items-center">
            <span className="w-2.5 h-2.5 bg-green rounded-full animate-pulse"></span>
            <p className="font-medium text-secondary">Weekly BackUp</p>
          </div>
          <span className="block text-green text-[12px]">OCT 7 12:25 pm</span>
        </div>
      </div>
    </div>
  );
};

export default SystemHealth;
