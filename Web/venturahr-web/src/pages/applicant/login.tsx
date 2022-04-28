import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"
import { Alert, Button, Input } from "antd"
import { LoginOutlined } from "@ant-design/icons"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [errors, setErrors] = useState<string[]>([])
  const [loading, setLoading] = useState(false)

  const { login } = useAuth()

  async function handleLogin(): Promise<any> {
    setLoading(true)
    try {
      return await login({ email, password })
    } catch (e) {
      setErrors(prev => [...prev, "Verifique suas credenciais."])
    } finally {
      setLoading(false)
    }
  }

  return (
    <main>
      <div className="container pt-16">
        <form className="w-4/12 bg-slate-400 text-white bg-opacity-5 mx-auto p-10 rounded-xl">
          <h2 className="mb-7 text-center">Entrar como Candidato</h2>
          {errors.length > 0 && (
            <div className="my-5 flex flex-col gap-4">
              {errors.map((error, index) => (
                <Alert
                  key={index}
                  showIcon
                  type="warning"
                  closable
                  description={error}
                  onClose={() => setErrors(e => e.filter(e => e !== error))}
                />
              ))}
            </div>
          )}
          <div className="mb-3">
            <label className="block mb-1 text-sm">Email:</label>
            <Input value={email} onChange={e => setEmail(e.target.value)} />
          </div>

          <div>
            <label className="block mb-1">Senha:</label>
            <Input.Password
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
          </div>

          <div className="my-7">
            <Button
              type="primary"
              size="large"
              block
              icon={<LoginOutlined />}
              onClick={handleLogin}
              loading={loading}
              className="rounded"
            >
              Entrar
            </Button>
          </div>

          <div className="mt-6 border-0 border-solid border-t border-slate-800 pt-5 text-center">
            Ainda não tem conta?
            <div>
              <Link href="/applicant/signup">
                <span className="underline underline-offset-4 cursor-pointer py-2 mx-auto hover:opacity-80 hover:text-cyan-400">
                  Cadastre-se
                </span>
              </Link>
            </div>
          </div>
        </form>
      </div>
    </main>
  )
}

export default Login
