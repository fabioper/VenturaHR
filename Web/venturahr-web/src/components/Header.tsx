import Link from "next/link"
import React, { useRef } from "react"
import { useAuth } from "../contexts/AuthContext"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { Avatar } from "primereact/avatar"
import { Menu } from "primereact/menu"
import { MenuItem } from "primereact/menuitem"

const Header: React.FC = () => {
  const { isLogged, logout, user } = useAuth()
  const menu = useRef<Menu>(null)

  const items: MenuItem[] = [
    {
      label: "Sair",
      icon: PrimeIcons.SIGN_OUT,
      command: logout,
    },
  ]
  return (
    <header className="sticky border-0 border-b border-solid border-b-slate-800 top-0 z-40 w-full backdrop-blur bg-opacity-5">
      <div className="container flex justify-between py-3 items-center">
        <Link href="/">
          <h1 className="font-extrabold cursor-pointer text-lg m-0 transition-all hover:scale-110">
            venturahr
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
                    className="p-button-sm p-button-outlined p-button-rounded"
                  />
                </Link>
              </li>
            </>
          ) : (
            <>
              <Button
                type="button"
                className="user-button rounded-full p-0"
                onClick={menu.current?.toggle}
              >
                <Avatar
                  image={user?.photoUrl}
                  label={!user?.photoUrl ? user?.name?.[0] : undefined}
                  shape="circle"
                  size="normal"
                  imageAlt={`Imagem do usuário ${user?.name}`}
                />
                <span className="text-sm mx-2 text-slate-300">
                  {user?.name}
                </span>
                <i className={`${PrimeIcons.ANGLE_DOWN} mr-2 text-slate-600`} />
              </Button>
            </>
          )}
        </ul>
      </div>
      <Menu model={items} popup ref={menu} />
    </header>
  )
}

export default Header
