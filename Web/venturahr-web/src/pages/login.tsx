import Link from "next/link"
import React, { useState } from "react"
import { getAuth, signInWithEmailAndPassword } from "firebase/auth"
import { firebaseApp } from "../config/firebase/firebase.config"

const login: React.FC = () => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const getFormValues = () => ({ email, password })

  async function handleSubmit(values: { password: string; email: string }) {
    const auth = getAuth(firebaseApp)

    try {
      const result = await signInWithEmailAndPassword(
        auth,
        values.email,
        values.password
      )
      console.log(result.user)
    } catch (e) {
      console.log(e)
    }
  }

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
          <button type="button" onClick={() => handleSubmit(getFormValues())}>
            Entrar
          </button>
        </div>

        <div>
          Ainda não tem conta? <Link href="/signup">Cadastre-se</Link>
        </div>
      </form>
    </div>
  )
}

export default login
