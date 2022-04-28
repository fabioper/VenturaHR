import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"
import { Button, Input } from "antd"
import { LoginOutlined } from "@ant-design/icons"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const { login } = useAuth()

  return (
    <main>
      <div className="container pt-16">
        <form className="w-4/12 bg-slate-400 text-white bg-opacity-5 mx-auto p-10 rounded-xl">
          <h1 className="mb-5 font-bold text-md text-center">
            Login Candidato
          </h1>
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

          <div className="my-5">
            <Button
              type="primary"
              size="large"
              className="w-full"
              icon={<LoginOutlined />}
              onClick={async () => await login({ email, password })}
            >
              Entrar
            </Button>
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
