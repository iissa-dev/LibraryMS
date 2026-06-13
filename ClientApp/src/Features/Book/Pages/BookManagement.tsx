import { useState } from "react";
import MainPageTitle from "../../../components/MainPageTitle";
import { Plus } from "lucide-react";
import MainSearch from "../../../components/MainSearch";
import BookCardList from "../components/BookCardList";
import CardStatisticsList from "../../../components/CardStatisticsList";
import { useNavigate } from "react-router-dom";
import GenreList from "../../../components/GenreList";

const BookManagement = () => {
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const handleAddNewBook = () => {
    navigate("/book/new");
  };
  return (
    <div>
      {/* Top of page */}
      {/* Title */}
      <MainPageTitle
        Title="Books Management"
        Description="Organize and manage the library's physical and digital collection."
      />
      <div className="mt-6">
        <CardStatisticsList />
      </div>
      {/* Main content */}
      <main>
        {/* search & filter */}
        <div className="main-card flex mb-4 flex-col md:flex-row">
          <div className="flex md:items-center gap-4 flex-1 flex-col md:flex-row">
            <MainSearch
              search={search}
              placeholder="Search books by title or author..."
              setSearch={setSearch}
            />
            {/* filter */}
            <GenreList />
          </div>
          <button
            onClick={handleAddNewBook}
            className="flex gap-1 items-center main-button mt-4 md:mt-0 w-fit"
            type="button"
          >
            <Plus size={20} />
            Add new book
          </button>
        </div>
        {/* Books Card */}
        <div className="mb-6 border-b border-border">
          <BookCardList pageNumber={1} pageSize={10} />
        </div>

        {/* Pagenations */}
        <div className="flex justify-between items-center flex-col md:flex-row">
          <div className="text-[12px] text-text-secondary mb-4 md:mb-0">
            Showing 1 to 5 of 12,400 books
          </div>
          <div className="flex gap-4 items-center">
            <input className="main-button" type="button" value={"Previous"} />
            <input
              className="bg-text-secondary text-white md:py-1.5 md:px-4 p-1 rounded-md cursor-pointer"
              type="button"
              value={"1"}
            />
            <input
              className="bg-text-secondary text-white md:py-1.5 md:px-4 p-1 rounded-md cursor-pointer"
              type="button"
              value={"2"}
            />
            <input
              className="bg-text-secondary text-white md:py-1.5 md:px-4 p-1 rounded-md cursor-pointer"
              type="button"
              value={"3"}
            />
            <span>...</span>
            <input
              className="bg-text-secondary text-white md:py-1.5 md:px-4 p-1 rounded-md cursor-pointer"
              type="button"
              value={"25"}
            />
            <input className="main-button" type="button" value={"Next"} />
          </div>
        </div>
      </main>
    </div>
  );
};

export default BookManagement;
