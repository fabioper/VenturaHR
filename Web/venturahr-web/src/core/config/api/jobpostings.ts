import axios from "axios"
import authTokenInterceptor from "./interceptors/authTokenInterceptor"
import expiredTokenInterceptor from "./interceptors/expiredTokenInterceptor"

const jobPostingsApi = axios.create({
  baseURL: process.env.NEXT_PUBLIC_JOB_POSTINGS_API_URL,
})

jobPostingsApi.interceptors.request.use(authTokenInterceptor)
jobPostingsApi.interceptors.response.use(res => res, expiredTokenInterceptor)

export default jobPostingsApi
