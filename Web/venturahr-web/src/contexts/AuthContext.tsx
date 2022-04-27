import React, { createContext, useContext, useEffect, useState } from "react"
import {
  Auth,
  AuthProvider,
  createUserWithEmailAndPassword,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
  User,
} from "firebase/auth"
import { getFunctions, httpsCallable } from "firebase/functions"
import { firebaseApp } from "../config/firebase/firebase.config"

export interface LoginCredentials {
  email: string
  password: string
}

export interface AuthContextProps {
  user?: User
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginCredentials) => Promise<any>
  logout: () => Promise<any>

  signup(credentials: SignUpCredentials): Promise<void>

  signupWithProvider(provider: AuthProvider, role: string): Promise<void>

  redirectUser(): Promise<string>
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  login: async () => {},
  logout: async () => {},
  signup: async () => {},
  signupWithProvider: async () => {},
  redirectUser: async () => "",
})

interface SignUpCredentials {
  email: string
  password: string
  role: string
  displayName: string
}

const AuthProvider: React.FC<{ auth: Auth; children: React.ReactNode }> = ({
  auth,
  children,
}) => {
  const [user, setUser] = useState<User>()
  const [loading, setLoading] = useState<boolean>(true)
  const [isLogged, setIsLogged] = useState(false)

  useEffect(() => setIsLogged(!!user), [user])

  useEffect(() => {
    const unsubscribe = auth.onAuthStateChanged(async () => {
      setUser(auth.currentUser || undefined)
    })
    return () => unsubscribe()
  }, [])

  const tryWithLoader = async (callback: () => Promise<any>) => {
    try {
      setLoading(true)
      await callback()
    } catch (e) {
      console.log(e)
    } finally {
      setLoading(false)
    }
  }

  const login = async ({ email, password }: LoginCredentials) => {
    await tryWithLoader(
      async () => await signInWithEmailAndPassword(auth, email, password)
    )
  }

  const logout = async () => {
    await tryWithLoader(async () => await signOut(auth))
  }

  async function ensureUserRole(email: string, role: string): Promise<void> {
    const functions = getFunctions(firebaseApp)
    const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")
    await assignRoleToUser({ email, role })
  }

  const signup = async ({ email, password, role }: SignUpCredentials) => {
    await tryWithLoader(async () => {
      const { user } = await createUserWithEmailAndPassword(
        auth,
        email,
        password
      )
      await ensureUserRole(user.email || email, role)
      await setUser(auth.currentUser || undefined)
    })
  }

  const signupWithProvider = async (provider: AuthProvider, role: string) => {
    await tryWithLoader(async () => {
      const { user } = await signInWithPopup(auth, provider)
      await ensureUserRole(user.email || "", role)
      await signInWithPopup(auth, provider)
    })
  }

  async function redirectUser(): Promise<string> {
    const token = await auth.currentUser?.getIdTokenResult(true)
    const userRole = (token?.claims.role as string[]) || []

    if (!userRole || !userRole.length) {
      return "/"
    }

    if (userRole.includes("applicant")) {
      return "/applicant/dashboard"
    }
    if (userRole.includes("company")) {
      return "/company/dashboard"
    }

    throw Error("Unrecognizable role")
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        loading,
        login,
        logout,
        isLogged,
        signup,
        signupWithProvider,
        redirectUser,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
