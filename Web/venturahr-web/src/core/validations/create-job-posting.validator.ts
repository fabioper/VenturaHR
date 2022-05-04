import * as yup from "yup"
import { SchemaOf } from "yup"
import { CreateJobPostingDto } from "../dtos/CreateJobPostingDto"

export const createJobPostingValidator: SchemaOf<CreateJobPostingDto> = yup
  .object()
  .shape({
    role: yup.string().required("Campo obrigatório"),
    compensation: yup.number().required("Campo obrigatório"),
    endDate: yup.date().required("Campo obrigatório"),
    location: yup.string().required("Campo obrigatório"),
    description: yup.string().nullable().required("Campo obrigatório"),
  })
