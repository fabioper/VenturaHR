/* eslint-disable @typescript-eslint/no-empty-interface */
import { UserRole } from "../../enums/UserRole"

export interface SignUpModel {
  email: string
  password: string
  role: UserRole
  name: string
}
