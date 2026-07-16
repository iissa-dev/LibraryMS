import MainPageTitle from "../../../components/MainPageTitle";
import BookCopyTable from "./BookCopyTable";

const BookCopy = () => {
  return (
    <div className="flex flex-col">
      <MainPageTitle
        Title={"Book Inventory Copies"}
        Description={
          "Managing 2,481 individual physical copies across the campus network."
        }
      />

      <div className="mt-6 ">
        <BookCopyTable />
      </div>
    </div>
  );
};

export default BookCopy;
