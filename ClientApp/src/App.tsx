import { Route, Routes } from "react-router-dom";
import Dashboard from "./Features/Dashboard/Pages/Dashboard";
import BookManagement from "./Features/Book/Pages/BookManagement";
import NotFoundPage from "./Util/NotFoundPage";
import BookFromPage from "./Features/Book/Pages/BookFromPage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";
import Login from "./Features/Account/Pages/Login";
import ProjectLayout from "./layouts/ProjectLayout";
import { AuthProvider } from "./context/AuthContext";
import Register from "./Features/Account/Pages/Register";
import BookCopy from "./Features/BookCopy/pages/BookCopy";
import PrivateRoute from "./Util/PrivateRoute";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Toaster position="bottom-right" />

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
                  <BookFromPage />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookManagement/view/:bookId"
              element={
                <PrivateRoute>
                  <BookFromPage readOnly={true} />
                </PrivateRoute>
              }
            />
            <Route
              path="/bookManagement/edit/:bookId"
              element={
                <PrivateRoute allowdRoles={["admin", "employee"]}>
                  <BookFromPage />
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
          </Route>
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
