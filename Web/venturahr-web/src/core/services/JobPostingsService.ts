import jobPostingsApi from "../config/api/jobpostings"
import { CreateJobRequest } from "../dtos/requests/CreateJobRequest"

const api = jobPostingsApi
const endpoint = "/job-postings"

export async function postJob(model: CreateJobRequest) {
  return await api.post(endpoint, model)
}
