import { CompanyProfile } from "./CompanyProfile"
import { ApplicantProfile } from "./ApplicantProfile"

export class AuthUser {
  id: string
  externalId: string
  jwt: string
  photoUrl?: string
  roles: string[]
  details: CompanyProfile | ApplicantProfile

  constructor(props: Partial<AuthUser>) {
    Object.assign(this, props)
  }

  public hasRole(...roles: string[]) {
    const hasAnyRole = !!roles && roles.length > 0
    return hasAnyRole && roles.every(role => this.roles.includes(role))
  }

  get redirectPage() {
    if (this.hasRole("applicant")) {
      return "/applicant/dashboard"
    }

    if (this.hasRole("company")) {
      return "/company/dashboard"
    }

    return "/"
  }
}
