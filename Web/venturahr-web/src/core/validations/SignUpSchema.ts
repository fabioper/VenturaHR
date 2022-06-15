import * as yup from "yup"
import { SchemaOf } from "yup"
import { UserType } from "../enums/UserType"
import { SignUpRequest } from "../dtos/requests/SignUpRequest"

const requiredField = "Obrigatório"
const invalidEmail = "E-mail inválido"
const invalidCpf = "CPF inválido"
const invalidCnpj = "CNPJ inválido"

export const signUpSchema: SchemaOf<SignUpRequest> = yup.object().shape({
  email: yup.string().email(invalidEmail).required(requiredField),
  password: yup.string().required(requiredField),
  name: yup.string().required(requiredField),
  phoneNumber: yup.string().required(requiredField),
  registration: yup
    .string()
    .when("userType", {
      is: UserType.Applicant,
      then: yup.string().min(11, invalidCpf).max(11, invalidCpf),
      otherwise: yup.string().min(14, invalidCnpj).max(14, invalidCnpj),
    })
    .required(requiredField),
  userType: yup.mixed().oneOf(Object.values(UserType)),
})
