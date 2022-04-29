import React, { createContext, useContext, useEffect, useState } from "react"
import {
  Auth,
  AuthProvider,
  createUserWithEmailAndPassword,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
  updateProfile,
} from "firebase/auth"
import { AuthUser } from "./AuthUser"
import { useLoader } from "../hooks/useLoader"
import { formatUser, isNewUser, setUserRole } from "../utils/auth.utils"
import { LoginDto } from "../core/dtos/LoginDto"
import { SignUpDto } from "../core/dtos/SignUpDto"
import { UserRole } from "../core/enums/UserRole"

export interface AuthContextProps {
  user?: AuthUser
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginDto) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpDto) => Promise<void>
  loginWithProvider: (provider: AuthProvider, role: UserRole) => Promise<void>
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

  async function loadUser(): Promise<void> {
    await withLoader(async () => {
      const currentUser = auth.currentUser
      const user = currentUser ? await formatUser(currentUser) : undefined
      if (user?.roles && user?.roles.length > 0) {
        setUser(user)
      }
    })
  }

  useEffect(() => {
    const unsubscribe = auth.onAuthStateChanged(async () => await loadUser())
    return () => unsubscribe()
  }, [])

  const login = async ({ email, password }: LoginDto) => {
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

  const signup = async (credentials: SignUpDto) => {
    await withLoader(async () => {
      const { user } = await createUserWithEmailAndPassword(
        auth,
        credentials.email,
        credentials.password
      )
      await setUserRole(user.email || credentials.email, credentials.role)
      await updateProfile(user, { displayName: credentials.displayName })
      await loadUser()
    })
  }

  const loginWithProvider = async (provider: AuthProvider, role: UserRole) => {
    await withLoader(async () => {
      const { user } = await signInWithPopup(auth, provider)
      isNewUser(user) && (await setUserRole(user.uid, role))
      await loadUser()
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
