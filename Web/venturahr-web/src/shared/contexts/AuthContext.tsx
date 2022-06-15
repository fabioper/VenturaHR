import React, { createContext, useContext, useEffect, useState } from "react"
import { useLoader } from "../hooks/useLoader"
import * as authService from "../../core/services/AuthService"
import { onAuthChange } from "../../core/services/AuthService"
import { UserProfile } from "../../core/models/UserProfile"
import { LoginRequest } from "../../core/dtos/requests/LoginRequest"
import { SignUpRequest } from "../../core/dtos/requests/SignUpRequest"

export interface AuthContextProps {
  user?: UserProfile
  loading: boolean
  isLogged: boolean
  login: (credentials: LoginRequest) => Promise<any>
  logout: () => Promise<any>
  signup: (credentials: SignUpRequest) => Promise<void>
}

export const AuthContext = createContext<AuthContextProps>({
  loading: true,
  isLogged: false,
  login: async () => {},
  logout: async () => {},
  signup: async () => {},
})

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<UserProfile>()
  const [isLogged, setIsLogged] = useState(false)
  const { loading, usingLoader } = useLoader()

  useEffect(() => setIsLogged(!!user), [user])

  useEffect(() => {
    const unsubscribe = onAuthChange(checkAuthState)
    return () => unsubscribe()
  }, [])

  async function loadUser(): Promise<void> {
    const currentUser = await authService.getCurrentUser()
    setUser(currentUser)
  }

  async function checkAuthState() {
    await usingLoader(loadUser)
  }

  const login = async (credentials: LoginRequest) => {
    await usingLoader(async () => await authService.login(credentials), true)
  }

  const logout = async () => {
    await usingLoader(async () => await authService.logout())
  }

  const signup = async (model: SignUpRequest) => {
    await usingLoader(async () => {
      await authService.createUser(model)
      await authService.login({ ...model })
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
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
