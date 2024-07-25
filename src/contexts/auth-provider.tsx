import React, { useContext, useEffect, useState } from "react"

import { AxiosResponse } from "axios"
import { useCookies } from "react-cookie"

import { IUserRoleConfigItem } from "@/types/user-permission.interface"
import { IJwtPayload, IUserRole } from "@/types/user.interface"

import famsAPI from "@/lib/fams-api"
import { jwtDecode } from "@/lib/utils"

type AuthProviderProps = {
  children: React.ReactNode
}

type AuthContextType = {
  user: IJwtPayload | null
  accessToken: string | null
  refreshToken: string | null
  role: IUserRole | null
  expiredAt: Date | null
  permissions: IUserRoleConfigItem | null
  setAccessToken: (value: string, expiresDate: Date) => void
  setRefreshToken: (value: string, expiresDate: Date) => void
  setRole: (value: IUserRole | null) => void
  setUser: (value: IJwtPayload | null) => void
  setExpiredAt: (value: Date, expiresDate: Date) => void
  setPermissions: (value: IUserRoleConfigItem | null) => void
  logout: () => void
}

export const AuthContext = React.createContext<AuthContextType | null>(null)

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<IJwtPayload | null>(null)
  const [cookies, setCookies, removeCookies] = useCookies([
    "access_token",
    "refresh_token",
    "expired_at"
  ])
  const [role, setRole] = useState<IUserRole | null>(null)
  const [permissions, setPermissions] = useState<IUserRoleConfigItem | null>(
    null
  )

  const setAccessToken = (value: string, expiresDate: Date) => {
    setCookies("access_token", value, {
      path: "/",
      expires: new Date(expiresDate)
    })
  }

  const setRefreshToken = (value: string, expiresDate: Date) => {
    setCookies("refresh_token", value, {
      path: "/",
      expires: new Date(expiresDate)
    })
  }

  const setExpiredAt = (value: Date, expiresDate: Date) => {
    setCookies("expired_at", value, {
      path: "/",
      expires: new Date(expiresDate)
    })
  }

  const getPermisions = (role: IUserRole) => {
    famsAPI
      .get("/UserPermission/GetAllPermission")
      .then((res: AxiosResponse<IUserRoleConfigItem[]>) => {
        const foundPermission = res.data.find(
          (item) => item.permissionId === role
        )
        if (foundPermission) setPermissions(foundPermission)
      })
  }

  const logout = async () => {
    try {
      await famsAPI.post("/Authorize/Logout")
    } catch (error) {
      console.error("Logout failed:", error)
    } finally {
      // Clear cookies
      removeCookies("access_token")
      removeCookies("refresh_token")
      removeCookies("expired_at")
      // Clear user and role state
      setUser(null)
      setRole(null)
    }
  }

  useEffect(() => {
    const checkUser = async () => {
      if (!cookies.refresh_token) {
        setUser(null)
        setRole(null)
        return
      }

      const decodedToken = jwtDecode(cookies.access_token)
      setUser(decodedToken)
      setRole(Number(decodedToken.Role as IUserRole))
      getPermisions(+decodedToken.Role)
    }
    checkUser()
  }, [cookies.refresh_token, cookies.access_token])
  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        accessToken: cookies.access_token,
        setAccessToken,
        refreshToken: cookies.refresh_token,
        setRefreshToken,
        permissions,
        setPermissions,
        role,
        setRole,
        logout,
        expiredAt: cookies.expired_at,
        setExpiredAt
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuthContext = (): AuthContextType => {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error("useAuthContext must be used within an AuthProvider")
  }
  return context
}
