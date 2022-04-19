import React from "react"
import "../styles/global.scss"

import Layout from "../layouts/Layout/Layout"
import { AppProps } from "next/app"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => (
  <Layout>
    <Component {...pageProps} />
  </Layout>
)

export default MyApp
