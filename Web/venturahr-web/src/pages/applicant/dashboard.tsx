import { NextPage } from "next"
import ProtectedPage from "../../shared/components/ProtectedPage"

const dashboard: NextPage = () => {
  return (
    <ProtectedPage onlyRoles={["applicant"]}>
      <div className="container">Área do candidato</div>
    </ProtectedPage>
  )
}

export default dashboard
