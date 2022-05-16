import React, { createContext, useContext, useEffect, useState } from "react"
import { AuthUser } from "./AuthUser"
import { useLoader } from "../hooks/useLoader"
import { LoginDto } from "../core/dtos/LoginDto"
import { SignUpDto } from "../core/dtos/SignUpDto"
import { UserRole } from "../core/enums/UserRole"
import {
  getCurrentUser,
  onAuthChange,
  ProviderOptions,
  signInUser,
  signInWithProvider,
  signOutUser,
  signUp,
} from "../core/services/auth.service"

export interface AuthContextProps {
  user?: AuthUser
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginDto) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpDto) => Promise<void>
  loginWithProvider: (
    providerId: ProviderOptions,
    role: UserRole
  ) => Promise<void>
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  login: async () => {},
  logout: async () => {},
  signup: async () => {},
  loginWithProvider: async () => {},
})

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<AuthUser>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, withLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  async function loadUser(): Promise<void> {
    await withLoader(async () => {
      const currentUser = getCurrentUser()
      const user = currentUser ? await currentUser : undefined
      if (user?.roles && user?.roles.length > 0) {
        setUser(user)
      }
    })
  }

  useEffect(() => {
    const unsubscribe = onAuthChange(loadUser)
    return () => unsubscribe()
  }, [])

  const login = async (credentials: LoginDto) => {
    await withLoader(async () => signInUser(credentials), true)
  }

  const logout = async () => {
    await withLoader(async () => {
      await signOutUser()
      setUser(undefined)
    })
  }

  const signup = async (credentials: SignUpDto) => {
    await withLoader(async () => {
      await signUp(credentials)
      await loadUser()
    }, true)
  }

  const loginWithProvider = async (
    providerId: ProviderOptions,
    role: UserRole
  ) => {
    await withLoader(async () => {
      await signInWithProvider(providerId, role)
      await loadUser()
    }, true)
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
