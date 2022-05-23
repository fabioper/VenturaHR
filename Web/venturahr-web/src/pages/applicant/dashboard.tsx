import { NextPage } from "next"
import ProtectedPage from "../../shared/components/ProtectedPage"
import { UserType } from "../../core/enums/UserType"

const dashboard: NextPage = () => {
  return (
    <ProtectedPage role={[UserType.Applicant]}>
      <div className="container">Área do candidato</div>
    </ProtectedPage>
  )
}

export default dashboard
