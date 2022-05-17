import React, { createContext, useContext, useEffect, useState } from "react"
import { AuthUser } from "../../core/models/AuthUser"
import { useLoader } from "../hooks/useLoader"
import { LoginModel } from "../../core/dtos/login/LoginModel"
import { SignUpModel } from "../../core/dtos/signup/SignUpModel"
import { UserRole } from "../../core/enums/UserRole"
import * as firebaseService from "../../core/services/FirebaseAuthService"

export interface AuthContextProps {
  user?: AuthUser
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginModel) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpModel) => Promise<void>
  loginUsingProvider: (
    providerId: firebaseService.ProviderOptions,
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
  const [user, setUser] = useState<AuthUser>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, usingLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  async function loadUser(): Promise<void> {
    await usingLoader(async () => {
      const currentUser = await firebaseService.getCurrentUser()
      const roleIsSet = currentUser?.roles && currentUser?.roles.length > 0
      roleIsSet && setUser(currentUser)
    })
  }

  useEffect(() => {
    const unsubscribe = firebaseService.onAuthChange(loadUser)
    return () => unsubscribe()
  }, [])

  const login = async (credentials: LoginModel) => {
    await usingLoader(async () => {
      return firebaseService.login(credentials)
    }, true)
  }

  const logout = async () => {
    await usingLoader(async () => {
      await firebaseService.logout()
      setUser(undefined)
    })
  }

  const signup = async (credentials: SignUpModel) => {
    await usingLoader(async () => {
      await firebaseService.signUp(credentials)
      await loadUser()
    }, true)
  }

  const loginUsingProvider = async (
    providerId: firebaseService.ProviderOptions,
    role: UserRole
  ) => {
    await usingLoader(async () => {
      await firebaseService.loginUsingProvider({ providerId, role })
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
