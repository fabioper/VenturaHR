import type { NextPage } from "next"
import Head from "next/head"
import { useAuth } from "../shared/contexts/AuthContext"
import { useRouter } from "next/router"
import LastPublishedJobs from "../shared/layout/sections/LastPublishedJobs/LastPublishedJobs"

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

      {/* <div className="py-6 h-full"> */}
      {/*   <div className="container flex flex-col items-center justify-center h-3/5"> */}
      {/*    <h2 className="text-5xl md:text-6xl font-display font-extrabold m-0 text-center"> */}
      {/*      Lorem ipsum dolor sit amet. */}
      {/*    </h2> */}
      {/*    <p className="max-w-2xl text-lg md:text-xl text-center font-light font-body leading-normal text-slate-400"> */}
      {/*      Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aspernatur */}
      {/*      assumenda consequuntur corporis dolor dolore. */}
      {/*    </p> */}
      {/*    <div className="flex items-center justify-center gap-2 mt-3"> */}
      {/*      <Link href="/signup"> */}
      {/*        <Button */}
      {/*          label="Cadastre-se" */}
      {/*          className="p-button-rounded p-button-shadowed" */}
      {/*        /> */}
      {/*      </Link> */}
      {/*      <Link href="/login"> */}
      {/*        <Button */}
      {/*          label="Já tenho conta" */}
      {/*          className="p-button-info p-button-outlined p-button-rounded" */}
      {/*        /> */}
      {/*      </Link> */}
      {/*    </div> */}
      {/*  </div> */}
      {/* </div> */}

      <div className="container">
        <LastPublishedJobs />
      </div>
    </div>
  )
}

export default Home
