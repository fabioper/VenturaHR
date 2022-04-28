import React from "react"
import { useAuth } from "../contexts/AuthContext"
import { useGuardAgainst } from "../hooks/useGuardAgainst"

interface ProtectedPageProps {
  onlyRoles?: string[]
  children: React.ReactNode
}

const ProtectedPage: React.FC<ProtectedPageProps> = ({
  onlyRoles = [],
  children,
}) => {
  if (onlyRoles.length > 0) {
    useGuardAgainst(
      async ({ isLogged, user }) =>
        isLogged && !(await user?.hasRole(...onlyRoles))
    )
  }

  const { loading } = useAuth()

  if (loading) {
    return <div className="loader">Carregando...</div>
  }

  return <>{children}</>
}

export default ProtectedPage
