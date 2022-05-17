import * as yup from "yup"
import { SchemaOf } from "yup"
import { CreateJobPostingModel } from "../dtos/jobposting/CreateJobPostingModel"

export const createJobPostingValidator: SchemaOf<CreateJobPostingModel> = yup
  .object()
  .shape({
    role: yup.string().required("Campo obrigatório"),
    compensation: yup.number().required("Campo obrigatório"),
    endDate: yup.date().required("Campo obrigatório"),
    location: yup.string().required("Campo obrigatório"),
    description: yup.string().nullable().required("Campo obrigatório"),
  })
