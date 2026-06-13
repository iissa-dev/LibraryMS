import { Bell } from "lucide-react";
import MainSearch from "../components/MainSearch";

type Params = {
  search: string;
  setSearch: (value: string) => void;
  placeholder: string;
  userImageUrl?: string;
  userName?: string;
};

const Header = ({
  search,
  setSearch,
  placeholder,
  userImageUrl,
  userName,
}: Params) => {
  return (
    <header className="h-18 flex items-center justify-between p-5 border-b border-border">
      {/* Search bar  */}
      <div>
        <MainSearch
          search={search}
          setSearch={setSearch}
          placeholder={placeholder}
        />
      </div>

      {/* Notification and Profile */}
      <div className="flex gap-4 items-center">
        <div className="p-2 hover:bg-bgLight rounded-full transition-colors relative">
          <Bell className="cursor-pointer" />
          <span className="absolute top-1 right-2 w-2.5 h-2.5 bg-red-500 rounded-full border-2 border-white"></span>
        </div>
        {/* current user image */}
        <div>
          {userImageUrl ? (
            <img
              src={userImageUrl}
              alt={`${userName}'s profile`}
              className="w-10 h-10 rounded-full object-cover border-2 border-primary"
            />
          ) : (
            <div
              className="w-10 h-10 rounded-full bg-primary text-white
              flex items-center justify-center font-bold font-sans text-sm"
            >
              {userName?.charAt(0).toUpperCase()}
            </div>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
