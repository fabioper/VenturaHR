import { NextPage } from "next"
import ProtectedPage from "../../components/ProtectedPage"

const dashboard: NextPage = () => {
  return (
    <ProtectedPage onlyRoles={["company"]}>
      <div>Área da empresa</div>
    </ProtectedPage>
  )
}

export default dashboard
