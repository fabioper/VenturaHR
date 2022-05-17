import { FinishCompanyProfileModel } from "../dtos/profile/FinishCompanyProfileModel"
import api from "../config/api"

const endpoint = "/companies"

export async function createCompanyUser(data: FinishCompanyProfileModel) {
  return await api.post(endpoint, data)
}
