import JobPosting from "./JobPosting"
import CriteriaAnswer from "./CriteriaAnswer"
import { Applicant } from "./Applicant"

export class JobApplication {
  id: string
  average: number
  appliedAt: string
  applicant: Applicant
  jobPosting: JobPosting
  answers: CriteriaAnswer[]
}
