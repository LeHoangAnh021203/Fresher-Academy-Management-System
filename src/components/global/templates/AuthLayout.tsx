import { useAuthContext } from "@/contexts/auth-provider"
import { Navigate, Outlet } from "react-router-dom"

const AuthLayout = () => {
  const { refreshToken, user } = useAuthContext()

  return !refreshToken || !user ? (
    <section className="h-screen w-screen">
      <Outlet />
    </section>
  ) : (
    <Navigate to="/" />
  )
}

export default AuthLayout
