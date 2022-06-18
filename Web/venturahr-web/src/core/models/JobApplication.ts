import JobPosting from "./JobPosting"
import CriteriaAnswer from "./CriteriaAnswer"

export class JobApplication {
  id: string
  average: number
  appliedAt: string
  jobPosting: JobPosting
  answers: CriteriaAnswer[]
}
