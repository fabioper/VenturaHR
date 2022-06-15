import { NextPage } from "next"
import ProtectedPage from "../../shared/components/ProtectedPage"
import { useAuth } from "../../shared/contexts/AuthContext"
import { Button } from "primereact/button"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import { Skeleton } from "primereact/skeleton"
import { useMemo, useState } from "react"
import { useRouter } from "next/router"
import Head from "next/head"
import { UserType } from "../../core/enums/UserType"
import CreateJobDialog from "../../shared/layout/sections/CreateJobDialog"

const Dashboard: NextPage = () => {
  const { user } = useAuth()
  const router = useRouter()
  const [showPublishJobModal, setShowPublishJobModal] = useState(false)

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
    <ProtectedPage role={UserType.Company} loader={contentSkeleton}>
      <Head>
        <title>{user?.name} | VenturaHR</title>
      </Head>

      <header className="py-8">
        <div className="container">
          <div className="flex justify-between items-end">
            <div>
              <BreadCrumb
                home={{
                  icon: PrimeIcons.HOME,
                  url: router.pathname,
                }}
                className="p-0 pb-5"
              />
              <h2 className="m-0 font-display text-3xl font-light">
                Dashboard
              </h2>
            </div>
            <div className="flex gap-3">
              <Button
                label="Publicar vaga"
                className="p-button-sm p-button-rounded p-button-shadowed"
                icon={PrimeIcons.PLUS}
                iconPos="right"
                onClick={() => setShowPublishJobModal(true)}
              />
            </div>
          </div>
        </div>
      </header>

      <CreateJobDialog
        visible={showPublishJobModal}
        onHide={() => setShowPublishJobModal(false)}
      />
    </ProtectedPage>
  )
}

export default Dashboard
