import * as yup from "yup"
import { SchemaOf } from "yup"
import { CreateJobRequest } from "../dtos/requests/CreateJobRequest"

export const createJobSchema: SchemaOf<CreateJobRequest> = yup.object().shape({
  title: yup.string().required("Campo obrigatório"),
  description: yup.string().nullable().required("Campo obrigatório"),
  salary: yup.number().required("Campo obrigatório"),
  expirationDate: yup.date().required("Campo obrigatório"),
  location: yup.string().required("Campo obrigatório"),
})
