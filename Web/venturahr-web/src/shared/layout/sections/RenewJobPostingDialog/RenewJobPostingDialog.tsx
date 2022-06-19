import React, { useMemo, useState } from "react"
import { Dialog, DialogProps } from "primereact/dialog"
import { Button } from "primereact/button"
import { Calendar } from "primereact/calendar"
import { PrimeIcons } from "primereact/api"
import JobPosting from "../../../../core/models/JobPosting"
import { DateTime } from "luxon"
import { useToaster } from "../../../hooks/useToaster"
import { renewJobPosting } from "../../../../core/services/JobPostingsService"

interface RenewJobPostingDialogProps {
  visible: boolean
  onHide: () => any
  jobPosting: JobPosting
}

const RenewJobPostingDialog: React.FC<RenewJobPostingDialogProps> = ({
  onHide,
  visible,
  jobPosting,
}) => {
  const { toast } = useToaster()
  const [isLoading, setIsLoading] = useState(false)

  const initialState = DateTime.fromISO(jobPosting.expireAt)
    .plus({ day: 1 })
    .toJSDate()

  const [newExpirationDate, setNewExpirationDate] = useState<Date>(initialState)

  const handleHide = (): void => onHide()

  const handleSubmit = async () => {
    setIsLoading(true)
    try {
      await renewJobPosting(jobPosting.id, newExpirationDate)
      toast.success("Vaga renovada com sucesso!")
      handleHide()
    } catch (e) {
      toast.error("Ocorreu um erro ao renovar esta vaga")
    } finally {
      setIsLoading(false)
    }
  }

  const dialogProps: DialogProps = useMemo(
    () => ({
      onHide: handleHide,
      visible: visible,
      header: "Renovar vaga",
      dismissableMask: true,
      blockScroll: true,
      focusOnShow: false,
      draggable: false,
      footer: () => (
        <div className="flex justify-end pt-10">
          <Button
            className="p-button-rounded p-button-outlined"
            label="Cancelar"
            type="button"
            icon={PrimeIcons.TIMES}
            onClick={handleHide}
          />
          <Button
            className="p-button-rounded"
            label="Renovar"
            icon={PrimeIcons.CHECK_SQUARE}
            type="submit"
            loading={isLoading}
            onClick={handleSubmit}
          />
        </div>
      ),
    }),
    [visible, jobPosting, newExpirationDate, isLoading]
  )

  return (
    <Dialog {...dialogProps}>
      <div>
        <Calendar
          inline
          className="w-full min-h-2xl"
          minDate={initialState}
          value={newExpirationDate}
          onChange={e => setNewExpirationDate(e.target.value as Date)}
        />
      </div>
    </Dialog>
  )
}

export default RenewJobPostingDialog
