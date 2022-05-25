import { AxiosRequestConfig } from "axios"
import { getAccessToken } from "../../../services/TokenService"

export default function authTokenInterceptor(config: AxiosRequestConfig) {
  const token = getAccessToken()

  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
}
