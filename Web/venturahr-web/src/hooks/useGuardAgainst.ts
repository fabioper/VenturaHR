import { AuthContextProps, useAuth } from "../contexts/AuthContext"
import { useRouter } from "next/router"
import { useEffect } from "react"

export function useGuardAgainst(
  guard: (auth: AuthContextProps) => boolean,
  redirect?: (auth: AuthContextProps) => Promise<string>
): void {
  const auth = useAuth()
  const router = useRouter()

  useEffect(() => {
    ;(async () => {
      guard(auth) &&
        (await router.push(
          redirect ? await redirect(auth) : await auth.redirectUser()
        ))
    })()
  }, [auth.user, guard])
}
