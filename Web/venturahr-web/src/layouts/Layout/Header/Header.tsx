import Link from "next/link"
import React from "react"

const Header: React.FC = () => {
  return (
    <header>
      <Link href="/">
        <div className="container">VenturaHR</div>
      </Link>
    </header>
  )
}

export default Header
