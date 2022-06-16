import { DesiredProfile } from "../../enums/DesiredProfile"

export interface CriteriaRequest {
  title: string
  description: string
  weight: number
  desiredProfile: DesiredProfile
}
