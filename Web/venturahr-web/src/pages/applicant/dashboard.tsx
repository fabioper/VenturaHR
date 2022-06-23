import { NextPage } from "next"
import ProtectedPage from "../../shared/components/ProtectedPage/ProtectedPage"
import { UserType } from "../../core/enums/UserType"
import Head from "next/head"
import { useAuth } from "../../shared/contexts/AuthContext"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import { useRouter } from "next/router"
import { useCallback, useEffect, useState } from "react"
import FilterResponse from "../../core/dtos/filters/FilterResponse"
import { JobApplication } from "../../core/models/JobApplication"
import { fetchJobApplications } from "../../core/services/JobApplicationsService"
import { DataTable, DataTablePFSEvent } from "primereact/datatable"
import { Column } from "primereact/column"
import { DateTime } from "luxon"
import { Button } from "primereact/button"
import Link from "next/link"

const Dashboard: NextPage = () => {
  const { user } = useAuth()
  const router = useRouter()

  const [jobApplications, setJobApplications] = useState<
    FilterResponse<JobApplication>
  >({
    page: 1,
    total: 0,
    results: [],
  })
  const [first, setFirst] = useState(0)
  const [currentPage, setCurrentPage] = useState(1)

  const loadJobApplications = async (page = 1) => {
    setJobApplications(
      await fetchJobApplications({
        applicant: user?.id,
        page,
        pageSize: 10,
      })
    )
  }

  useEffect(() => {
    ;(async () => await loadJobApplications(currentPage))()
  }, [currentPage])

  const handlePageChange = useCallback((pageData: DataTablePFSEvent): void => {
    const page = pageData.page || 0
    setCurrentPage(page + 1)
    setFirst(pageData.first)
  }, [])

  return (
    <ProtectedPage role={UserType.Applicant}>
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
                  command: () => router.push("/").then(),
                }}
                className="p-0 pb-5"
              />
              <h2 className="text-3xl font-display font-normal">Dashboard</h2>
            </div>
          </div>
        </header>

        <div>
          <h2 className="text-3xl font-display font-normal m-0 mb-5">
            Vagas respondidas
          </h2>
        </div>

        <DataTable
          value={jobApplications.results}
          paginator
          rows={10}
          first={first}
          onPage={handlePageChange}
        >
          <Column
            header="Vaga"
            body={(app: JobApplication) => app.jobPosting.title}
          />
          <Column
            header="Local"
            body={(app: JobApplication) => app.jobPosting.location}
          />
          <Column
            header="Local"
            body={(app: JobApplication) =>
              DateTime.fromISO(app.appliedAt).toLocaleString()
            }
          />

          <Column
            body={(app: JobApplication) => (
              <div>
                <Link href={`/jobpostings/${app.jobPosting.id}`}>
                  <Button
                    icon={PrimeIcons.EYE}
                    className="p-button-text p-button-sm p-button-info p-button-rounded"
                    label="Ver Detalhes"
                  />
                </Link>
              </div>
            )}
            style={{ width: "15rem" }}
          />
        </DataTable>
      </div>
    </ProtectedPage>
  )
}

export default Dashboard
