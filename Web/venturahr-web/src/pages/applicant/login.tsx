import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"
import { Message } from "primereact/message"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import Head from "next/head"
import {
  GithubAuthProvider,
  GoogleAuthProvider,
  TwitterAuthProvider,
} from "firebase/auth"
import Link from "next/link"
import { UserRole } from "../../core/enums/UserRole"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState<string | null>()
  const [loading, setLoading] = useState(false)

  const { login, loginWithProvider, loading: authenticating } = useAuth()

  async function handleLogin(
    e: React.FormEvent<HTMLFormElement>
  ): Promise<any> {
    e.preventDefault()
    setError(null)
    setLoading(true)
    try {
      return await login({ email, password })
    } catch (e) {
      setError("Verifique suas credenciais.")
    } finally {
      setLoading(false)
    }
  }

  return (
    <main>
      <Head>
        <title>Login | VenturaHR</title>
      </Head>

      <div className="container mt-5">
        <header className="my-10 text-center">
          <h2 className="mt-0 mb-2 text-slate-50 text-3xl">
            Entre no seu perfil
          </h2>
        </header>
        <div className="sm:w-full md:w-10/12 lg:w-4/12 bg-slate-900 mx-auto mb-10 p-10 rounded-xl bg-opacity-80">
          {!!error && (
            <div className="mb-5 flex flex-col gap-4">
              <Message severity="error" text={error} />
            </div>
          )}
          <form onSubmit={handleLogin} className="flex flex-col gap-y-5">
            <div>
              <label className="block mb-1.5" htmlFor="email">
                Email:
              </label>
              <InputText
                autoFocus
                id="email"
                type="email"
                placeholder="usuario@exemplo.com"
                value={email}
                onChange={e => setEmail(e.target.value)}
                className="w-full"
              />
            </div>

            <div>
              <label className="block mb-1.5" htmlFor="password">
                Senha:
              </label>
              <Password
                inputId="password"
                value={password}
                onChange={e => setPassword(e.target.value)}
                placeholder="Digite sua senha"
                feedback={false}
                className="w-full"
                inputClassName="w-full"
              />
            </div>

            <div>
              <Button
                type="submit"
                loading={loading || authenticating}
                icon={PrimeIcons.SIGN_IN}
                className="w-full"
                label="Entrar"
              />
            </div>
          </form>

          <div className="my-7 grid grid-cols-[1fr_auto_1fr] gap-x-2 items-center">
            <span className="h-0.5 bg-slate-800" />
            <span className="text-slate-500 text-sm font-normal">ou</span>
            <span className="h-0.5 bg-slate-800" />
          </div>

          <div className="flex flex-col gap-3 my-7">
            <div className="flex justify-center items-center flex-row gap-2">
              <Button
                type="button"
                className="google p-0"
                aria-label="Google"
                onClick={async () =>
                  await loginWithProvider(
                    new GoogleAuthProvider(),
                    UserRole.Applicant
                  )
                }
              >
                <i className="pi pi-google px-2 py-2"></i>
                <span className="px-0 text-sm">Google</span>
              </Button>

              <Button
                type="button"
                className="github p-0"
                aria-label="github"
                onClick={async () =>
                  await loginWithProvider(
                    new GithubAuthProvider(),
                    UserRole.Applicant
                  )
                }
              >
                <i className="pi pi-github px-2 py-2"></i>
                <span className="px-0 text-sm">Github</span>
              </Button>

              <Button
                type="button"
                className="twitter p-0"
                aria-label="github"
                onClick={async () =>
                  await loginWithProvider(
                    new TwitterAuthProvider(),
                    UserRole.Applicant
                  )
                }
              >
                <i className="pi pi-twitter px-2 py-2"></i>
                <span className="px-0 text-sm">Twitter</span>
              </Button>
            </div>
          </div>

          <div className="flex flex-col items-center">
            <Link href="/applicant/signup">
              <Button
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
