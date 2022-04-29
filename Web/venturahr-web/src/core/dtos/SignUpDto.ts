import { UserRole } from "../enums/UserRole"

export interface SignUpDto {
  email: string
  password: string
  role: UserRole
  displayName: string
}
