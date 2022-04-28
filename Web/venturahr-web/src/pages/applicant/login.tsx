import Link from "next/link"
import React, { useState } from "react"
import { useAuth } from "../../contexts/AuthContext"
import { NextPage } from "next"
import { useGuardAgainst } from "../../hooks/useGuardAgainst"
import { Input, InputGroup, InputRightElement } from "@chakra-ui/input"
import {
  Alert,
  AlertDescription,
  AlertIcon,
  Button,
  Icon,
} from "@chakra-ui/react"
import { MdLogin } from "react-icons/md"

const Login: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState<string | null>()
  const [showPassword, setShowPassword] = useState(false)
  const [loading, setLoading] = useState(false)

  const { login } = useAuth()

  async function handleLogin(): Promise<any> {
    setError(null)
    setLoading(true)
    try {
      return await login({ email, password })
    } catch (e) {
      setError("Verifique suas credenciais.")
    } finally {
      setLoading(false)
    }
  }

  return (
    <main>
      <div className="container pt-16">
        <h2 className="mb-7 text-center text-3xl font-bold">
          Entrar como Candidato
        </h2>

        <form className="w-4/12 bg-slate-400 text-white bg-opacity-5 mx-auto p-10 rounded-xl">
          {!!error && (
            <div className="mb-5 flex flex-col gap-4">
              <Alert status="error">
                <AlertIcon />
                <AlertDescription>{error}</AlertDescription>
              </Alert>
            </div>
          )}
          <div className="mb-3">
            <label className="block mb-1 text-sm">Email:</label>
            <Input value={email} onChange={e => setEmail(e.target.value)} />
          </div>

          <div>
            <label className="block mb-1">Senha:</label>
            <InputGroup size="md">
              <Input
                pr="4.5rem"
                type={showPassword ? "text" : "password"}
                placeholder="Enter password"
                value={password}
                onChange={e => setPassword(e.target.value)}
              />
              <InputRightElement width="4.5rem">
                <Button
                  h="1.75rem"
                  size="sm"
                  onClick={() => setShowPassword(show => !show)}
                >
                  {showPassword ? "Hide" : "Show"}
                </Button>
              </InputRightElement>
            </InputGroup>
          </div>

          <div className="my-7">
            <Button
              onClick={handleLogin}
              isLoading={loading}
              loadingText="Enviando"
              colorScheme="teal"
              size="lg"
              width="100%"
              rightIcon={<Icon as={MdLogin} />}
            >
              Entrar
            </Button>
          </div>

          <div className="border-0 border-solid border-t border-slate-700 pt-5 text-center">
            Ainda não tem conta?
            <div>
              <Link href="/applicant/signup">
                <Button variant="link" colorScheme="linkedin" className="mt-2">
                  Crie sua conta
                </Button>
              </Link>
            </div>
          </div>
        </form>
      </div>
    </main>
  )
}

export default Login
