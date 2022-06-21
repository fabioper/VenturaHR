import { BaseFilter } from "./BaseFilter"

export interface JobPostingsFilter extends BaseFilter {
  company?: string
  query?: string
}
