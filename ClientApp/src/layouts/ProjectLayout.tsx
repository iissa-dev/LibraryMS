import Footer from "./Footer";
import Header from "./Header";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar";

const ProjectLayout = () => {
  const [search, setSearch] = useState("");

  return (
    <div className="min-h-screen sm:flex bg-background">
      <Sidebar />

      <div className="flex flex-col flex-1 overflow-hidden">
        <Header
          search={search}
          setSearch={(value) => setSearch(value)}
          placeholder={"Search in Dashborad"}
          userName="Issa"
        />

        <main className="flex-1 p-4 md:p-6 mb-10 md:mb-0">
          <Outlet />
          <Footer />
        </main>
      </div>
    </div>
  );
};

export default ProjectLayout;
