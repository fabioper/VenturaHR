import React, { useCallback, useEffect, useState } from "react"
import { UserType } from "../../../../core/enums/UserType"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import ProtectedPage from "../../../../shared/components/ProtectedPage"
import { useRouter } from "next/router"
import { useJobPostingOfId } from "../../../../shared/hooks/useJobPostingOfId"
import { NextPage } from "next"
import { JobApplication } from "../../../../core/models/JobApplication"
import { fetchApplicationsFromJobPosting } from "../../../../core/services/JobApplicationsService"
import { DataTable } from "primereact/datatable"
import { Column } from "primereact/column"
import CriteriaAnswer from "../../../../core/models/CriteriaAnswer"

const JobPostingResults: NextPage = () => {
  const router = useRouter()
  const jobPostingId = router.query.id as string
  const jobPosting = useJobPostingOfId(jobPostingId)

  const [applications, setApplications] = useState<JobApplication[]>([])
  const [expandedApplications, setExpandedApplications] = useState<
    JobApplication[]
  >([])

  const loadApplications = async (page = 1) => {
    if (jobPostingId) {
      setApplications(await fetchApplicationsFromJobPosting(jobPostingId))
    }
  }

  useEffect(() => {
    ;(async () => await loadApplications())()
  }, [])

  const criteriaAnswersTemplate = useCallback((application: JobApplication) => {
    return (
      <div className="max-w-sm px-10">
        <DataTable value={application.answers}>
          <Column
            header="Critério"
            body={(x: CriteriaAnswer) => x.criteriaTitle}
          />
          <Column header="Resposta" body={(x: CriteriaAnswer) => x.value} />
        </DataTable>
      </div>
    )
  }, [])

  if (!jobPosting) {
    return <>Carregando...</>
  }

  return (
    <ProtectedPage role={UserType.Company} loader={<>Carregando</>}>
      <Head>
        <title>{jobPosting.title} | VenturaHR</title>
      </Head>

      <main className="container">
        <header className="pt-8 pb-4">
          <div className="flex justify-between items-end">
            <div className="w-full">
              <BreadCrumb
                home={{
                  icon: PrimeIcons.HOME,
                  url: router.pathname,
                }}
                model={[
                  { url: "/company/dashboard", label: "Vagas publicadas" },
                  {
                    url: "/company/job-postings/" + jobPostingId,
                    label: jobPosting?.title,
                  },
                  { label: "Resultados" },
                ]}
                className="p-0 pb-5"
              />
              <div className="flex flex-row items-center justify-between gap-5">
                <div className="flex flex-col items-start justify-between gap-2">
                  <h2 className="m-0 font-display text-4xl font-light">
                    {jobPosting.title}
                  </h2>
                </div>
              </div>

              <div>{/* TODO */}</div>
            </div>
          </div>
        </header>

        <div>
          <DataTable
            value={applications}
            expandedRows={expandedApplications}
            onRowToggle={e => setExpandedApplications(e.data)}
            rowExpansionTemplate={criteriaAnswersTemplate}
          >
            <Column expander style={{ width: "3em" }} />
            <Column
              header="Candidato"
              body={(application: JobApplication) => application.applicant.name}
            />

            <Column
              header="E-mail"
              body={(application: JobApplication) =>
                application.applicant.email
              }
            />

            <Column
              header="Telefone"
              body={(application: JobApplication) =>
                application.applicant.phoneNumber
              }
            />

            <Column
              header="Média"
              body={(application: JobApplication) => application.average}
            />
          </DataTable>
        </div>
      </main>
    </ProtectedPage>
  )
}

export default JobPostingResults
