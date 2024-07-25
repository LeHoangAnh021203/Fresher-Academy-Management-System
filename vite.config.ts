/// <reference types="vitest" />
/// <reference types="vite/client" />
import react from "@vitejs/plugin-react"
import path from "path"
import { defineConfig } from "vite"

export default defineConfig({
  plugins: [react()],
  server: { port: 4000 },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src")
    }
  },
  test: {
    globals: true,
    environment: "jsdom",
    setupFiles: ["./src/setupTest.ts", "jest-webextension-mock"],
    coverage: {
      reportOnFailure: true,
      provider: "v8",
      exclude: [
        "src/graphql/**",
        "src/lib/**",
        "src/pages/**",
        "src/constants/**",
        "src/contexts/**",
        "src/components/global/atoms/**",
        "src/components/global/animation/**",
        "src/components/global/molecules/data-table/**",
        "src/components/local/Class/data-table/**",
        "src/components/local/Syllabus/data-table/**",
        "src/components/local/Training-Program/data-table/**",
        "src/components/local/User/data-table/**"
      ],
      reportsDirectory: "./src/__tests__/coverage"
    },
    css: true
  }
})
