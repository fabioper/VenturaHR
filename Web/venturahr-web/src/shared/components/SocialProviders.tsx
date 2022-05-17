import React from "react"
import { Button } from "primereact/button"
import { UserRole } from "../../core/enums/UserRole"
import { FirebaseError } from "@firebase/util"
import { useAuth } from "../contexts/AuthContext"
import { ProviderOptions } from "../../core/services/FirebaseAuthService"

interface SocialProvidersProps {
  onUserCancelError: () => any
  onError: () => any
}

const SocialProviders: React.FC<SocialProvidersProps> = ({
  onError,
  onUserCancelError,
}) => {
  const { loginUsingProvider } = useAuth()

  function handleFirebaseErrors(e: FirebaseError): void {
    if (e.code === "auth/user-cancelled") {
      onUserCancelError()
    } else {
      console.log(e)
    }
  }

  const handleProviderLogin = async (providerId: ProviderOptions) => {
    try {
      await loginUsingProvider(providerId, UserRole.Applicant)
    } catch (e) {
      if (e instanceof FirebaseError) {
        handleFirebaseErrors(e)
      }
      onError()
    }
  }

  return (
    <div className="flex flex-col gap-3 my-7">
      <div className="flex justify-center items-center flex-row gap-2">
        <Button
          type="button"
          className="google p-0"
          aria-label="Google"
          onClick={async () => await handleProviderLogin("google")}
        >
          <i className="pi pi-google px-2 py-2"></i>
          <span className="px-0 text-sm">Google</span>
        </Button>

        <Button
          type="button"
          className="github p-0"
          aria-label="github"
          onClick={async () => await handleProviderLogin("github")}
        >
          <i className="pi pi-github px-2 py-2"></i>
          <span className="px-0 text-sm">Github</span>
        </Button>

        <Button
          type="button"
          className="twitter p-0"
          aria-label="twitter"
          onClick={async () => await handleProviderLogin("twitter")}
        >
          <i className="pi pi-twitter px-2 py-2"></i>
          <span className="px-0 text-sm">Twitter</span>
        </Button>
      </div>
    </div>
  )
}

export default SocialProviders
