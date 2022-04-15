import Link from "next/link"
import styles from "./Header.module.scss"
import React from "react"

const Header: React.FC = () => {
  return (
    <header className={styles.header}>
      <Link href="/">
        <div className="container">VenturaHR</div>
      </Link>
    </header>
  )
}

export default Header
