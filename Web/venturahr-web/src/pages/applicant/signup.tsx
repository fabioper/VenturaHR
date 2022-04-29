import { GoogleAuthProvider } from "firebase/auth"
import { NextPage } from "next"
import Link from "next/link"
import { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"

const Signup: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const { signup, loginWithProvider } = useAuth()
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [displayName, setDisplayName] = useState("")
  const providers = [{ label: "Google", provider: new GoogleAuthProvider() }]

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
            Cadastrar
          </button>
        </div>

        <div>
          <h2>Ou faça login através das redes sociais abaixo:</h2>
          <ul>
            {providers.map((provider, index) => (
              <li key={index}>
                <a
                  href="#"
                  onClick={() =>
                    loginWithProvider(provider.provider, "applicant")
                  }
                >
                  {provider.label}
                </a>
              </li>
            ))}
          </ul>
        </div>

        <div>
          Já tem conta? <Link href="/applicant/login">Faça login</Link>
        </div>
      </form>
    </div>
  )
}

export default Signup
