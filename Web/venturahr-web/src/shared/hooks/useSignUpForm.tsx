import { UserType } from "../../core/enums/UserType"
import { useAuth } from "../contexts/AuthContext"
import { useToaster } from "./useToaster"
import useForm from "./useForm"
import { SignUpModel } from "../../core/dtos/auth/SignUpModel"
import { signUpValidator } from "../../core/validations/signup.validator"
import { useEffect } from "react"

export function useSignUpForm(userType: UserType) {
  const { signup } = useAuth()
  const { toast } = useToaster()

  const { form, renderError, field } = useForm<SignUpModel>({
    onSubmit: handleSignUp,
    schema: signUpValidator,
  })

  function handleValidationErrors(errors: string[]): void {
    toast.error("Por favor, revise os itens destacados.")

    if (errors.includes("Email")) {
      form.setFieldError("email", "E-mail já utilizado")
    }

    if (errors.includes("Registration")) {
      form.setFieldError("registration", "CNPJ já utilizado")
    }
  }

  async function handleSignUp(values: SignUpModel) {
    try {
      await signup({ ...values })
    } catch (e: any) {
      if (e.response?.status === 400) {
        const data = e.response.data as { errors: { [field: string]: string } }
        const errors = Object.keys(data.errors)
        handleValidationErrors(errors)
      }
    }
  }

  useEffect(() => {
    form.setFieldValue("userType", userType)
  }, [])

  return { form, renderError, field }
}
