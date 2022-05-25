import { NextPage } from "next"
import Link from "next/link"
import React from "react"
import { useGuardAgainst } from "../shared/hooks/useGuardAgainst"
import Head from "next/head"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { TabPanel, TabView } from "primereact/tabview"
import SignUpApplicant from "../shared/layout/sections/SignUpApplicant"
import SignUpCompany from "../shared/layout/sections/SignUpCompany"

const Signup: NextPage = () => {
  useGuardAgainst(async ({ isLogged }) => isLogged)

  return (
    <div>
      <Head>
        <title>Cadastro | VenturaHR</title>
      </Head>

      <div className="container">
        <header className="my-10 text-center">
          <h2 className="mt-0 mb-2 text-slate-50 text-4xl font-light font-display">
            Crie seu perfil
          </h2>
        </header>

        <div className="sm:w-full md:w-10/12 lg:w-5/12 bg-[#0d1424] mx-auto mb-10 p-10 rounded-xl bg-opacity-80">
          <TabView>
            <TabPanel header="Candidato">
              <SignUpApplicant />
            </TabPanel>
            <TabPanel header="Empresa">
              <SignUpCompany />
            </TabPanel>
          </TabView>

          <div className="flex flex-col items-center">
            <Link href="/login">
              <Button
                type="button"
                className="p-button-text p-button-rounded p-button-sm mt-2"
                icon={PrimeIcons.ARROW_RIGHT}
                iconPos="right"
                label="Já tenho cadastro"
              />
            </Link>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Signup
