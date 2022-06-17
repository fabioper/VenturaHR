import { DesiredProfile } from "../enums/DesiredProfile"

export default class Criteria {
  id: string
  title: string
  description: string
  weight: number
  desiredProfile: DesiredProfile
}
