import Link from "next/link"
import React from "react"
import { useAuth } from "../../contexts/AuthContext" // eslint-disable-next-line @typescript-eslint/no-empty-interface

// eslint-disable-next-line @typescript-eslint/no-empty-interface
interface HeaderProps {}

const Header: React.FC<HeaderProps> = () => {
  const { isLogged, logout } = useAuth()

  return (
    <header className=" bg-slate-800 text-sky-50">
      <div className="container flex justify-between py-5">
        <Link href="/">
          <h1 className="font-bold cursor-pointer">VENTURAHR</h1>
        </Link>

        <ul className="list-none flex flex-row text-sm gap-5">
          {!isLogged ? (
            <>
              <li>
                <Link href="/applicant/login">Entrar como Candidato</Link>
              </li>
              <li>
                <Link href="/company/login">Entrar como Empresa</Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <a
                  href="#"
                  onClick={async e => {
                    e.preventDefault()
                    await logout()
                  }}
                >
                  Sair
                </a>
              </li>
            </>
          )}
        </ul>
      </div>
    </header>
  )
}

export default Header
