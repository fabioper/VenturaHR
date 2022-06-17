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
              <h2 className="m-0 font-display text-3xl font-light">
                {jobPosting.title}
              </h2>
            </div>
          </div>
        </header>

        <div>
          <div
            dangerouslySetInnerHTML={{
              __html: marked.parse(jobPosting.description),
            }}
          ></div>
        </div>
      </main>
    </ProtectedPage>
  )
}

export default JobPostingResults
