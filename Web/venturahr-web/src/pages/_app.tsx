import React from "react"
import "antd/dist/antd.dark.css"
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
