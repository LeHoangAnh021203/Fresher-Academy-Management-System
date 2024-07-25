export interface IAuthResponseData {
  accessTokenToken: string
  expiredAt: Date
  refreshToken: string
}

export interface IRefreshTokenResponse {
  success: boolean
  message: string
  data: {
    accessTokenToken: string
    refreshToken: string
    expiredAt: Date
  }
}
