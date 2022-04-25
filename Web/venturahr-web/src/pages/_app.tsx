import React from "react"
import "../styles/global.scss"

import { AppProps } from "next/app"

const MyApp: React.FC<AppProps> = ({ Component, pageProps }: AppProps) => (
  <Component {...pageProps} />
)

export default MyApp
