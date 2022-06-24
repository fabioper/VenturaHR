import type { NextPage } from "next"
import Head from "next/head"
import { useAuth } from "../shared/contexts/AuthContext"
import { useRouter } from "next/router"
import dynamic from "next/dynamic"

const LastPublishedJobs = dynamic(
  () => import("../shared/layout/sections/LastPublishedJobs/LastPublishedJobs"),
  { ssr: false }
)

const Home: NextPage = () => {
  const { user, isLogged } = useAuth()
  const router = useRouter()

  if (isLogged && user) {
    router.push(user.redirectPage).then()
  }

  return (
    <div>
      <Head>
        <title>Ventura HR</title>
        <meta name="description" content="VenturaHR description" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <div className="container">
        <LastPublishedJobs />
      </div>
    </div>
  )
}

export default Home
