export default class FilterResponse<T> {
  page: number
  total: number
  results: T[]
}
