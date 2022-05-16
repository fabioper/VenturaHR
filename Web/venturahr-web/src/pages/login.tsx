import React, { useState } from "react"
import { useAuth } from "../shared/contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../shared/hooks/useGuardAgainst"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import Head from "next/head"
import Link from "next/link"
import useForm from "../shared/hooks/useForm"
import { LoginDto } from "../core/dtos/LoginDto"
import { loginValidator } from "../core/validations/login.validator"
import { Message } from "primereact/message"
import SocialProviders from "../shared/components/SocialProviders"
import SectionDivider from "../shared/components/SectionDivider"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const { login, loading } = useAuth()
  const [error, setError] = useState<string | null>(null)

  const handleLogin = async ({ email, password }: LoginDto): Promise<any> => {
    setError(null)
    try {
      await login({ email, password })
    } catch (e) {
      setError("Email/senha incorretas")
    }
  }

  const { form, renderError, isValid } = useForm<LoginDto>(
    {
      email: "",
      password: "",
    },
    loginValidator,
    handleLogin
  )

  return (
    <main>
      <Head>
        <title>Login | VenturaHR</title>
      </Head>

      <div className="container mt-5">
        <header className="my-10 text-center">
          <h2 className="mt-0 mb-2 text-slate-50 text-4xl font-light font-display">
            Entre no seu perfil
          </h2>
        </header>
        <div className="sm:w-full md:w-10/12 lg:w-4/12 bg-[#0d1424] mx-auto mb-10 p-10 rounded-xl shadow-xl">
          {error && (
            <div className="mb-5 flex flex-col gap-4">
              <Message severity="error" text={error} />
            </div>
          )}

          <form onSubmit={form.handleSubmit} className="flex flex-col gap-y-5">
            <div>
              <label className="block mb-1.5" htmlFor="email">
                Email:
              </label>
              <InputText
                autoFocus
                id="email"
                type="email"
                placeholder="usuario@exemplo.com"
                value={form.values.email}
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                className={`w-full p-button-shadowed ${
                  !isValid("email") ? "p-invalid" : ""
                }`}
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
                className="w-full"
                label="Entrar"
              />
            </div>
          </form>

          <SectionDivider>ou</SectionDivider>

          <SocialProviders
            onUserCancelError={() => setError("Não autorizado")}
            onError={() => setError("Ocorreu um erro")}
          />

          <div className="flex flex-col items-center">
            <Link href="/signup">
              <Button
                type="button"
                className="p-button-text p-button-rounded p-button-sm mt-2"
                icon={PrimeIcons.ARROW_RIGHT}
                iconPos="right"
                label="Não tenho cadastro"
              />
            </Link>
          </div>
        </div>
      </div>
    </main>
  )
}

export default Login
