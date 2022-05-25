import axios from "axios"
import authTokenInterceptor from "./interceptors/authTokenInterceptor"
import expiredTokenInterceptor from "./interceptors/expiredTokenInterceptor"

const usersApi = axios.create({
  baseURL: process.env.NEXT_PUBLIC_USERS_API_URL,
})

usersApi.interceptors.request.use(authTokenInterceptor)
usersApi.interceptors.response.use(res => res, expiredTokenInterceptor)

export default usersApi
