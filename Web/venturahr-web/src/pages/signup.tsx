import { NextPage } from "next"
import Link from "next/link"
import { useState } from "react"
import { firebaseApp } from "../config/firebase/firebase.config"
import { createUserWithEmailAndPassword, getAuth } from "firebase/auth"

const Signup: NextPage = () => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const getFormValues = () => ({ email, password })

  async function handleSubmit(values: { password: string; email: string }) {
    const auth = getAuth(firebaseApp)

    try {
      const result = await createUserWithEmailAndPassword(
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
