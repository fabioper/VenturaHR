import * as yup from "yup"
import { SchemaOf } from "yup"
import { UserRole } from "../enums/UserRole"
import { SignUpApplicantModel } from "../dtos/signup/SignUpApplicantModel"
import { SignUpCompanyModel } from "../dtos/signup/SignUpCompanyModel"

export const signupApplicantValidator: SchemaOf<SignUpApplicantModel> = yup
  .object()
  .shape({
    role: yup.mixed().oneOf(Object.values(UserRole)),
    email: yup.string().email().required(),
    password: yup.string().required(),
    name: yup.string().required(),
  })

export const signupCompanyValidator: SchemaOf<SignUpCompanyModel> = yup
  .object()
  .shape({
    role: yup.mixed().oneOf(Object.values(UserRole)),
    email: yup.string().email().required(),
    password: yup.string().required(),
    name: yup.string().required(),
    phoneNumber: yup.string().required(),
    registration: yup.string().required(),
  })
