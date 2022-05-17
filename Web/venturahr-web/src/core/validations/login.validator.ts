import * as yup from "yup"
import { SchemaOf } from "yup"
import { LoginModel } from "../dtos/LoginModel"

export const loginValidator: SchemaOf<LoginModel> = yup.object().shape({
  email: yup
    .string()
    .email("Formato de e-mail inválido")
    .required("Campo obrigatório"),
  password: yup.string().required("Campo obrigatório"),
})
