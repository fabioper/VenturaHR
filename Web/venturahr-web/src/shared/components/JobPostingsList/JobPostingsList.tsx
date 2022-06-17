import React, { useCallback, useEffect, useState } from "react"
import { DataTable, DataTablePFSEvent } from "primereact/datatable"
import JobPosting from "../../../core/models/JobPosting"
import { Column } from "primereact/column"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import FilterResponse from "../../../core/dtos/filters/FilterResponse"
import { DateTime } from "luxon"
import Link from "next/link"

interface JobPostingsListProps {
  onPageChange: (page: number) => void
  data: FilterResponse<JobPosting>
  loading: boolean
}

const JobPostingsList: React.FC<JobPostingsListProps> = ({
  onPageChange,
  data,
  loading,
}) => {
  const [first, setFirst] = useState(0)
  const [currentPage, setCurrentPage] = useState(1)

  useEffect(() => {
    onPageChange(currentPage)
  }, [currentPage])

  const handlePageChange = (pageData: DataTablePFSEvent): void => {
    const page = pageData.page || 0
    setCurrentPage(page + 1)
    setFirst(pageData.first)
  }

  const salaryTemplate = useCallback((job: JobPosting): string => {
    const currencyFormatter = new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
    })

    return currencyFormatter.format(job.salary)
  }, [])

  const actionsTemplate = useCallback((job: JobPosting): JSX.Element => {
    return (
      <div>
        <Link href={`/company/job-postings/${job.id}`}>
          <Button
            icon={PrimeIcons.EYE}
            className="p-button-sm p-button-text p-button-info"
          />
        </Link>
      </div>
    )
  }, [])

  const expirationTemplate = (job: JobPosting): JSX.Element => {
    const expiration = DateTime.fromISO(job.expireAt)
    return (
      <div className="flex items-center gap-2 align-middle">
        <i className={PrimeIcons.CALENDAR}></i>
        <span>
          {expiration.toRelativeCalendar({
            locale: "pt-BR",
          }) || ""}
        </span>
      </div>
    )
  }

  return (
    <div>
      <DataTable
        value={data.results}
        rowHover
        paginator
        loading={loading}
        lazy
        rows={10}
        totalRecords={data.total}
        onPage={handlePageChange}
        first={first}
      >
        <Column header="Título" body={(job: JobPosting) => job.title} />
        <Column header="Local" body={(job: JobPosting) => job.location} />
        <Column header="Salário" body={salaryTemplate} />
        <Column
          header="Data de Expiração"
          body={(job: JobPosting) => expirationTemplate(job)}
        />
        <Column body={actionsTemplate} style={{ width: "2rem" }} />
      </DataTable>
    </div>
  )
}

export default JobPostingsList
