import React, { createContext, useContext, useEffect, useState } from "react"
import { AuthUser } from "../../core/models/AuthUser"
import { useLoader } from "../hooks/useLoader"
import { LoginModel } from "../../core/dtos/LoginModel"
import { SignUpModel } from "../../core/dtos/SignUpModel"
import { UserRole } from "../../core/enums/UserRole"
import {
  getCurrentUser,
  onAuthChange,
  ProviderOptions,
  signInUser,
  signInWithProvider,
  signOutUser,
  signUpUser,
} from "../../core/services/auth.service"

export interface AuthContextProps {
  user?: AuthUser
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginModel) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpModel) => Promise<void>
  signInUserUsingSocialProvider: (
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
  signInUserUsingSocialProvider: async () => {},
})

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<AuthUser>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, usingLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  async function loadUser(): Promise<void> {
    await usingLoader(async () => {
      const currentUser = await getCurrentUser()
      const roleIsSet = currentUser?.roles && currentUser?.roles.length > 0
      roleIsSet && setUser(currentUser)
    })
  }

  useEffect(() => {
    const unsubscribe = onAuthChange(loadUser)
    return () => unsubscribe()
  }, [])

  const login = async (credentials: LoginModel) => {
    await usingLoader(async () => {
      return signInUser(credentials)
    }, true)
  }

  const logout = async () => {
    await usingLoader(async () => {
      await signOutUser()
      setUser(undefined)
    })
  }

  const signup = async (credentials: SignUpModel) => {
    await usingLoader(async () => {
      await signUpUser(credentials)
      await loadUser()
    }, true)
  }

  const signInUserUsingSocialProvider = async (
    providerId: ProviderOptions,
    role: UserRole
  ) => {
    await usingLoader(async () => {
      await signInWithProvider({ providerId, role })
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
        signInUserUsingSocialProvider,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
