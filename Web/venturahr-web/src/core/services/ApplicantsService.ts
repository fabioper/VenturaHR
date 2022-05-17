import { FinishApplicantProfileModel } from "../dtos/profile/FinishApplicantProfileModel"
import api from "../config/api"

const endpoint = "/applicants"

export async function createApplicantUser(data: FinishApplicantProfileModel) {
  return await api.post(endpoint, data)
}
