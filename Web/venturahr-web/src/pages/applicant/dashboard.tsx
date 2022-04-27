import { NextPage } from "next"
import ProtectedPage from "../../components/ProtectedPage"

const dashboard: NextPage = () => {
  return (
    <ProtectedPage onlyRoles={["applicant"]}>
      <div>Área do candidado</div>
    </ProtectedPage>
  )
}

export default dashboard
