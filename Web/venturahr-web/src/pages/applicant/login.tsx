import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const { login } = useAuth()

  return (
    <main>
      <div className="container py-6">
        <form className="w-6/12 bg-slate-50 text-sky-800 mx-auto p-10 rounded-lg">
          <h1 className="mb-5 font-bold">Faça o login</h1>
          <div className="mb-3">
            <label className="block mb-1 text-sm">Email:</label>
            <input
              type="text"
              value={email}
              className="border w-full"
              onChange={e => setEmail(e.target.value)}
            />
          </div>

          <div>
            <label className="block mb-1">Senha:</label>
            <input
              type="password"
              value={password}
              className="border w-full"
              onChange={e => setPassword(e.target.value)}
            />
          </div>

          <div className="my-5">
            <button
              type="button"
              onClick={async () => await login({ email, password })}
            >
              Entrar
            </button>
          </div>

          <div className="border-t border-t-slate-200 pt-3 text-center">
            Ainda não tem conta?{" "}
            <Link href="/applicant/signup">
              <span className="underline cursor-pointer">Cadastre-se</span>
            </Link>
          </div>
        </form>
      </div>
    </main>
  )
}

export default Login
