import { Link, useLocation } from "react-router-dom";
import { SIDEBAR_ITEMS } from "./Constants/Sidebar.constant";
import { useState } from "react";
import { MoreHorizontal } from "lucide-react";
import useClickOutside from "../hooks/useClickOutside";

const Sidebar = () => {
  const { pathname } = useLocation();
  const [showMoreMenu, setShowMoreMenu] = useState(false);
  const menuRef = useClickOutside(() => setShowMoreMenu(false));

  const maxMobileVisibleItems = 3;
  const visibleMobileItems = SIDEBAR_ITEMS.slice(0, maxMobileVisibleItems);
  const hiddenMobileItems = SIDEBAR_ITEMS.slice(maxMobileVisibleItems);

  const baseItemStyle = `p-2 sm:p-4 transition-all duration-300 hover:bg-white/10 hover:text-white font-medium text-xs sm:text-sm flex flex-col sm:flex-row items-center justify-center lg:justify-start gap-0.5 sm:gap-2 rounded-md flex-1 sm:flex-none`;
  const activeItemStyle = `font-sans font-semibold text-white bg-white/10 tracking-wide border-b-2 sm:border-b-0 sm:border-r-2 border-primary`;
  const inactiveItemStyle = `bg-none text-white/40`;

  return (
    <div
      className="bg-neutral/95 text-white p-2 sm:p-4 z-50 transition-all duration-300
                    fixed bottom-0 left-0 right-0 w-full h-auto flex flex-row justify-around items-center border-t border-border/10
                    sm:sticky sm:top-0 sm:h-screen sm:w-20 sm:flex-col sm:justify-start sm:items-stretch sm:border-t-0 sm:border-r
                    lg:w-62.5"
    >
      <div className="mb-6 hidden lg:block">
        <h2 className="text-white font-bold text-xl">Librarian Portal</h2>
        <p className="text-[12px] text-white/40">Central Library Admin</p>
      </div>

      {/* Navigations */}
      <nav className="w-full relative" ref={menuRef}>
        {/* large screens */}
        <ul className="hidden sm:flex flex-col w-full gap-2">
          {SIDEBAR_ITEMS.map((item, i) => {
            const isActive = pathname === item.path;
            const Icon = item.icons;
            return (
              <li
                key={i}
                className={`${baseItemStyle} ${isActive ? activeItemStyle : inactiveItemStyle} font-sans`}
              >
                <Link
                  className="flex flex-col lg:flex-row items-center justify-center lg:justify-start gap-2 w-full h-full"
                  to={item.path}
                >
                  <Icon
                    size={20}
                    className={isActive ? "text-white" : "text-white/40"}
                  />
                  <span className="hidden lg:block">{item.label}</span>
                </Link>
              </li>
            );
          })}
        </ul>

        {/* small */}
        <ul className="flex sm:hidden flex-row justify-around items-center w-full gap-1">
          {visibleMobileItems.map((item, i) => {
            const isActive = pathname === item.path;
            const Icon = item.icons;
            return (
              <li
                key={i}
                className={`${baseItemStyle} ${isActive ? activeItemStyle : inactiveItemStyle} font-sans`}
              >
                <Link
                  className="flex flex-col items-center justify-center w-full h-full"
                  to={item.path}
                >
                  <Icon
                    size={18}
                    className={isActive ? "text-white" : "text-white/40"}
                  />
                </Link>
              </li>
            );
          })}

          {/* show more menue */}
          {hiddenMobileItems.length > 0 && (
            <li className="relative flex-1 flex justify-center">
              <button
                onClick={() => setShowMoreMenu(!showMoreMenu)}
                className={`p-2 w-full flex flex-col items-center justify-center rounded-md text-white/40 hover:text-white hover:bg-white/10 transition-all ${showMoreMenu ? "text-white bg-white/10" : ""}`}
              >
                <MoreHorizontal size={18} />
              </button>

              {/* Show more menue logic */}
              {showMoreMenu && (
                <div className="absolute bottom-14 right-2 bg-neutral border border-border/20 rounded-md shadow-xl p-2 w-48 flex flex-col gap-1 animate-in fade-in slide-in-from-bottom-2 duration-200">
                  {hiddenMobileItems.map((item, i) => {
                    const isActive = pathname === item.path;
                    const Icon = item.icons;
                    return (
                      <Link
                        key={i}
                        to={item.path}
                        onClick={() => setShowMoreMenu(false)}
                        className={`flex items-center gap-3 p-3 rounded-md text-sm font-medium transition-all ${isActive ? "bg-white/10 text-white font-semibold" : "text-white/60 hover:bg-white/5 hover:text-white"}`}
                      >
                        <Icon
                          size={18}
                          className={isActive ? "text-white" : "text-white/40"}
                        />
                        <span>{item.label}</span>
                      </Link>
                    );
                  })}
                </div>
              )}
            </li>
          )}
        </ul>
      </nav>
    </div>
  );
};

export default Sidebar;
