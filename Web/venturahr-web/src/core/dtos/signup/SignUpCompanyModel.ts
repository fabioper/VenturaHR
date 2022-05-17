import { SignUpModel } from "./SignUpModel"

export interface SignUpCompanyModel extends SignUpModel {
  registration: string
  phoneNumber: string
}
