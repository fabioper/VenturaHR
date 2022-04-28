import React from "react"
import "../styles/global.scss"

import { AppProps } from "next/app"
import AuthProvider from "../contexts/AuthContext"
import { getAuth } from "firebase/auth"
import { firebaseApp } from "../config/firebase/firebase.config"
import Header from "../components/Header/Header"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => {
  const auth = getAuth(firebaseApp)
  auth.useDeviceLanguage()

  return (
    <AuthProvider auth={auth}>
      <Header />
      <Component {...pageProps} />
    </AuthProvider>
  )
}

export default MyApp
