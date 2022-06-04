import React, { useEffect } from "react"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { useAuth } from "../../contexts/AuthContext"
import useForm from "../../hooks/useForm"
import { UserType } from "../../../core/enums/UserType"
import { signUpValidator } from "../../../core/validations/signup.validator"
import { SignUpModel } from "../../../core/dtos/auth/SignUpModel"
import { InputMask } from "primereact/inputmask"

const SignUpApplicant: React.FC = () => {
  const { signup, loading } = useAuth()

  const { form, renderError, field } = useForm<SignUpModel>({
    onSubmit: handleSignUp,
    schema: signUpValidator,
  })

  async function handleSignUp(values: SignUpModel) {
    await signup({ ...values })
  }

  useEffect(() => {
    form.setFieldValue("userType", UserType.Applicant)
  }, [])

  return (
    <form onSubmit={form.handleSubmit} className="flex flex-col gap-y-5">
      <div>
        <label htmlFor="name">Nome:</label>
        <InputText {...field("name")} />
        {renderError("name")}
      </div>

      <div>
        <label htmlFor="registration">CPF:</label>
        <InputMask
          {...field("registration")}
          type="tel"
          mask="999.999.999-99"
          autoClear
          unmask
        />
        {renderError("registration")}
      </div>

      <div>
        <label htmlFor="phoneNumber">Telefone:</label>
        <InputMask
          {...field("phoneNumber")}
          type="tel"
          mask="(99) 99999-9999"
          autoClear
          unmask
        />
        {renderError("phoneNumber")}
      </div>

      <div>
        <label htmlFor="email">Email:</label>
        <InputText {...field("email")} type="email" />
        {renderError("email")}
      </div>

      <div>
        <label htmlFor="password">Senha:</label>
        <Password
          {...field("password", { idField: "inputId" })}
          feedback={false}
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
  )
}

export default SignUpApplicant
