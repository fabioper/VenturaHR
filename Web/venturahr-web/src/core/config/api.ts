import axios, { AxiosError, AxiosRequestConfig } from "axios"
import {
  getAccessToken,
  hasAccessToken,
  refreshToken,
} from "../services/AuthService"

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
})

const authTokenInterceptor = (config: AxiosRequestConfig) => {
  const token = getAccessToken()

  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
}

async function expiredTokenInterceptor(error: AxiosError) {
  if (error.response?.status === 401 && hasAccessToken()) {
    return await refreshToken()
  }
  return Promise.reject(error)
}

api.interceptors.request.use(authTokenInterceptor)
api.interceptors.response.use(res => res, expiredTokenInterceptor)

export default api
