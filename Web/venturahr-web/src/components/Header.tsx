import Link from "next/link"
import React from "react"
import { useAuth } from "../contexts/AuthContext"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"

const Header: React.FC = () => {
  const { isLogged, logout } = useAuth()

  return (
    <header className="border-0 border-b border-slate-900 border-solid backdrop-blur-xl sticky top-0 left-0">
      <div className="container flex justify-between py-5 items-center">
        <Link href="/">
          <h1 className="font-bold cursor-pointer text-base m-0 transition-all hover:scale-110">
            VENTURAHR
          </h1>
        </Link>

        <ul className="list-none flex flex-row text-sm gap-2 m-0">
          {!isLogged ? (
            <>
              <li>
                <Link href="/applicant/login">
                  <Button
                    icon={PrimeIcons.SIGN_IN}
                    iconPos="right"
                    label="Entrar"
                    className="p-button-sm p-button-outlined p-button-raised"
                  />
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <Button
                  onClick={logout}
                  icon={PrimeIcons.SIGN_OUT}
                  iconPos="right"
                  label="Sair"
                  className="p-button-danger p-button-sm p-button-rounded p-button-outlined"
                />
              </li>
            </>
          )}
        </ul>
      </div>
    </header>
  )
}

export default Header
