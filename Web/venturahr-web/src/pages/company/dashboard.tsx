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
import CreateJobDialog from "../../shared/layout/sections/CreateJobDialog/CreateJobDialog"
import JobPostingsList from "../../shared/components/JobPostingsList/JobPostingsList"
import FilterResponse from "../../core/dtos/filters/FilterResponse"
import JobPosting from "../../core/models/JobPosting"
import { useLoader } from "../../shared/hooks/useLoader"
import { fetchJobPostings } from "../../core/services/JobPostingsService"

const Dashboard: NextPage = () => {
  const { user } = useAuth()
  const { loading, usingLoader } = useLoader()
  const router = useRouter()
  const [showPublishJobModal, setShowPublishJobModal] = useState(false)
  const [data, setData] = useState<FilterResponse<JobPosting>>({
    page: 0,
    total: 0,
    results: [],
  })

  const loadJobPostings = async (page = 1) => {
    await usingLoader(async () => {
      setData(
        await fetchJobPostings({
          pageSize: 10,
          page,
          company: user?.id,
        })
      )
    })
  }

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

      <div className="container">
        <header className="py-8">
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
                className="p-button-sm p-button-shadowed"
                icon={PrimeIcons.PLUS}
                iconPos="right"
                onClick={() => setShowPublishJobModal(true)}
              />
            </div>
          </div>
        </header>

        <div>
          <h2 className="text-3xl font-display font-normal">
            Vagas Publicadas
          </h2>

          <JobPostingsList
            loading={loading}
            data={data}
            onPageChange={async page => await loadJobPostings(page)}
          />
        </div>
      </div>

      <CreateJobDialog
        visible={showPublishJobModal}
        onHide={async () => {
          await loadJobPostings()
          setShowPublishJobModal(false)
        }}
      />
    </ProtectedPage>
  )
}

export default Dashboard
