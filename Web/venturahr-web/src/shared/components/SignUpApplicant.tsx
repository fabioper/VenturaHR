import React, { useState } from "react"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import SectionDivider from "./SectionDivider"
import SocialProviders from "./SocialProviders"
import { useAuth } from "../contexts/AuthContext"
import useForm from "../hooks/useForm"
import { SignUpDto } from "../../core/dtos/SignUpDto"
import { UserRole } from "../../core/enums/UserRole"
import { signupValidator } from "../../core/validations/signup.validator"
import { FirebaseError } from "@firebase/util"
import { Message } from "primereact/message"

const SignUpApplicant: React.FC = () => {
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

  return (
    <>
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
            className={`w-full ${!isValid("displayName") ? "p-invalid" : ""}`}
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
            toggleMask
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
    </>
  )
}

export default SignUpApplicant
