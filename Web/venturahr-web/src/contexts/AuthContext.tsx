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
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  async login() {},
  async logout() {},
  async signup() {},
  async signupWithProvider(): Promise<void> {},
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

  useEffect(() => {
    setIsLogged(!!user)
  }, [user])

  useEffect(() => {
    const updateUser = (state: User | null) => setUser(state || undefined)
    const unsubscribeAuthStateChanged = auth.onAuthStateChanged(updateUser)
    const unsubscribeIdTokenChanged = auth.onIdTokenChanged(updateUser)

    return () => {
      unsubscribeAuthStateChanged()
      unsubscribeIdTokenChanged()
    }
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
    await tryWithLoader(async () => {
      const { user } = await signInWithEmailAndPassword(auth, email, password)
      setUser(user)
    })
  }

  const logout = async () => {
    await tryWithLoader(async () => {
      await signOut(auth)
      setUser(undefined)
    })
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
      await login({ email, password })
    })
  }

  const signupWithProvider = async (provider: AuthProvider, role: string) => {
    await tryWithLoader(async () => {
      const { user } = await signInWithPopup(auth, provider)
      await ensureUserRole(user.email || "", role)
      await signInWithPopup(auth, provider)
    })
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
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
