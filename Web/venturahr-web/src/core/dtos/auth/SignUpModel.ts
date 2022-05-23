/* eslint-disable @typescript-eslint/no-empty-interface */
import { UserType } from "../../enums/UserType"

export interface SignUpModel {
  email: string
  password: string
  role: UserType
  name: string
  registration: string
  phoneNumber: string
}