import axios, { AxiosRequestConfig } from "axios"
import { getToken } from "../core/services/auth.service"

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || "",
})

const authTokenInterceptor = (config: AxiosRequestConfig) => {
  const token = getToken()
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
}

api.interceptors.request.use(authTokenInterceptor)

export default api
