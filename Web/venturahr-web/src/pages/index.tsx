import type { NextPage } from "next"
import Head from "next/head"

import { Button } from "primereact/button"
import Link from "next/link"
import { PrimeIcons } from "primereact/api"

const Home: NextPage = () => {
  return (
    <div>
      <Head>
        <title>Ventura HR</title>
        <meta name="description" content="VenturaHR description" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main>
        <div className="container">
          <div className="p-buttonset">
            <Link href="companies/signup">
              <Button
                label="Cadastrar Empresa"
                icon={PrimeIcons.BUILDING}
                className="p-button-sm p-button-outlined p-button-success"
              />
            </Link>
            <Link href="applicants/signup">
              <Button
                label="Cadastrar Candidato"
                icon={PrimeIcons.USER}
                className="p-button-sm p-button-outlined p-button-info"
              />
            </Link>
          </div>
        </div>
      </main>
    </div>
  )
}

export default Home
