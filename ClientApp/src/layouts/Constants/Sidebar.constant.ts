import { LayoutDashboard, LibraryBig, Warehouse } from "lucide-react";

export const SIDEBAR_ITEMS = [
  { path: "/", label: "Dashboard", icons: LayoutDashboard, onlyAdmin: false },
  {
    path: "/bookManagement",
    label: "Book Management",
    icons: LibraryBig,
    onlyAdmin: false,
    },
  {
    path: "/bookInventory",
    label: "Book Inventory",
    icons: Warehouse,
    onlyAdmin: false,
  },
] as const;
