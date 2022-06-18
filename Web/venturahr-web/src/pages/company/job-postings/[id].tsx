import { NextPage } from "next"
import { useRouter } from "next/router"
import { UserType } from "../../../core/enums/UserType"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import ProtectedPage from "../../../shared/components/ProtectedPage"
import { useEffect, useState } from "react"
import JobPosting from "../../../core/models/JobPosting"
import { fetchJobPosting } from "../../../core/services/JobPostingsService"
import { marked } from "marked"
import { DateTime } from "luxon"
import { Button } from "primereact/button"
import { JobPostingStatus } from "../../../core/enums/JobPostingStatus"

export const JobPostingResults: NextPage = () => {
  const router = useRouter()
  const jobPostingId = router.query.id as string
  const [jobPosting, setJobPosting] = useState<JobPosting>()

  async function loadJobPostingOfId(jobPostingId: string) {
    const data = await fetchJobPosting(jobPostingId)
    setJobPosting(data)
  }

  useEffect(() => {
    if (jobPostingId) {
      ;(async () => await loadJobPostingOfId(jobPostingId))()
    }
  }, [jobPostingId])

  if (!jobPosting) {
    return <>Carregando...</>
  }

  return (
    <ProtectedPage role={UserType.Company} loader={<>Carregando</>}>
      <Head>
        <title>{jobPosting.title} | VenturaHR</title>
      </Head>

      <main className="container">
        <header className="py-8">
          <div className="flex justify-between items-end">
            <div>
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
              <h2 className="m-0 font-display text-4xl font-light">
                {jobPosting.title}
              </h2>
            </div>
          </div>
        </header>

        <div className="grid md:grid-cols-[2fr_auto] gap-5 items-start">
          <div className="grow-3">
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
          <div className="p-5 bg-blue-800 grow-1 min-w-[300px] rounded-2xl shadow flex flex-col">
            <div>
              <p>
                <strong className="block">Publicada em:</strong>{" "}
                {DateTime.fromISO(jobPosting.createdAt).toLocaleString()}
              </p>

              <p>
                <strong className="block">Expira em:</strong>{" "}
                {DateTime.fromISO(jobPosting.expireAt).toLocaleString()}
              </p>
            </div>

            {jobPosting.status === JobPostingStatus.Expired && (
              <Button
                icon={PrimeIcons.CALENDAR_PLUS}
                label="Renovar"
                className="p-button-sm w-full mt-auto"
                style={{
                  background: "azure",
                  color: "#0f766e",
                  borderColor: "#0f766e",
                }}
              />
            )}
            <Button
              icon={PrimeIcons.LOCK}
              label="Fechar"
              className="p-button-sm w-full mt-2"
              style={{
                color: "#ffffff",
                background: "#050d19",
                border: "1px solid #050d19",
              }}
            />
          </div>
        </div>
      </main>
    </ProtectedPage>
  )
}

export default JobPostingResults
