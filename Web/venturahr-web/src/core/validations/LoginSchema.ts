import * as yup from "yup"
import { SchemaOf } from "yup"
import { LoginRequest } from "../dtos/requests/LoginRequest"

export const loginSchema: SchemaOf<LoginRequest> = yup.object().shape({
  email: yup
    .string()
    .email("Formato de e-mail inválido")
    .required("Campo obrigatório"),
  password: yup.string().required("Campo obrigatório"),
})
