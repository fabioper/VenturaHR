import React, { useCallback, useEffect, useState } from "react"
import JobPosting from "../../../../core/models/JobPosting"
import { fetchJobPostings } from "../../../../core/services/JobPostingsService"
import { Card } from "primereact/card"
import { Chip } from "primereact/chip"
import { PrimeIcons } from "primereact/api"
import { DateTime } from "luxon"
import Link from "next/link"

const LastPublishedJobs: React.FC = () => {
  const [jobPostings, setJobPostings] = useState<JobPosting[]>([])
  const [loading, setLoading] = useState(true)

  const loadLastJobPostings = useCallback(async () => {
    setLoading(true)
    try {
      const response = await fetchJobPostings({
        page: 1,
        pageSize: 10,
      })
      setJobPostings(response.results)
    } catch (e) {
      console.log(e)
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    ;(async () => await loadLastJobPostings())()
  }, [])

  return (
    <div>
      <h2 className="font-display font-normal text-4xl text-center mt-20 mb-10">
        Últimas vagas publicadas
      </h2>

      <div className="card-grid">
        {jobPostings.map(job => (
          <Link href={"/jobpostings/" + job.id} key={job.id}>
            <Card
              title={job.title}
              subTitle={job.company.name}
              className="cursor-pointer"
              footer={() => {
                return (
                  <div className="flex gap-2 items-center justify-between mt-auto">
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
    </div>
  )
}

export default LastPublishedJobs
