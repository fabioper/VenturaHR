import jobPostingsApi from "../config/api/jobpostings"
import { JobApplication } from "../models/JobApplication"
import ApplicationsFilter from "../dtos/filters/ApplicationsFilter"
import FilterResponse from "../dtos/filters/FilterResponse"
import JobApplicationRequest from "../dtos/requests/JobApplicationRequest"

const api = jobPostingsApi
const endpoint = "/applications"

export async function fetchApplicationsFromJobPosting(jobPostingId: string) {
  const { data } = await api.get<JobApplication[]>(
    "/job-postings" + "/" + jobPostingId + "/applications"
  )

  return data
}

export async function fetchJobApplications(params: ApplicationsFilter) {
  const { data } = await api.get<FilterResponse<JobApplication>>(endpoint, {
    params,
  })
  return data
}

export async function createJobApplication(data: JobApplicationRequest) {
  await api.post(endpoint, data)
}
