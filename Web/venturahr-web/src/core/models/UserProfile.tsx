export class UserProfile {
  id: string
  name: string
  email: string
  jwt: string
  photoUrl?: string
  roles: string[]

  constructor(props: Partial<UserProfile>) {
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
