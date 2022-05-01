import * as yup from "yup"
import { SchemaOf } from "yup"
import { SignUpDto } from "../dtos/SignUpDto"
import { UserRole } from "../enums/UserRole"

export const signupValidator: SchemaOf<SignUpDto> = yup.object().shape({
  email: yup
    .string()
    .email("Formato de e-mail inválido")
    .required("Campo obrigatório"),
  password: yup.string().required("Campo obrigatório"),
  displayName: yup.string().required("Campo obrigatório"),
  role: yup.mixed().oneOf(Object.values(UserRole)),
})
