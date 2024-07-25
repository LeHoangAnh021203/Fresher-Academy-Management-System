import { useEffect } from "react"

import { useAuthContext } from "@/contexts/auth-provider"
import axios from "axios"

import { IRefreshTokenResponse } from "@/types/auth.interface"

const famsAPI = axios.create({
  baseURL: "http://localhost:5141",
  timeout: 3000,
  headers: {
    "Content-Type": "application/json"
  }
})

function AxiosInterceptor({ children }: { children: React.ReactNode }) {
  const {
    accessToken,
    refreshToken,
    expiredAt,
    setAccessToken,
    setRefreshToken,
    setExpiredAt
  } = useAuthContext()

  useEffect(() => {
    const requestInterceptor = famsAPI.interceptors.request.use(
      function (config) {
        const token = accessToken
        if (token) {
          config.headers.Authorization = `Bearer ${token}`
        }
        return config
      },
      function (error) {
        return Promise.reject(error)
      }
    )

    const responseInterceptor = famsAPI.interceptors.response.use(
      function (response) {
        return response
      },
      async function (error) {
        const originalRequest = error.config
        if (error.response.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true
          const { data }: { data: IRefreshTokenResponse } = await famsAPI.post(
            "/Authorize/RefreshAccessToken",
            {
              accessTokenToken: accessToken,
              expiredAt: expiredAt,
              refreshToken: refreshToken
            }
          )
          setRefreshToken(data.data.refreshToken, data.data.expiredAt)
          setAccessToken(data.data.accessTokenToken, data.data.expiredAt)
          setExpiredAt(data.data.expiredAt, data.data.expiredAt)
          originalRequest.headers.Authorization = `Bearer ${data.data.accessTokenToken}`
          originalRequest._retry = false
          return famsAPI(originalRequest)
        }
        return Promise.reject(error)
      }
    )

    return () => {
      famsAPI.interceptors.request.eject(requestInterceptor)
      famsAPI.interceptors.response.eject(responseInterceptor)
    }
  }, [
    accessToken,
    refreshToken,
    expiredAt,
    setAccessToken,
    setRefreshToken,
    setExpiredAt
  ])

  return children
}

export default famsAPI
export { AxiosInterceptor }
