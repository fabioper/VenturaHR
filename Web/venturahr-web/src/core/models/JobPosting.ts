import Company from "./Company"
import Criteria from "./Criteria"

export default class JobPosting {
  id: string
  title: string
  description: string
  location: string
  salary: number
  average: number
  createdAt: string
  updatedAt: string
  expireAt: string
  company: Company
  criterias: Criteria[]
}
