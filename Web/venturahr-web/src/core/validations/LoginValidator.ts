import * as yup from "yup"
import { SchemaOf } from "yup"
import { LoginDto } from "../dtos/LoginDto"

export const LoginValidator: SchemaOf<LoginDto> = yup.object().shape({
  email: yup.string().email().required(),
  password: yup.string().required(),
})
