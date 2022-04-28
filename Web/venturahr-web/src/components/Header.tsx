import Link from "next/link"
import React from "react"
import { useAuth } from "../contexts/AuthContext"
import { Button } from "antd"
import { UserOutlined } from "@ant-design/icons"

const Header: React.FC = () => {
  const { isLogged, logout } = useAuth()

  return (
    <header className="border-0 border-b border-slate-800 border-solid">
      <div className="container flex justify-between py-5 items-center">
        <Link href="/">
          <h1 className="font-bold cursor-pointer text-base m-0">VENTURAHR</h1>
        </Link>

        <ul className="list-none flex flex-row text-sm gap-2 m-0">
          {!isLogged ? (
            <>
              <li>
                <Link href="/company/login">
                  <Button icon={<UserOutlined />} className="rounded">
                    Entrar como Empresa
                  </Button>
                </Link>
              </li>
              <li>
                <Link href="/applicant/login">
                  <Button
                    type="primary"
                    icon={<UserOutlined />}
                    className="rounded"
                  >
                    Entrar como Candidato
                  </Button>
                </Link>
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
