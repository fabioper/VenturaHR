import { CriteriaRequest } from "./CriteriaRequest"

export interface CreateJobRequest {
  title: string
  description: string
  salary: number
  location: string
  expirationDate: Date
  criterias: CriteriaRequest[]
}
