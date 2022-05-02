import Link from "next/link"
import React, { useEffect, useMemo, useRef } from "react"
import { useAuth } from "../contexts/AuthContext"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { Avatar } from "primereact/avatar"
import { Menu } from "primereact/menu"
import { MenuItem } from "primereact/menuitem"
import { useRouter } from "next/router"

const Header: React.FC = () => {
  const { isLogged, logout, user } = useAuth()
  const router = useRouter()
  const menu = useRef<Menu>(null)
  const header = useRef<HTMLDivElement>(null)

  useEffect(() => {
    window.addEventListener("scroll", () => {
      if (header.current) {
        if (window.scrollY > 100) {
          header.current.classList.remove("bg-transparent")
        } else {
          header.current.classList.add("bg-transparent")
        }
      }
    })
  }, [])

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
      className="sticky border-0 border-b border-solid border-b-slate-800 bg-[#0d1424] transition bg-opacity-20 top-0 w-full backdrop-blur bg-transparent"
    >
      <div className="container flex justify-between py-3 items-center">
        <Link href="/">
          <h1 className="font-extrabold cursor-pointer text-lg m-0 transition-all hover:scale-110">
            venturahr
          </h1>
        </Link>

        {!isLogged ? (
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
            {router.pathname !== "/signup" && (
              <li>
                <Link href="/signup" passHref>
                  <Button
                    iconPos="right"
                    label="Cadastre-se"
                    className="p-button-sm p-button-rounded p-button-raised p-button-shadowed"
                  />
                </Link>
              </li>
            )}
          </ul>
        ) : (
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
            <span className="text-sm mx-2 text-slate-300">{user?.name}</span>
            <i className={`${PrimeIcons.ANGLE_DOWN} mr-2 text-slate-600`} />
          </Button>
        )}
      </div>
      <Menu model={items} popup ref={menu} />
    </header>
  )
}

export default Header
