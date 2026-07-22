import {
  Handshake,
  LayoutDashboard,
  LibraryBig,
  Users,
  Warehouse,
} from "lucide-react";

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
    onlyAdmin: true,
  },
  {
    path: "/loans",
    label: "Loans",
    icons: Handshake,
    onlyAdmin: true,
  },
  {
    path: "/member",
    label: "Members",
    icons: Users,
    onlyAdmin: false,
  },
] as const;
