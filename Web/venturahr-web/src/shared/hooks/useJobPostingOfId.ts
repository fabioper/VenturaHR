import JobPosting from "../../core/models/JobPosting"
import { useEffect, useState } from "react"
import { fetchJobPosting } from "../../core/services/JobPostingsService"

export function useJobPostingOfId(
  jobPostingId: string
): JobPosting | undefined {
  const [jobPosting, setJobPosting] = useState<JobPosting>()

  const loadJobPostingOfId = async (jobPostingId: string) => {
    const data = await fetchJobPosting(jobPostingId)
    setJobPosting(data)
  }

  useEffect(() => {
    if (jobPostingId) {
      ;(async () => await loadJobPostingOfId(jobPostingId))()
    }
  }, [jobPostingId])

  return jobPosting
}
