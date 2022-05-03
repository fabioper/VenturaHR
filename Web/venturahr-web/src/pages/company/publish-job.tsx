import { NextPage } from "next"
import { PrimeIcons } from "primereact/api"
import { BreadCrumb } from "primereact/breadcrumb"
import ProtectedPage from "../../components/ProtectedPage"
import { UserRole } from "../../core/enums/UserRole"
import { MenuItem } from "primereact/menuitem"

const PublishJob: NextPage = () => {
  const breadcrumbItems: MenuItem[] = [
    { label: "Vagas publicadas" },
    { label: "Publicar vaga" },
  ]
  return (
    <ProtectedPage onlyRoles={[UserRole.Company]}>
      <main>
        <header className="py-8">
          <div className="container">
            <div className="flex justify-between items-end">
              <div>
                <BreadCrumb
                  model={breadcrumbItems}
                  home={{
                    icon: PrimeIcons.HOME,
                    url: "/company/dashboard",
                  }}
                  className="p-0 pb-5"
                />
              </div>
            </div>
            <h2 className="m-0 font-display text-3xl font-light">
              Publicar vaga
            </h2>
          </div>
        </header>

        <div className="container">Formulário aqui</div>
      </main>
    </ProtectedPage>
  )
}

export default PublishJob
