import { UserType } from "../enums/UserType"

export class UserProfile {
  id: string
  name: string
  email: string
  phoneNumber: string
  registration: string
  userType: UserType

  constructor(props: Partial<UserProfile>) {
    Object.assign(this, props)
  }

  public hasRole(...roles: UserType[]) {
    return roles.includes(this.userType)
  }

  get redirectPage() {
    if (this.hasRole(UserType.Applicant)) {
      return "/applicant/dashboard"
    }

    if (this.hasRole(UserType.Company)) {
      return "/company/dashboard"
    }

    return "/"
  }
}
