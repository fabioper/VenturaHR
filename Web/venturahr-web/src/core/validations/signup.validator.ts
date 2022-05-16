import * as yup from "yup"
import { SchemaOf } from "yup"
import {
  SignUpApplicantDto,
  SignUpCompanyDto,
  SignUpDto,
} from "../dtos/SignUpDto"
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

export const signupApplicantValidator: SchemaOf<SignUpApplicantDto> = yup
  .object()
  .shape({
    email: yup.string().email().required(),
    password: yup.string().required(),
    name: yup.string().required(),
  })

export const signupCompanyValidator: SchemaOf<SignUpCompanyDto> = yup
  .object()
  .shape({
    email: yup.string().email().required(),
    password: yup.string().required(),
    name: yup.string().required(),
    phoneNumber: yup.string().required(),
    registration: yup.string().required(),
  })
