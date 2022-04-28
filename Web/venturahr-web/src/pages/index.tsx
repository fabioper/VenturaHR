import type { NextPage } from "next"
import Head from "next/head"
import Link from "next/link"
import { useAuth } from "../contexts/AuthContext"
import ProtectedPage from "../components/ProtectedPage"

const Home: NextPage = () => {
  const { user, isLogged, logout } = useAuth()

  return (
    <ProtectedPage>
      <div>
        <Head>
          <title>Ventura HR</title>
          <meta name="description" content="VenturaHR description" />
          <link rel="icon" href="/favicon.ico" />
        </Head>

        <main>
          <div className="container">Content</div>
          {!isLogged ? (
            <div>
              <p>
                <Link href="/applicant/login">Entrar como candidato</Link>
              </p>
              <p>
                <Link href="/company/login">Entrar como empresa</Link>
              </p>
            </div>
          ) : (
            <div>
              <p>Hello, {user?.name}</p>
              <a href="/" onClick={() => logout()}>
                Sair
              </a>
            </div>
          )}
        </main>
      </div>
    </ProtectedPage>
  )
}

export default Home
