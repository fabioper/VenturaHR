import React, { createContext, useContext } from "react"

// eslint-disable-next-line @typescript-eslint/no-empty-interface
interface AuthContextProps {}

export const AuthContext = createContext<AuthContextProps>({})

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return <AuthContext.Provider value={{}}>{children}</AuthContext.Provider>
}

export default AuthProvider

export const useAuth = () => useContext(AuthContext)
