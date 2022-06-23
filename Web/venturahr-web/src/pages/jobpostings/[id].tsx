import { NextPage } from "next"
import { useRouter } from "next/router"
import Head from "next/head"
import { BreadCrumb } from "primereact/breadcrumb"
import { PrimeIcons } from "primereact/api"
import ProtectedPage from "../../shared/components/ProtectedPage/ProtectedPage"
import React, { useCallback, useEffect, useState } from "react"
import { marked } from "marked"
import { DateTime } from "luxon"
import { Card } from "primereact/card"
import { useAuth } from "../../shared/contexts/AuthContext"
import { UserType } from "../../core/enums/UserType"
import { Button } from "primereact/button"
import JobApplicationDialog from "../../shared/layout/sections/JobApplicationDialog/JobApplicationDialog"
import {
  fetchJobPosting,
  verifyIfUserCanApplyTo,
} from "../../core/services/JobPostingsService"
import { Skeleton } from "primereact/skeleton"
import JobPosting from "../../core/models/JobPosting"
import { Rating } from "primereact/rating"

export const JobPostingDetails: NextPage = () => {
  const router = useRouter()
  const jobPostingId = router.query.id as string
  const [jobPosting, setJobPosting] = useState<JobPosting>()
  const [showApplicationDialog, setShowApplicationDialog] = useState(false)
  const { user } = useAuth()
  const [checkingIfAlreadyApplied, setCheckingIfAlreadyApplied] = useState(true)
  const [alreadyApplied, setAlreadyApplied] = useState(false)

  const loadJobPostingOfId = async (jobPostingId: string) => {
    const data = await fetchJobPosting(jobPostingId)
    setJobPosting(data)
  }

  useEffect(() => {
    if (jobPostingId) {
      ;(async () => await loadJobPostingOfId(jobPostingId))()
    }
  }, [jobPostingId])

  useEffect(() => {
    ;(async () => {
      jobPosting &&
        user?.hasRole(UserType.Applicant) &&
        (await checkIfAlreadyApplied(jobPosting.id))
    })()
  }, [jobPosting])

  const checkIfAlreadyApplied = useCallback(async (jobPostingId: string) => {
    setCheckingIfAlreadyApplied(true)

    try {
      const userCanApply = await verifyIfUserCanApplyTo(jobPostingId)
      setAlreadyApplied(!userCanApply)
    } catch (e) {
      console.log(e)
    } finally {
      setCheckingIfAlreadyApplied(false)
    }
  }, [])

  if (!jobPosting) {
    return <>Carregando...</>
  }

  const applyButton = (): JSX.Element => {
    if (user?.userType !== UserType.Applicant) {
      return <></>
    }

    if (checkingIfAlreadyApplied) {
      return <Skeleton width="100px" />
    }

    if (alreadyApplied) {
      return (
        <span className="text-green-400 font-display text-right inline-flex gap-3 items-center my-5">
          <i className={PrimeIcons.CHECK_CIRCLE}></i>
          Você já se candidatou para esta vaga!
        </span>
      )
    }

    return (
      <Button
        label="Candidatar"
        className="p-button-rounded my-5"
        onClick={() => setShowApplicationDialog(true)}
      />
    )
  }

  return (
    <ProtectedPage loader={<>Carregando</>}>
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
                  {
                    label: "Vagas",
                    command: () => router.push("/jobpostings"),
                  },
                  { label: jobPosting?.title },
                ]}
                className="p-0 pb-5"
              />
              <div className="flex items-center justify-between flex-col md:flex-row">
                <h2 className="m-0 font-display text-4xl font-light">
                  {jobPosting.title}
                </h2>
                {applyButton()}
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

        <div className="grid lg:grid-cols-[1.5fr_1fr] gap-10">
          <div
            className="text-sm max-w-4xl text-[#a7b1c8]"
            dangerouslySetInnerHTML={{
              __html: marked.parse(jobPosting.description),
            }}
          />

          <div>
            <h3 className="font-display text-2xl font-light mt-0">Critérios</h3>

            <div className="grid grid-cols-2 gap-5 max-w-3xl">
              {jobPosting.criterias.map(criteria => (
                <Card
                  key={criteria.id}
                  className="p-0"
                  title={criteria.title}
                  subTitle={criteria.description}
                  footer={() => (
                    <Rating
                      value={criteria.desiredProfile}
                      readOnly
                      cancel={false}
                      stars={criteria.desiredProfile}
                    />
                  )}
                />
              ))}
            </div>
          </div>
        </div>
      </main>

      <JobApplicationDialog
        visible={showApplicationDialog}
        onHide={async () => {
          setShowApplicationDialog(false)
          await loadJobPostingOfId(jobPostingId)
        }}
        jobPosting={jobPosting}
      />
    </ProtectedPage>
  )
}

export default JobPostingDetails
