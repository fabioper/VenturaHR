import jobPostingsApi from "../config/api/jobpostings"
import { PostJobModel } from "../dtos/jobposting/PostJobModel"

const api = jobPostingsApi
const endpoint = "/job-postings"

export async function postJob(model: PostJobModel) {
  return await api.post(endpoint, model)
}
