import { useEffect } from "react"

import { useAuthContext } from "@/contexts/auth-provider"
import { Navigate, Outlet, useLocation, useNavigate } from "react-router-dom"

import { IUserRole } from "@/types/user.interface"

import Footer from "../organisms/Footer"
import Header from "../organisms/Header"
import SideBar from "../organisms/SideBar"

const RootLayout = () => {
  const { role, user, refreshToken } = useAuthContext()
  const navigate = useNavigate()
  const location = useLocation()

  useEffect(() => {
    const allowedRoles = [
      IUserRole.SuperAdmin,
      IUserRole.ClassAdmin,
      IUserRole.Trainer
    ]

    if (!refreshToken || !role || !user || !allowedRoles.includes(role)) {
      navigate("/login")
    }

    if (
      location.pathname === "/user-permissions" &&
      role !== IUserRole.SuperAdmin
    ) {
      navigate("/")
    }
  }, [refreshToken, role, user, navigate, location.pathname])

  return refreshToken ? (
    <div className="h-screen">
      <Header />
      <div className="flex min-h-full">
        <SideBar />
        <Outlet />
      </div>
      <Footer />
    </div>
  ) : (
    <Navigate to="/login" />
  )
}

export default RootLayout
