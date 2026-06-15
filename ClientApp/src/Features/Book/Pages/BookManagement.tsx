import { useEffect, useState } from "react";
import MainPageTitle from "../../../components/MainPageTitle";
import { Plus, Trash2 } from "lucide-react";
import MainSearch from "../../../components/MainSearch";
import BookCardList from "../components/BookCardList";
import CardStatisticsList from "../../../components/CardStatisticsList";
import { useNavigate } from "react-router-dom";
import GenreList from "../../../components/GenreList";

const BookManagement = () => {
  const [showDeletedData, setShowDeletedData] = useState(false);
  const [filterByTitle, setFilterByTitle] = useState("");
  const [filterByGenre, setFilterByGenre] = useState<number | undefined>();
  const [pageNumber, setPageNumber] = useState(1);
  const navigate = useNavigate();
  const handleAddNewBook = () => {
    navigate("/book/new");
  };

  const handleGenreChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = e.target.value;
    setFilterByGenre(selectedValue === "" ? undefined : Number(selectedValue));
    setPageNumber(1);
  };

  useEffect(() => {
    setPageNumber(1);
  }, [filterByTitle, showDeletedData]); 
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
              search={filterByTitle}
              placeholder="Search books by title..."
              setSearch={setFilterByTitle}
            />
            {/* filter */}
            <GenreList
              value={filterByGenre ?? ""}
              onChange={handleGenreChange}
            />
            <div className="flex">
              <input
                type="checkbox"
                id="show-deleted-data"
                className="hidden"
                onChange={() => setShowDeletedData(!showDeletedData)}
              />
              <label
                htmlFor="show-deleted-data"
                title="Deleted Books"
                className="cursor-pointer select-none transition-colors duration-200"
              >
                <Trash2
                  className={showDeletedData ? "text-red" : "text-neutral"}
                />
              </label>
            </div>
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
        <div>
          <BookCardList
            searchByTitle={filterByTitle}
            searchByGenre={filterByGenre}
            deletedData={showDeletedData}
            pageNumber={pageNumber}
            setPageNumber={setPageNumber}
          />
        </div>
      </main>
    </div>
  );
};

export default BookManagement;
