import { Route, Routes } from "react-router-dom";
import Dashboard from "./Features/Dashboard/Pages/Dashboard";
import Header from "./layouts/Header";
import Sidebar from "./layouts/Sidebar";
import BookManagement from "./Features/Book/Pages/BookManagement";
import NotFoundPage from "./Util/NotFoundPage";
import { useState } from "react";
import Footer from "./layouts/Footer";
import BookFromPage from "./Features/Book/Pages/BookFromPage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";

const queryClient = new QueryClient();

function App() {
  const [search, setSearch] = useState("");
  return (
    <QueryClientProvider client={queryClient}>
      <div className="min-h-screen sm:flex bg-background">
        <Toaster position="bottom-right" />
        <Sidebar />

        <div className="flex flex-col flex-1">
          <Header
            search={search}
            setSearch={(value) => setSearch(value)}
            placeholder={"Search in Dashborad"}
            userName="Issa"
          />

          <main className="flex-1 p-4 md:p-6 mb-10 md:mb-0">
            <Routes>
              <Route path="/" element={<Dashboard />} />
              <Route path="/bookManagement" element={<BookManagement />} />
              <Route path="/book/new" element={<BookFromPage />} />
              <Route
                path="/bookManagement/view/:bookId"
                element={<BookFromPage readOnly={true} />}
              />
              <Route
                path="/bookManagement/edit/:bookId"
                element={<BookFromPage />}
              />
              <Route path="*" element={<NotFoundPage />} />
            </Routes>
            <Footer />
          </main>
        </div>
      </div>
    </QueryClientProvider>
  );
}

export default App;
