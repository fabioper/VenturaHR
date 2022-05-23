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
  if (role) {
    useGuardAgainst(async ({ isLogged, user }) => {
      return isLogged && !(await user?.hasRole(role))
    })
  }

  const { loading } = useAuth()

  if (loading) {
    return loader || <div className="loader">Carregando...</div>
  }

  return <>{children}</>
}

export default ProtectedPage
