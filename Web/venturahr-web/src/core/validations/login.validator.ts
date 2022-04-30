import * as yup from "yup"
import { SchemaOf } from "yup"
import { LoginDto } from "../dtos/LoginDto"

export const loginValidator: SchemaOf<LoginDto> = yup.object().shape({
  email: yup
    .string()
    .email("Formato de e-mail inválido")
    .required("Campo obrigatório"),
  password: yup.string().required("Campo obrigatório"),
})
