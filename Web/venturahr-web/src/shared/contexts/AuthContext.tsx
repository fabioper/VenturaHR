import React, { createContext, useContext, useEffect, useState } from "react"
import { useLoader } from "../hooks/useLoader"
import { LoginModel } from "../../core/dtos/login/LoginModel"
import { SignUpModel } from "../../core/dtos/signup/SignUpModel"
import { UserRole } from "../../core/enums/UserRole"
import * as authService from "../../core/services/AuthService"
import { UserProfile } from "../../core/models/UserProfile"

export interface AuthContextProps {
  user?: UserProfile
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginModel) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpModel) => Promise<void>
  loginUsingProvider: (
    providerId: authService.ProviderOptions,
    role: UserRole
  ) => Promise<void>
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  login: async () => {},
  logout: async () => {},
  signup: async () => {},
  loginUsingProvider: async () => {},
})

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<UserProfile>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, usingLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  async function loadUser(): Promise<void> {
    await usingLoader(async () => {
      const currentUser = await authService.getCurrentUser()
      const roleIsSet = currentUser?.roles && currentUser?.roles.length > 0
      roleIsSet && setUser(currentUser)
    })
  }

  useEffect(() => {
    const unsubscribe = authService.onAuthChange(loadUser)
    return () => unsubscribe()
  }, [])

  const login = async (credentials: LoginModel) => {
    await usingLoader(async () => {
      return authService.login(credentials)
    }, true)
  }

  const logout = async () => {
    await usingLoader(async () => {
      await authService.logout()
      setUser(undefined)
    })
  }

  const signup = async (credentials: SignUpModel) => {
    await usingLoader(async () => {
      await authService.signUp(credentials)
      await loadUser()
    }, true)
  }

  const loginUsingProvider = async (
    providerId: authService.ProviderOptions,
    role: UserRole
  ) => {
    await usingLoader(async () => {
      await authService.loginUsingProvider({ providerId, role })
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
        loginUsingProvider,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
