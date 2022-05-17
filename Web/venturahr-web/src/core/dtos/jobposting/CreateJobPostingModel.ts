export class CreateJobPostingModel {
  role: string
  compensation: number
  location: string
  endDate: Date
  description: string

  constructor(props: Partial<CreateJobPostingModel>) {
    Object.assign(this, props)
  }
}
