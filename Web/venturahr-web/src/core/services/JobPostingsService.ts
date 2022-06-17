import jobPostingsApi from "../config/api/jobpostings"
import { CreateJobRequest } from "../dtos/requests/CreateJobRequest"
import FilterResponse from "../dtos/filters/FilterResponse"
import JobPosting from "../models/JobPosting"
import { JobPostingsFilter } from "../dtos/filters/JobPostingsFilter"

const api = jobPostingsApi
const endpoint = "/job-postings"

export async function postJob(model: CreateJobRequest) {
  return await api.post(endpoint, model)
}

export async function fetchJobPostings(params?: JobPostingsFilter) {
  const { data } = await api.get<FilterResponse<JobPosting>>(endpoint, {
    params,
  })

  return data
}
