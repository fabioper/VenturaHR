import { NextPage } from "next"
import Link from "next/link"
import React, { useState } from "react"
import { useGuardAgainst } from "../hooks/useGuardAgainst"
import { UserRole } from "../core/enums/UserRole"
import Head from "next/head"
import { Message } from "primereact/message"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import SectionDivider from "../components/SectionDivider"
import SocialProviders from "../components/SocialProviders"
import useForm from "../hooks/useForm"
import { SignUpDto } from "../core/dtos/SignUpDto"
import { signupValidator } from "../core/validations/signup.validator"
import { useAuth } from "../contexts/AuthContext"
import { FirebaseError } from "@firebase/util"
import { SelectButton } from "primereact/selectbutton"

const Signup: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)
  const [error, setError] = useState<string | null>(null)
  const { signup, loading } = useAuth()

  const { form, renderError, isValid } = useForm<SignUpDto>(
    {
      displayName: "",
      email: "",
      password: "",
      role: UserRole.Applicant,
    },
    signupValidator,
    handleSignUp
  )

  function handleFirebaseErrors(e: FirebaseError): void {
    if (e.code === "auth/email-already-in-use") {
      setError(null)
      form.setFieldError("email", "E-mail não disponível")
    } else {
      console.log(e.code)
    }
  }

  async function handleSignUp(values: SignUpDto) {
    setError(null)
    try {
      await signup({
        email: values.email,
        password: values.password,
        displayName: values.displayName,
        role: values.role,
      })
    } catch (e) {
      if (e instanceof FirebaseError) {
        return handleFirebaseErrors(e)
      }
      console.log(e)
    }
  }

  const roleOptions = [
    { label: "Candidato", value: UserRole.Applicant },
    { label: "Empresa", value: UserRole.Company },
  ]

  return (
    <div>
      <Head>
        <title>Cadastro | VenturaHR</title>
      </Head>

      <div className="container">
        <header className="my-10 text-center">
          <h2 className="mt-0 mb-2 text-slate-50 text-4xl font-light font-display">
            Crie seu perfil
          </h2>
        </header>
        <div className="sm:w-full md:w-10/12 lg:w-4/12 bg-[#0d1424] mx-auto mb-10 p-10 rounded-xl bg-opacity-80">
          {error && (
            <div className="mb-5 flex flex-col gap-4">
              <Message severity="error" text={error} />
            </div>
          )}

          <form onSubmit={form.handleSubmit} className="flex flex-col gap-y-5">
            <div>
              <label className="block mb-1.5" htmlFor="displayName">
                Nome:
              </label>
              <InputText
                autoFocus
                id="displayName"
                placeholder="John Doe"
                value={form.values.displayName}
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                className={`w-full ${
                  !isValid("displayName") ? "p-invalid" : ""
                }`}
              />
              {renderError("displayName")}
            </div>

            <div>
              <label className="block mb-1.5" htmlFor="email">
                Email:
              </label>
              <InputText
                id="email"
                type="email"
                placeholder="usuario@exemplo.com"
                value={form.values.email}
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                className={`w-full ${!isValid("email") ? "p-invalid" : ""}`}
              />
              {renderError("email")}
            </div>

            <div>
              <label className="block mb-1.5" htmlFor="role">
                Perfil
              </label>

              <div className="flex items-center gap-5 mt-3">
                <SelectButton
                  id="role"
                  value={form.values.role}
                  options={roleOptions}
                  onChange={form.handleChange}
                  onBlur={form.handleBlur}
                  className="w-full"
                />
                {/* <div className="flex items-center gap-2">
                  <RadioButton
                    inputId="applicantRole"
                    value={UserRole.Applicant}
                    name="role"
                    onChange={form.handleChange}
                    checked={form.values.role === UserRole.Applicant}
                  />
                  <label htmlFor="applicantRole">Candidato</label>
                </div>
                <div className="flex items-center gap-2">
                  <RadioButton
                    inputId="companyRole"
                    value={UserRole.Company}
                    name="role"
                    onChange={form.handleChange}
                    checked={form.values.role === UserRole.Company}
                  />
                  <label htmlFor="companyRole">Empresa</label>
                </div> */}
              </div>
              {renderError("role")}
            </div>

            <div>
              <label className="block mb-1.5" htmlFor="password">
                Senha:
              </label>
              <Password
                inputId="password"
                value={form.values.password}
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                placeholder="Digite sua senha"
                feedback={false}
                className={`w-full ${!isValid("password") ? "p-invalid" : ""}`}
                inputClassName="w-full"
              />
              {renderError("password")}
            </div>

            <div>
              <Button
                type="submit"
                loading={form.isSubmitting || loading}
                disabled={!form.isValid}
                icon={PrimeIcons.SIGN_IN}
                className="w-full p-button-shadowed"
                label="Confirmar"
              />
            </div>
          </form>

          <SectionDivider>ou</SectionDivider>

          <SocialProviders
            onUserCancelError={() => setError("Não autorizado")}
            onError={() => setError("Ocorreu um erro")}
          />

          <div className="flex flex-col items-center">
            <Link href="/login">
              <Button
                type="button"
                className="p-button-text p-button-rounded p-button-sm mt-2"
                icon={PrimeIcons.ARROW_RIGHT}
                iconPos="right"
                label="Já tenho cadastro"
              />
            </Link>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Signup
