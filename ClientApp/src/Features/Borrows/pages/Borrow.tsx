import MainPageTitle from "../../../components/MainPageTitle";
import BorrowTable from "./BorrowTable";

const Borrow = () => {
  return (
    <div className="flex flex-col">
      <MainPageTitle
        Title={"Active Book Loans"}
        Description={"Manage and menitor all currently checked-out valumes."}
      />

      <div className="mt-6">
        <BorrowTable />
      </div>
    </div>
  );
};

export default Borrow;
