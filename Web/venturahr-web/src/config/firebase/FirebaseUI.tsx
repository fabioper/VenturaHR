import React, { useEffect, useRef } from "react"
import firebase from "firebase/compat/app"
import "firebaseui/dist/firebaseui.css"
import { firebaseApp } from "./firebase.config"

const FirebaseUI: React.FC = () => {
  const ref = useRef(null)

  async function loadFirebaseUi() {
    const firebaseui = await import("firebaseui")
    const ui =
      firebaseui.auth.AuthUI.getInstance() ||
      new firebaseui.auth.AuthUI(firebaseApp.auth())

    if (ref.current) {
      ui.start(ref.current, {
        signInOptions: [
          { provider: firebase.auth.EmailAuthProvider.PROVIDER_ID },
          firebase.auth.GoogleAuthProvider.PROVIDER_ID,
        ],
        signInFlow: "popup",
        signInSuccessUrl: "/",
      })
    }
  }

  useEffect(() => {
    ;(async () => loadFirebaseUi())()
  }, [])

  return (
    <div>
      <div ref={ref} />
    </div>
  )
}

export default FirebaseUI
