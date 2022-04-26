import type { NextPage } from "next"
import Head from "next/head"
import Link from "next/link"

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
        <Link href="/login">Sign in</Link>
      </main>
    </div>
  )
}

export default Home
