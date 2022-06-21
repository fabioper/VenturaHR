import React, { useCallback, useEffect, useState } from "react"
import { UserType } from "../../../../core/enums/UserType"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import ProtectedPage from "../../../../shared/components/ProtectedPage/ProtectedPage"
import { useRouter } from "next/router"
import { NextPage } from "next"
import { JobApplication } from "../../../../core/models/JobApplication"
import { fetchApplicationsFromJobPosting } from "../../../../core/services/JobApplicationsService"
import { DataTable } from "primereact/datatable"
import { Column } from "primereact/column"
import CriteriaAnswer from "../../../../core/models/CriteriaAnswer"
import JobStatusBadge from "../../../../shared/components/JobStatusBadge/JobStatusBadge"
import { JobPostingStatus } from "../../../../core/enums/JobPostingStatus"
import { Button } from "primereact/button"
import { DateTime } from "luxon"
import RenewJobPostingDialog from "../../../../shared/layout/sections/RenewJobPostingDialog/RenewJobPostingDialog"
import JobPosting from "../../../../core/models/JobPosting"
import { fetchJobPosting } from "../../../../core/services/JobPostingsService"

const JobPostingResults: NextPage = () => {
  const router = useRouter()
  const jobPostingId = router.query.id as string
  const [jobPosting, setJobPosting] = useState<JobPosting>()
  const [renewDialogIsVisible, setRenewDialogIsVisible] = useState(false)
  const [applications, setApplications] = useState<JobApplication[]>([])
  const [expandedApplications, setExpandedApplications] = useState<
    JobApplication[]
  >([])

  const loadJobPostingOfId = async (jobPostingId: string) => {
    const data = await fetchJobPosting(jobPostingId)
    setJobPosting(data)
  }

  const loadApplications = async () => {
    if (jobPostingId) {
      setApplications(await fetchApplicationsFromJobPosting(jobPostingId))
    }
  }

  useEffect(() => {
    if (jobPostingId) {
      ;(async () => await loadJobPostingOfId(jobPostingId))()
    }
  }, [jobPostingId])

  useEffect(() => {
    ;(async () => await loadApplications())()
  }, [])

  const criteriaAnswersTemplate = useCallback((application: JobApplication) => {
    return (
      <div className="max-w-3xl">
        <DataTable value={application.answers}>
          <Column
            header="Critério"
            body={(x: CriteriaAnswer) => x.criteriaTitle}
          />
          <Column
            header="Descrição"
            body={(x: CriteriaAnswer) => x.criteriaDescription}
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
                  <JobStatusBadge status={jobPosting.status} />
                </div>

                <div className="flex gap-5 items-center">
                  <div className="flex gap-2 my-10">
                    {jobPosting.status !== JobPostingStatus.Closed && (
                      <Button
                        icon={PrimeIcons.CALENDAR_PLUS}
                        label="Renovar"
                        className="p-button-sm p-button-info"
                        onClick={() => setRenewDialogIsVisible(true)}
                      />
                    )}
                  </div>
                </div>
              </div>

              <div className="flex gap-24 mb-10">
                <div className="text-sm flex gap-2 items-center justify-center">
                  <i className={`${PrimeIcons.CALENDAR_PLUS} opacity-80`} />
                  <p className="m-0 leading-tight text-lg font-display font-light">
                    <strong className="block text-xs font-body">
                      Publicada em:
                    </strong>{" "}
                    {DateTime.fromISO(jobPosting.createdAt).toLocaleString()}
                  </p>
                </div>

                <div className="text-sm flex gap-2 items-center justify-center">
                  <i className={`${PrimeIcons.CALENDAR_MINUS} opacity-80`} />
                  <p className="m-0 leading-tight text-lg font-display font-light">
                    <strong className="block text-xs font-body">
                      Expira em:
                    </strong>{" "}
                    {DateTime.fromISO(jobPosting.expireAt).toLocaleString()}
                  </p>
                </div>
              </div>
            </div>
          </div>
        </header>

        <div className="max-w-5xl mx-auto my-10">
          <h3 className="text-3xl font-display font-normal text-center">
            Respostas recebidas
          </h3>

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

            <Column
              header="Data"
              body={(application: JobApplication) =>
                DateTime.fromISO(application.appliedAt).toLocaleString()
              }
            />
          </DataTable>
        </div>
      </main>

      <RenewJobPostingDialog
        visible={renewDialogIsVisible}
        onHide={async () => {
          setRenewDialogIsVisible(false)
          await loadJobPostingOfId(jobPostingId)
        }}
        jobPosting={jobPosting}
      />
    </ProtectedPage>
  )
}

export default JobPostingResults
