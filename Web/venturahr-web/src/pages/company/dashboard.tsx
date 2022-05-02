import { NextPage } from "next"
import ProtectedPage from "../../components/ProtectedPage"
import { useAuth } from "../../contexts/AuthContext"
import { Button } from "primereact/button"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import { Skeleton } from "primereact/skeleton"
import { useMemo } from "react"

const dashboard: NextPage = () => {
  const { user } = useAuth()

  const contentSkeleton = useMemo(
    () => (
      <div>
        <div className="container py-8">
          <div className="flex justify-between items-end">
            <div>
              <Skeleton width="50px" className="mb-5" />
              <Skeleton width="150px" />
            </div>
            <Skeleton width="100px" />
          </div>
        </div>
      </div>
    ),
    []
  )

  return (
    <ProtectedPage onlyRoles={["company"]} loader={contentSkeleton}>
      <header className=" bg-[#06111f] bg-opacity-70 border-0 border-b border-solid border-slate-800 backdrop-blur-2xl py-8">
        <div className="container">
          <div className="flex justify-between items-end">
            <div>
              <BreadCrumb
                home={{
                  icon: PrimeIcons.HOME,
                }}
                className="p-0 pb-5"
              />
              <h2 className="m-0 font-display text-3xl font-light">
                {user?.name}
              </h2>
            </div>
            <div className="flex gap-3">
              <Button
                label="Publicar vaga"
                className="p-button-sm p-button-rounded"
                icon={PrimeIcons.PLUS}
                iconPos="right"
              />
            </div>
          </div>
        </div>
      </header>
    </ProtectedPage>
  )
}

export default dashboard
