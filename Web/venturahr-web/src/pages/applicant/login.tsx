import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"
import { Message } from "primereact/message"
import { InputText } from "primereact/inputtext"
import { Password } from "primereact/password"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState<string | null>()
  const [loading, setLoading] = useState(false)

  const { login } = useAuth()

  async function handleLogin(): Promise<any> {
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
      <div className="container pt-10">
        <h2 className="mb-7 text-center text-3xl font-bold">
          Entrar como Candidato
        </h2>

        <form className="w-4/12 bg-slate-400 text-white bg-opacity-5 mx-auto p-10 rounded-xl">
          {!!error && (
            <div className="mb-5 flex flex-col gap-4">
              <Message severity="warn">{error}</Message>
            </div>
          )}
          <div className="mb-3">
            <label className="block mb-1">Email:</label>
            <InputText
              value={email}
              onChange={e => setEmail(e.target.value)}
              className="w-full"
            />
          </div>

          <div>
            <label className="block mb-1">Senha:</label>
            <Password
              value={password}
              onChange={e => setPassword(e.target.value)}
              feedback={false}
              className="w-full"
              inputClassName="w-full"
            />
          </div>

          <div className="my-7">
            <Button
              onClick={handleLogin}
              loading={loading}
              icon={PrimeIcons.SIGN_IN}
              className="w-full"
              label={loading ? "Aguard um momento" : "Entrar"}
            />
          </div>

          <div className="border-0 border-solid border-t border-slate-700 pt-5 text-center">
            Ainda não tem conta?
            <div>
              <Link href="/applicant/signup">
                <Button className="p-button-text p-button-sm mt-2">
                  Crie sua conta
                </Button>
              </Link>
            </div>
          </div>
        </form>
      </div>
    </main>
  )
}

export default Login
