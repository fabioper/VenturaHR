import type { NextPage } from "next"
import Head from "next/head"

const Home: NextPage = () => {
  return (
    <div>
      <Head>
        <title>Ventura HR</title>
        <meta name="description" content="VenturaHR description" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main>
        <div className="container">Content</div>
      </main>
    </div>
  )
}

export default Home
