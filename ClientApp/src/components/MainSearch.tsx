import { Search } from "lucide-react";

type Params = {
  search: string;
  setSearch: (value: string) => void;
  placeholder: string;
};

const MainSearch = ({ search, setSearch, placeholder }: Params) => {
  return (
    <div>
      <form
        className="md:w-75 flex items-center"
        onSubmit={(e) => e.preventDefault()}
      >
        <div className="relative w-full flex items-center">
          <Search className="absolute left-3 text-neutral w-5 h-5 pointer-events-none" />
          <input
            className="search-input pl-10"
            type="text"
            id="search"
            placeholder={placeholder}
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
        </div>
      </form>
    </div>
  );
};

export default MainSearch;
