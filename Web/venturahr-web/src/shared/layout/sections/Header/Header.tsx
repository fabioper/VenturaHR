import Link from "next/link"
import React, { useMemo, useRef } from "react"
import { useAuth } from "../../../contexts/AuthContext"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { Avatar } from "primereact/avatar"
import { Menu } from "primereact/menu"
import { MenuItem } from "primereact/menuitem"
import { useRouter } from "next/router"
import { Skeleton } from "primereact/skeleton"
import { useHeaderTransparency } from "../../../hooks/useHeaderTransparency"

const Header: React.FC = () => {
  const { isLogged, logout, user, loading } = useAuth()
  const router = useRouter()
  const menu = useRef<Menu>(null)
  const header = useHeaderTransparency()

  const items: MenuItem[] = useMemo(
    () => [
      {
        label: "Sair",
        icon: PrimeIcons.SIGN_OUT,
        command: logout,
      },
    ],
    []
  )

  return (
    <header
      ref={header}
      className="sticky border-0 border-b border-solid border-b-slate-800 bg-[#0d1424] transition bg-opacity-20 top-0 w-full backdrop-blur bg-transparent z-10"
    >
      <div className="container flex justify-between py-3 items-center">
        <Link href="/">
          <h1 className="font-extrabold cursor-pointer text-lg m-0 transition-all hover:scale-110">
            venturahr
          </h1>
        </Link>

        {loading ? (
          <ul className="list-none m-0 p-0 flex gap-5">
            <li>
              <Skeleton width="80px" height="25px" />
            </li>
            <li>
              <Skeleton width="80px" height="25px" />
            </li>
          </ul>
        ) : !isLogged ? (
          <ul className="list-none m-0 p-0 flex gap-2">
            {router.pathname !== "/login" && (
              <li>
                <Link href="/login">
                  <Button
                    iconPos="right"
                    label="Entrar"
                    className="p-button-sm p-button-outlined p-button-rounded"
                  />
                </Link>
              </li>
            )}
            {router.pathname !== "/auth" && (
              <li>
                <Link href="/signup" passHref>
                  <Button
                    iconPos="right"
                    label="Cadastre-se"
                    className="p-button-sm p-button-rounded p-button-raised"
                  />
                </Link>
              </li>
            )}
          </ul>
        ) : (
          <ul className="list-none m-0 p-0 flex gap-2">
            {router.pathname !== "/jobpostings" && (
              <li>
                <Link href="/jobpostings/">
                  <Button
                    label="Procurar vagas"
                    icon={PrimeIcons.SEARCH}
                    className="p-button-rounded p-button-sm p-button-text"
                  />
                </Link>
              </li>
            )}
            <li>
              <Button
                type="button"
                className="user-button rounded-full p-0"
                onClick={menu.current?.toggle}
              >
                <Avatar
                  label={user?.name?.[0]}
                  shape="circle"
                  size="normal"
                  imageAlt={`Imagem do usu??rio ${user?.name}`}
                />
                <span className="text-sm mx-2 text-slate-300">
                  {user?.name}
                </span>
                <i className={`${PrimeIcons.ANGLE_DOWN} mr-2 text-slate-600`} />
              </Button>
            </li>
          </ul>
        )}
      </div>
      <Menu model={items} popup ref={menu} />
    </header>
  )
}

export default Header
