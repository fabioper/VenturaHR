import React from "react"
import "primereact/resources/themes/lara-dark-blue/theme.css"
import "primereact/resources/primereact.min.css"
import "primeicons/primeicons.css"
import "primeflex/primeflex.min.css"
import "../styles/global.scss"

import Layout from "../layouts/Layout/Layout"
import { AppProps } from "next/app"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => (
  <Layout>
    <Component {...pageProps} />
  </Layout>
)

export default MyApp
