import React from "react"
import "primereact/resources/themes/lara-dark-indigo/theme.css"
import "primereact/resources/primereact.min.css"
import "primeicons/primeicons.css"
import "../styles/global.scss"

import { AppProps } from "next/app"
import AuthProvider from "../contexts/AuthContext"
import { getAuth } from "firebase/auth"
import { firebaseApp } from "../config/firebase/firebase.config"
import Header from "../components/Header"

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
