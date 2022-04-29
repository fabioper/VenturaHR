import React, { createContext, useContext, useEffect, useState } from "react"
import {
  Auth,
  AuthProvider,
  createUserWithEmailAndPassword,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
  updateProfile,
  User,
} from "firebase/auth"
import { getFunctions, httpsCallable } from "firebase/functions"
import { firebaseApp } from "../config/firebase/firebase.config"
import { AuthUser } from "./AuthUser"
import { useLoader } from "../hooks/useLoader"
import { toAuthUser } from "../utils/toAuthUser"

export interface LoginCredentials {
  email: string
  password: string
}

export interface AuthContextProps {
  user?: AuthUser
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginCredentials) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpCredentials) => Promise<void>
  loginWithProvider: (provider: AuthProvider, role: string) => Promise<void>
}

interface SignUpCredentials {
  email: string
  password: string
  role: string
  displayName: string
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  login: async () => {},
  logout: async () => {},
  signup: async () => {},
  loginWithProvider: async () => {},
})

const AuthProvider: React.FC<{ auth: Auth; children: React.ReactNode }> = ({
  auth,
  children,
}) => {
  const [user, setUser] = useState<AuthUser>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, withLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  async function reloadUser(): Promise<void> {
    await withLoader(async () => {
      const currentUser = auth.currentUser
      const user = currentUser ? await toAuthUser(currentUser) : undefined
      if (user?.roles && user?.roles.length > 0) {
        setUser(user)
      }
    })
  }

  useEffect(() => {
    const unsubscribe = auth.onAuthStateChanged(async () => await reloadUser())
    return () => unsubscribe()
  }, [])

  const login = async ({ email, password }: LoginCredentials) => {
    await withLoader(
      async () => await signInWithEmailAndPassword(auth, email, password),
      true
    )
  }

  const logout = async () => {
    await withLoader(async () => {
      await signOut(auth)
      setUser(undefined)
    })
  }

  const ensureUserRole = async (email: string, role: string): Promise<void> => {
    const functions = getFunctions(firebaseApp)
    const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")
    await assignRoleToUser({ email, role })
  }

  const fillProfile = async (user: User, credentials: SignUpCredentials) => {
    await ensureUserRole(user.email || credentials.email, credentials.role)
    await updateProfile(user, { displayName: credentials.displayName })
    await reloadUser()
  }

  const signup = async (credentials: SignUpCredentials) => {
    await withLoader(async () => {
      const { user } = await createUserWithEmailAndPassword(
        auth,
        credentials.email,
        credentials.password
      )
      await fillProfile(user, credentials)
    })
  }

  const loginWithProvider = async (provider: AuthProvider, role: string) => {
    await withLoader(async () => {
      const { user } = await signInWithPopup(auth, provider)
      await ensureUserRole(user.email || "", role)
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
        loginWithProvider,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
