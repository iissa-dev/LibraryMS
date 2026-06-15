import { Search } from "lucide-react";
import { useState } from "react";

type Params = {
  search: string;
  setSearch: (value: string) => void;
  placeholder: string;
};

const MainSearch = ({ search, setSearch, placeholder }: Params) => {
  const [searchLocal, setSearchLocal] = useState(search);

  const triggerSearch = () => {
    setSearch(searchLocal);
  };

  const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    triggerSearch();
  };

  const handleEmptyFilter = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = e.target.value;

    setSearchLocal(newValue);

    if (newValue === "") {
      setSearch("");
    }
  };

  return (
    <div>
      <form className="md:w-75 flex items-center" onSubmit={handleSubmit}>
        <div className="relative w-full flex items-center">
          <Search className="absolute left-3 text-neutral w-5 h-5 pointer-events-none" />
          <input
            className="search-input pl-10"
            type="text"
            id="search"
            placeholder={placeholder}
            value={searchLocal}
            onChange={handleEmptyFilter}
          />
        </div>
        <button type="submit" className="main-button ml-4">
          <Search className="left-3 text-white w-5 h-5 pointer-events-none" />
        </button>
      </form>
    </div>
  );
};

export default MainSearch;
