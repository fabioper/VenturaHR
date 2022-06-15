import { UserType } from "../../enums/UserType"

export interface SignUpRequest {
  email: string
  password: string
  name: string
  registration: string
  phoneNumber: string
  userType: UserType
}
