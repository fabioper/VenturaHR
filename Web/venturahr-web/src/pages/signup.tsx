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

const Signup: NextPage = () => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const getFormValues = () => ({ email, password })

  async function handleSubmit(values: { password: string; email: string }) {
    const auth = getAuth(firebaseApp)

    try {
      const { user } = await createUserWithEmailAndPassword(
        auth,
        values.email,
        values.password
      )

      const functions = getFunctions(firebaseApp)
      const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")

      const role = "applicant"
      await assignRoleToUser({ email: user.email, role: role })

      const { user: loggedUser } = await signInWithEmailAndPassword(
        auth,
        values.email,
        values.password
      )
      console.log(loggedUser)
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
