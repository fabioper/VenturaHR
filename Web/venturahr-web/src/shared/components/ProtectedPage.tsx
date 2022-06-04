import React from "react"
import { useAuth } from "../contexts/AuthContext"
import { useGuardAgainst } from "../hooks/useGuardAgainst"
import { UserType } from "../../core/enums/UserType"

interface ProtectedPageProps {
  role?: UserType
  children: React.ReactNode
  loader?: JSX.Element
}

const ProtectedPage: React.FC<ProtectedPageProps> = ({
  role,
  loader,
  children,
}) => {
  if (role !== undefined) {
    useGuardAgainst(async ({ isLogged, user }) => {
      return !isLogged || !(await user?.hasRole(role))
    })
  }

  const { loading } = useAuth()

  if (loading) {
    return (
      loader || (
        <div className="container text-center h-1/2 w-screen flex justify-center items-center">
          <div className="loader text-slate-700">Carregando...</div>
        </div>
      )
    )
  }

  return <>{children}</>
}

export default ProtectedPage
