import { NextPage } from "next"
import Link from "next/link"
import { useState } from "react"
import { firebaseApp } from "../config/firebase/firebase.config"
import {
  createUserWithEmailAndPassword,
  getAuth,
  signInWithEmailAndPassword,
} from "firebase/auth"
import { getFunctions, httpsCallable } from "firebase/functions"

async function ensureUserRole(email: string, role: string): Promise<void> {
  const functions = getFunctions(firebaseApp)
  const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")
  await assignRoleToUser({ email: email, role: role })
}

const Signup: NextPage = () => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const getFormValues = () => ({ email, password })

  async function handleSubmit(values: { password: string; email: string }) {
    const auth = getAuth(firebaseApp)
    const { email, password } = values

    try {
      const { user } = await createUserWithEmailAndPassword(
        auth,
        email,
        password
      )

      const role = "applicant"
      await ensureUserRole(user.email || email, role)

      const result = await signInWithEmailAndPassword(auth, email, password)
      console.log(result.user)
    } catch (e) {
      console.log(e)
    }
  }

  return (
    <div>
      <h1>Cadastre-se</h1>
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
