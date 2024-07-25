import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { ReactQueryDevtools } from "@tanstack/react-query-devtools"
import ReactDOM from "react-dom/client"
import { BrowserRouter } from "react-router-dom"
import { Toaster } from "sonner"

import App from "./App.tsx"
import AuthProvider from "./contexts/auth-provider.tsx"
import { SyllabusDetailProvider } from "./contexts/syllabus-detail-provider.tsx"
import "./index.css"
import { AxiosInterceptor } from "./lib/fams-api.ts"

const queryClient = new QueryClient()

ReactDOM.createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AuthProvider>
      <SyllabusDetailProvider>
        <AxiosInterceptor>
          <QueryClientProvider client={queryClient}>
            <ReactQueryDevtools initialIsOpen={false} />
            <Toaster richColors />
            <App />
          </QueryClientProvider>
        </AxiosInterceptor>
      </SyllabusDetailProvider>
    </AuthProvider>
  </BrowserRouter>
)
