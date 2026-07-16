import { Moon, Sun } from "lucide-react";
import { useState, useEffect } from "react";

const DarkModeButton = () => {
  const [isDarkMode, setIsDarkMode] = useState(() => {
    return localStorage.getItem("theme") === "dark";
  });

  useEffect(() => {
    const root = document.documentElement;
    if (isDarkMode) {
      root.classList.add("dark");
      localStorage.setItem("theme", "dark");
      window.dispatchEvent(new Event("theme-change"));
    } else {
      root.classList.remove("dark");
      localStorage.setItem("theme", "light");
      setIsDarkMode(false);
      window.dispatchEvent(new Event("theme-change"));
    }
  }, [isDarkMode]);

  useEffect(() => {
    const handleThemeChange = () => {
      setIsDarkMode(document.documentElement.classList.contains("dark"));
    };
    window.addEventListener("theme-change", handleThemeChange);
    return () => window.removeEventListener("theme-change", handleThemeChange);
  }, []);
  return (
    <div
      className="bg-primary/20 w-10 h-4 md:w-20 md:h-8 p-1 rounded-2xl relative cursor-pointer border border-primary/30 transition-colors duration-300 flex items-center"
      onClick={() => setIsDarkMode((prev) => !prev)}
    >
      <span
        className={`bg-primary w-3 h-3 md:w-6 md:h-6 rounded-full absolute top-1/2 flex items-center justify-center -translate-y-1/2 transition-all duration-500 text-white shadow-md
            ${isDarkMode ? "translate-x-5 md:translate-x-11 rotate-360 bg-amber-500" : "translate-x-0 rotate-0 bg-blue-600"}`}
      >
        {isDarkMode ? (
          <Moon size={14} className="transition-transform duration-500" />
        ) : (
          <Sun size={14} className="transition-transform duration-500" />
        )}
      </span>
    </div>
  );
};

export default DarkModeButton;
