import { AuthContextProps, useAuth } from "../contexts/AuthContext"
import { useRouter } from "next/router"
import { useEffect } from "react"

export function useGuardAgainst(
  guard: (auth: AuthContextProps) => boolean,
  redirect?: string
): void {
  const auth = useAuth()
  const router = useRouter()

  useEffect(() => {
    guard(auth) && router.push(redirect || "/").then()
  }, [auth.user, guard])
}
