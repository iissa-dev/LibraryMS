import { Bell, LogOut } from "lucide-react";
import MainSearch from "../components/MainSearch";
import { useState } from "react";
import useClickOutside from "../hooks/useClickOutside";
import { useAuth } from "../hooks/useAuth";
import DarkModeButton from "../components/DarkModeButton";

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
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const mouseRef = useClickOutside(() => setIsMenuOpen(false));

  const { logout } = useAuth();

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
      <div ref={mouseRef} className="flex gap-4 items-center">
        <div className="p-2 hover:bg-bgLight rounded-full transition-colors relative">
          <Bell className="cursor-pointer text-text" />
          <span className="absolute top-1 right-2 w-2.5 h-2.5 bg-red rounded-full border-2 border-white"></span>
        </div>
        {/* current user image */}
        <div
          className="relative cursor-pointer select-none"
          onClick={() => {
            setIsMenuOpen((prev) => !prev);
          }}
        >
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
        <div className="hidden md:block">
          <DarkModeButton />
        </div>
        {isMenuOpen && (
          <div className="bg-background-secondary p-4 rounded-xl shadow-2xl absolute right-4 top-15 w-62.5 z-50">
            <ul>
              <li
                onClick={logout}
                className="text-text-secondary cursor-pointer flex gap-2 mb-4
              items-center transition-all duration-300 hover:bg-border/20 p-1 hover:text-primary rounded-md"
              >
                <LogOut size={15} /> <span>Logout</span>
              </li>
              <li className="flex items-center justify-between md:hidden border-t py-2 text-text-secondary/20 ">
                <span className="text-text-secondary ">Dark Mode</span>
                {<DarkModeButton />}
              </li>
            </ul>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
