import type { NextPage } from "next"
import Head from "next/head"
import { useAuth } from "../contexts/AuthContext"
import ProtectedPage from "../components/ProtectedPage"
import { useRouter } from "next/router"

const Home: NextPage = () => {
  const { user, isLogged, logout } = useAuth()
  const router = useRouter()

  if (isLogged && user) {
    router.push(user.redirectPage).then()
  }

  return (
    <ProtectedPage>
      <div>
        <Head>
          <title>Ventura HR</title>
          <meta name="description" content="VenturaHR description" />
          <link rel="icon" href="/favicon.ico" />
        </Head>

        <main className="py-6">
          <div className="container">Content</div>
        </main>
      </div>
    </ProtectedPage>
  )
}

export default Home
