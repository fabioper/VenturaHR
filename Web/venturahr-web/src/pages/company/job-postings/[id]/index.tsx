import { NextPage } from "next"
import { useRouter } from "next/router"
import { UserType } from "../../../../core/enums/UserType"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import ProtectedPage from "../../../../shared/components/ProtectedPage"
import React from "react"
import { marked } from "marked"
import { DateTime } from "luxon"
import { Button } from "primereact/button"
import Link from "next/link"
import { useJobPostingOfId } from "../../../../shared/hooks/useJobPostingOfId"
import JobStatusBadge from "../../../../shared/components/JobStatusBadge/JobStatusBadge"

export const JobPostingDetails: NextPage = () => {
  const router = useRouter()
  const jobPostingId = router.query.id as string
  const jobPosting = useJobPostingOfId(jobPostingId)

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
                  { label: jobPosting?.title },
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
                    <Link
                      href={`/company/job-postings/${jobPostingId}/results`}
                    >
                      <Button
                        icon={PrimeIcons.CHART_BAR}
                        label="Ver resultados"
                        className="p-button-sm"
                      />
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </header>

        <div className="flex gap-24 mb-10">
          <div className="text-sm flex gap-2 items-center justify-center">
            <i className={`${PrimeIcons.CALENDAR_PLUS} opacity-80`} />
            <p className="m-0 leading-tight text-lg font-display font-light">
              <strong className="block text-xs font-body">Publicada em:</strong>{" "}
              {DateTime.fromISO(jobPosting.createdAt).toLocaleString()}
            </p>
          </div>

          <div className="text-sm flex gap-2 items-center justify-center">
            <i className={`${PrimeIcons.CALENDAR_MINUS} opacity-80`} />
            <p className="m-0 leading-tight text-lg font-display font-light">
              <strong className="block text-xs font-body">Expira em:</strong>{" "}
              {DateTime.fromISO(jobPosting.expireAt).toLocaleString()}
            </p>
          </div>
        </div>

        <div>
          <div
            className="text-sm max-w-4xl text-[#a7b1c8]"
            dangerouslySetInnerHTML={{
              __html: marked.parse(jobPosting.description),
            }}
          />

          <div>
            <h3 className="font-display text-2xl font-light mt-16">
              Critérios
            </h3>

            <div className="grid grid-cols-2 gap-5 max-w-3xl">
              {jobPosting.criterias.map(criteria => (
                <div
                  key={criteria.id}
                  className="border border-slate-800 border-solid rounded-lg py-2 px-5 bg-[#00000024]"
                >
                  <h4 className="m-0 mb-2">{criteria.title}</h4>
                  <p className="m-0 text-sm">{criteria.description}</p>
                  <div>
                    <p className="my-2 text-xs">
                      Perfil: {criteria.desiredProfile}
                    </p>
                    <p className="my-2 text-xs">Peso: {criteria.weight}</p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </main>
    </ProtectedPage>
  )
}

export default JobPostingDetails
