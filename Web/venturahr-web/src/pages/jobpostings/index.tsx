import React, { useEffect, useState } from "react"
import { NextPage } from "next"
import ProtectedPage from "../../shared/components/ProtectedPage/ProtectedPage"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import { useRouter } from "next/router"
import JobPosting from "../../core/models/JobPosting"
import FilterResponse from "../../core/dtos/filters/FilterResponse"
import { fetchJobPostings } from "../../core/services/JobPostingsService"
import { useLoader } from "../../shared/hooks/useLoader"
import { Chip } from "primereact/chip"
import { Card } from "primereact/card"
import { DateTime } from "luxon"
import { Paginator } from "primereact/paginator"
import { InputText } from "primereact/inputtext"
import Link from "next/link"
import { ProgressSpinner } from "primereact/progressspinner"

const index: NextPage = () => {
  const router = useRouter()
  const [jobPostings, setJobPostings] = useState<FilterResponse<JobPosting>>({
    page: 0,
    total: 0,
    results: [],
  })
  const [first, setFirst] = useState(0)
  const [currentPage, setCurrentPage] = useState(1)
  const { loading, withLoader } = useLoader()
  const [searchQuery, setSearchQuery] = useState("")

  const totalRows = 9

  const loadJobPostings = async (query?: string) => {
    await withLoader(async () => {
      setJobPostings(
        await fetchJobPostings({
          page: currentPage,
          pageSize: totalRows,
          query,
        })
      )
    })
  }

  useEffect(() => {
    ;(async () => {
      setCurrentPage(1)
      await loadJobPostings(searchQuery)
    })()
  }, [searchQuery])

  useEffect(() => {
    ;(async () => await loadJobPostings())()
  }, [currentPage])

  return (
    <ProtectedPage>
      <Head>
        <title>Vagas | VenturaHR</title>
      </Head>

      <main className="container">
        <header className="py-8">
          <div>
            <div>
              <BreadCrumb
                home={{
                  icon: PrimeIcons.HOME,
                  command(): void {
                    router.push("/").then()
                  },
                }}
                className="p-0 pb-5"
              />
              <div className="flex justify-between items-center w-full">
                <h2 className="m-0 font-display text-3xl font-light">Vagas</h2>
                <span className="p-input-icon-left">
                  <i className="pi pi-search" />
                  <InputText
                    value={searchQuery}
                    placeholder="Search"
                    onChange={e => setSearchQuery(e.target.value)}
                  />
                </span>
              </div>
            </div>
          </div>
        </header>

        {loading && (
          <p className="text-center p-10 text-slate-500">
            <ProgressSpinner />
          </p>
        )}

        {!jobPostings.results.length && !loading && (
          <p className="text-center p-10 text-slate-500">
            Nenhuma vaga publicada
          </p>
        )}

        {!!jobPostings.results.length && !loading && (
          <div className="card-grid">
            {jobPostings.results.map(job => (
              <Link href={"/jobpostings/" + job.id} key={job.id}>
                <Card
                  title={job.title}
                  subTitle={job.company.name}
                  className="cursor-pointer"
                  footer={() => {
                    return (
                      <div className="flex gap-2 items-end justify-between mt-auto">
                        <div className="flex gap-2 flex-wrap">
                          {job.criterias.map(criteria => (
                            <Chip
                              key={criteria.id}
                              label={criteria.title}
                              className="text-xs"
                            ></Chip>
                          ))}
                        </div>
                        <span className="text-sm inline-flex gap-2 items-center shrink-0">
                          <i
                            className={`${PrimeIcons.CALENDAR} text-xs text-slate-500`}
                          ></i>
                          {DateTime.fromISO(job.createdAt).toRelativeCalendar()}
                        </span>
                      </div>
                    )
                  }}
                />
              </Link>
            ))}
          </div>
        )}

        <Paginator
          first={first}
          rows={totalRows}
          totalRecords={jobPostings.total}
          onPageChange={e => {
            setCurrentPage(e.page + 1)
            setFirst(e.first)
          }}
        />
      </main>
    </ProtectedPage>
  )
}

export default index
