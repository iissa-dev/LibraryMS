import { defineConfig } from "vite";
import tailwindcss from "@tailwindcss/vite";
import react from "@vitejs/plugin-react";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  build: {
    outDir: "../LibraryMS.Api/wwwroot",
    emptyOutDir: false,
  },
  server: {
    // allowedHosts: true,
    host: true,
    proxy: {
      "/api": {
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
