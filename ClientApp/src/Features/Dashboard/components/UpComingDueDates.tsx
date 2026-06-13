const UPCOMING_LIST = [
  {
    date: "tomorrow",
    title: "To kill a mockingbird",
    memberName: "Issa atieh",
  },
  {
    date: "oct 24",
    title: "Brave new world",
    memberName: "Nowras al-damin",
  },
  {
    date: "oct 6",
    title: "The catcher in the rye",
    memberName: "Robert M.",
  },
];

const UpComingDueDates = () => {
  return (
    <div className="main-card md:col-span-1">
      {/* header */}
      <div className="flex justify-between items-center border-b py-4 border-border mb-2">
        <h3 className="text-primary font-bold text-xl">Upcoming Due Dates</h3>
      </div>

      {/* Upcoming Due Dates info */}
      <div>
        {/* box */}
        {UPCOMING_LIST.map((item, i) => (
          <div
            key={i}
            className="bg-neutral/20 flex flex-col mb-2 p-2 rounded-sm"
          >
            <p className="text-primary font-bold text-[12px] uppercase">
              {item.date}
            </p>
            <p className="capitalize">{item.title}</p>
            <p className="text-text-secondary text-[12px] text-right capitalize">
              Member: {item.memberName}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default UpComingDueDates;
