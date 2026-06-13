import { LayoutDashboard, LibraryBig } from "lucide-react";

export const SIDEBAR_ITEMS = [
  { path: "/", label: "Dashboard", icons: LayoutDashboard, onlyAdmin: false },
  {
    path: "/bookManagement",
    label: "Book Management",
    icons: LibraryBig,
    onlyAdmin: false,
  },
] as const;
