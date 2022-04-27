import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"

const Login: NextPage = () => {
  useGuardAgainst(({ isLogged }) => isLogged)
  const { login } = useAuth()

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  return (
    <div>
      <h1>Faça o login</h1>
      <form>
        <div>
          <label>Email:</label>
          <input
            type="text"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </div>

        <div>
          <label>Senha:</label>
          <input
            type="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
        </div>

        <div>
          <button
            type="button"
            onClick={async () => await login({ email, password })}
          >
            Entrar
          </button>
        </div>

        <div>
          Ainda não tem conta? <Link href="/company/signup">Cadastre-se</Link>
        </div>
      </form>
    </div>
  )
}

export default Login
