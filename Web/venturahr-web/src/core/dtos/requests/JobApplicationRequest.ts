import CriteriaAnswerRequest from "./CriteriaAnswerRequest"

export default class JobApplicationRequest {
  jobPostingId: string
  criteriaAnswers: CriteriaAnswerRequest[]
}
