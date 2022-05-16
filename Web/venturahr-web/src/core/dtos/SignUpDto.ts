import { UserRole } from "../enums/UserRole"

export interface SignUpDto {
  email: string
  password: string
  role: UserRole
  displayName: string
}

export interface SignUpApplicantDto {
  email: string
  password: string
  name: string
}

export interface SignUpCompanyDto {
  email: string
  password: string
  name: string
  registration: string
  phoneNumber: string
}
