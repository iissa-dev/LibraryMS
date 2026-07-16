import { Route, Routes } from "react-router-dom";
import Dashboard from "./Features/Dashboard/Pages/Dashboard";
import BookManagement from "./Features/Book/Pages/BookManagement";
import NotFoundPage from "./Util/NotFoundPage";
import BookFormPage from "./Features/Book/Pages/BookFromPage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";
import Login from "./Features/Account/Pages/Login";
import ProjectLayout from "./layouts/ProjectLayout";
import { AuthProvider } from "./context/AuthContext";
import Register from "./Features/Account/Pages/Register";
import BookCopy from "./Features/BookCopy/pages/BookCopy";
import PrivateRoute from "./Util/PrivateRoute";
import Borrow from "./Features/Borrows/pages/Borrow";
import DarkModeButton from "./components/DarkModeButton";
import Members from "./Features/Client/pages/Members";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Toaster
        position="bottom-right"
        containerStyle={{ backgroundColor: "" }}
        toastOptions={{
          style: {
            backgroundColor: "var(--color-background)",
            color: "var(--color-text)",
          },
        }}
      />
      <div className="hidden">
        <DarkModeButton />
      </div>
      <AuthProvider>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/registery" element={<Register />} />

          <Route element={<ProjectLayout />}>
            <Route
              path="/"
              element={
                <PrivateRoute>
                  <Dashboard />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookManagement"
              element={
                <PrivateRoute allowdRoles={["admin", "employee", "client"]}>
                  <BookManagement />
                </PrivateRoute>
              }
            />
            <Route
              path="/book/new"
              element={
                <PrivateRoute>
                  <BookFormPage />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookManagement/view/:bookId"
              element={
                <PrivateRoute>
                  <BookFormPage readOnly={true} />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookManagement/edit/:bookId"
              element={
                <PrivateRoute allowdRoles={["admin", "employee"]}>
                  <BookFormPage />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookInventory"
              element={
                <PrivateRoute allowdRoles={["admin", "employee"]}>
                  <BookCopy />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookInventory/view/:bookId"
              element={
                <PrivateRoute allowdRoles={["admin", "employee"]}>
                  <BookCopy />
                </PrivateRoute>
              }
            />
            <Route
              path="/loans"
              element={
                <PrivateRoute>
                  <Borrow />
                </PrivateRoute>
              }
            />
            <Route
              path="member"
              element={
                <PrivateRoute>
                  <Members />
                </PrivateRoute>
              }
            />
          </Route>

          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
