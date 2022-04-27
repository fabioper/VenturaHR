import { NextPage } from "next"
import Link from "next/link"
import { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"

const Signup: NextPage = () => {
  useGuardAgainst(({ isLogged }) => isLogged)

  const { signup } = useAuth()
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [displayName, setDisplayName] = useState("")

  return (
    <div>
      <h1>Cadastre-se</h1>
      <form>
        <div>
          <label>Nome:</label>
          <input
            type="text"
            value={displayName}
            onChange={e => setDisplayName(e.target.value)}
          />
        </div>

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
            onClick={() =>
              signup({
                displayName,
                email,
                password,
                role: "applicant",
              })
            }
          >
            Criar
          </button>
        </div>

        <div>
          Já tem conta? <Link href="/login">Faça login</Link>
        </div>
      </form>
    </div>
  )
}

export default Signup
