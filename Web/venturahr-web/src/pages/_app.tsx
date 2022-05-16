import React from "react"
import "primereact/resources/themes/lara-dark-indigo/theme.css"
import "primereact/resources/primereact.min.css"
import "primeicons/primeicons.css"
import "../styles/global.scss"

import { AppProps } from "next/app"
import AuthProvider from "../shared/contexts/AuthContext"
import Header from "../shared/components/Header"

import "@fontsource/inter/300.css"
import "@fontsource/inter/400.css"
import "@fontsource/inter/600.css"
import "@fontsource/inter/700.css"
import "@fontsource/jost/300.css"
import "@fontsource/jost/400.css"
import "@fontsource/jost/700.css"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => {
  return (
    <AuthProvider>
      <Header />
      <Component {...pageProps} />
    </AuthProvider>
  )
}

export default MyApp
