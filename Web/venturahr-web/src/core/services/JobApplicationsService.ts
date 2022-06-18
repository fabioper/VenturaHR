import jobPostingsApi from "../config/api/jobpostings"
import { JobApplication } from "../models/JobApplication"

const api = jobPostingsApi
const endpoint = "/applications"

export async function fetchApplicationsFromJobPosting(jobPostingId: string) {
  const { data } = await api.get<JobApplication[]>(
    "/job-postings" + "/" + jobPostingId + "/applications"
  )

  return data
}
