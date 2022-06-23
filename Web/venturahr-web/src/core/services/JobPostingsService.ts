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

export async function fetchJobPosting(jobPostingId: string) {
  const { data } = await api.get<JobPosting>(endpoint + "/" + jobPostingId)
  return data
}

export async function renewJobPosting(
  jobPostingId: string,
  newExpiration: Date
) {
  return await api.put(endpoint + "/" + jobPostingId + "/renew", {
    newExpiration,
  })
}

export async function verifyIfUserCanApplyTo(jobPostingId: string) {
  const { data } = await api.get<boolean>(
    endpoint + "/" + jobPostingId + "/can-apply"
  )

  return data
}
