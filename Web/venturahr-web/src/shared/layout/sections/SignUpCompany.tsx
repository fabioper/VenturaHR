import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import useForm from "../../hooks/useForm"
import { UserType } from "../../../core/enums/UserType"
import { signUpValidator } from "../../../core/validations/signup.validator"
import { Message } from "primereact/message"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { SignUpModel } from "../../../core/dtos/auth/SignUpModel"

const SignUpCompany: React.FC = () => {
  const [error, setError] = useState<string | null>(null)
  const { signup, loading } = useAuth()

  const { form, renderError, isValid } = useForm<SignUpModel>(
    {
      name: "",
      email: "",
      password: "",
      phoneNumber: "",
      registration: "",
      userType: UserType.Company,
    },
    signUpValidator,
    handleSignUp
  )

  async function handleSignUp(values: SignUpModel) {
    setError(null)
    try {
      await signup({ ...values })
    } catch (e) {
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
          <label className="block mb-1.5" htmlFor="name">
            Nome:
          </label>
          <InputText
            autoFocus
            id="name"
            placeholder="John Doe"
            value={form.values.name}
            onChange={form.handleChange}
            onBlur={form.handleBlur}
            className={`w-full ${!isValid("name") ? "p-invalid" : ""}`}
          />
          {renderError("name")}
        </div>

        <div>
          <label className="block mb-1.5" htmlFor="registration">
            CNPJ:
          </label>
          <InputText
            autoFocus
            id="registration"
            placeholder="John Doe"
            value={form.values.registration}
            onChange={form.handleChange}
            onBlur={form.handleBlur}
            className={`w-full ${!isValid("registration") ? "p-invalid" : ""}`}
          />
          {renderError("registration")}
        </div>

        <div>
          <label className="block mb-1.5" htmlFor="phoneNumber">
            Telefone:
          </label>
          <InputText
            autoFocus
            id="phoneNumber"
            placeholder="John Doe"
            value={form.values.phoneNumber}
            onChange={form.handleChange}
            onBlur={form.handleBlur}
            className={`w-full ${!isValid("phoneNumber") ? "p-invalid" : ""}`}
          />
          {renderError("phoneNumber")}
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
    </>
  )
}

export default SignUpCompany
