import React, { useEffect, useState } from "react"
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
import { InputMask } from "primereact/inputmask"

const SignUpCompany: React.FC = () => {
  const [error, setError] = useState<string | null>(null)
  const { signup, loading } = useAuth()

  const { form, renderError, isValid, field } = useForm<SignUpModel>({
    onSubmit: handleSignUp,
    schema: signUpValidator,
  })

  async function handleSignUp(values: SignUpModel) {
    setError(null)

    try {
      await signup({ ...values })
    } catch (e) {
      console.log(e)
    }
  }

  useEffect(() => {
    form.setFieldValue("userType", UserType.Applicant)
  }, [])

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
          <InputText {...field("name")} />
          {renderError("name")}
        </div>

        <div>
          <label className="block mb-1.5" htmlFor="registration">
            CNPJ:
          </label>
          <InputMask
            {...field("registration")}
            type="tel"
            mask="99.999.999/9999-99"
            autoClear
            unmask
          />
          {renderError("registration")}
        </div>

        <div>
          <label className="block mb-1.5" htmlFor="phoneNumber">
            Telefone:
          </label>
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
          <label className="block mb-1.5" htmlFor="email">
            Email:
          </label>
          <InputText type="email" {...field("email")} />
          {renderError("email")}
        </div>

        <div>
          <label className="block mb-1.5" htmlFor="password">
            Senha:
          </label>
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
    </>
  )
}

export default SignUpCompany
