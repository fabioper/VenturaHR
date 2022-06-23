import React from "react"
import { useAuth } from "../../contexts/AuthContext"
import { UserType } from "../../../core/enums/UserType"
import { useRouter } from "next/router"

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
  const { loading, isLogged, user } = useAuth()
  const router = useRouter()

  if (loading) {
    return (
      loader || (
        <div className="container text-center h-1/2 w-screen flex justify-center items-center">
          <div className="loader text-slate-700">Carregando...</div>
        </div>
      )
    )
  }

  if (!isLogged && !loading) {
    router.push("/login").then()
  }

  if (user && role !== undefined && !user.hasRole(role)) {
    router.push(user.redirectPage).then()
  }

  return <>{children}</>
}

export default ProtectedPage
