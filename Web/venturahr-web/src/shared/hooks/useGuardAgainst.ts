import { AuthContextProps, useAuth } from "../contexts/AuthContext"
import { useRouter } from "next/router"
import { useEffect } from "react"

export function useGuardAgainst(
  rule: (auth: AuthContextProps) => Promise<boolean>,
  redirect?: (auth: AuthContextProps) => Promise<string>
): void {
  const auth = useAuth()
  const router = useRouter()

  async function redirectUser(): Promise<void> {
    const redirectPath = redirect
      ? await redirect(auth)
      : auth.user?.redirectPage
    await router.push(redirectPath || "/")
  }

  useEffect(() => {
    ;(async () => {
      const ruleIsSatisfied = await rule(auth)
      if (ruleIsSatisfied) {
        await redirectUser()
      }
    })()
  }, [auth.user, rule])
}
