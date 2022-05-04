export class CreateJobPostingDto {
  role: string
  compensation: number
  location: string
  endDate: Date
  description: string

  constructor(props: Partial<CreateJobPostingDto>) {
    Object.assign(this, props)
  }
}
