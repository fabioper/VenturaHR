import * as yup from "yup"
import { SchemaOf } from "yup"
import { UserType } from "../enums/UserType"
import { SignUpModel } from "../dtos/auth/SignUpModel"

export const signUpValidator: SchemaOf<SignUpModel> = yup.object().shape({
  role: yup.mixed().oneOf(Object.values(UserType)),
  email: yup.string().email().required(),
  password: yup.string().required(),
  name: yup.string().required(),
  phoneNumber: yup.string().required(),
  registration: yup.string().required(),
})
