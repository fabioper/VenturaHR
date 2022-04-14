import React from "react"
import Layout from "../Layouts/Layout/Layout"
import { AppProps } from "next/app"
import "../styles/global.scss"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => (
  <Layout>
    <Component {...pageProps} />
  </Layout>
)

export default MyApp
