import React, { useMemo } from "react"
import { Badge } from "primereact/badge"
import { JobPostingStatus } from "../../../core/enums/JobPostingStatus"

interface JobStatusBadge {
  status: JobPostingStatus
}

const JobStatusBadge: React.FC<JobStatusBadge> = ({ status }) => {
  const labels = {
    [JobPostingStatus.Expired]: { label: "Expirada", severity: "warn" },
    [JobPostingStatus.Closed]: { label: "Fechada", severity: "error" },
    [JobPostingStatus.Published]: { label: "Publicada", severity: "success" },
  }

  const jobStatus = useMemo(() => labels[status], [status])

  return <Badge value={jobStatus.label} severity={jobStatus.severity} />
}

export default JobStatusBadge
