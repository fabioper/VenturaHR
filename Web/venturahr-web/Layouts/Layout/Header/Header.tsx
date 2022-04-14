import Link from "next/link"
import React from "react"
import styles from "./Header.module.scss"

const Header: React.FC = () => {
  return (
    <header className={styles.header}>
      <Link href="/">
        <div className="container">Header</div>
      </Link>
    </header>
  )
}

export default Header
