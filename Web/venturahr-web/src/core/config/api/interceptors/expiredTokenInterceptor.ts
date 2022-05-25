import { AxiosError } from "axios"
import { hasAccessToken } from "../../../services/TokenService"
import { refresh } from "../../../services/AuthService"

export default async function expiredTokenInterceptor(error: AxiosError) {
  if (error.response?.status === 401 && hasAccessToken()) {
    return await refresh()
  }
  return Promise.reject(error)
}
