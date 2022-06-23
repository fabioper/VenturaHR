import React, { useMemo, useState } from "react"
import { Dialog, DialogProps } from "primereact/dialog"
import { Button } from "primereact/button"
import JobPosting from "../../../../core/models/JobPosting"
import { Slider, SliderChangeParams } from "primereact/slider"
import { createJobApplication } from "../../../../core/services/JobApplicationsService"
import { useToaster } from "../../../hooks/useToaster"

interface JobApplicationDialogProps {
  visible: boolean
  onHide: () => any
  jobPosting: JobPosting
}

const JobApplicationDialog: React.FC<JobApplicationDialogProps> = ({
  onHide,
  visible,
  jobPosting,
}) => {
  const [answers, setAnswers] = useState<{ [criteriaId: string]: number }>({})
  const [isLoading, setIsLoading] = useState(false)
  const { toast } = useToaster()

  const handleHide = () => {
    onHide()
    setAnswers({})
  }

  const shouldDisableSubmitButton = (): boolean =>
    Object.keys(answers).length !== jobPosting.criterias.length

  const handleSubmit = async (answers: { [p: string]: number }) => {
    setIsLoading(true)
    try {
      await createJobApplication({
        jobPostingId: jobPosting.id,
        criteriaAnswers: Object.keys(answers).map(criteriaId => ({
          criteriaId,
          value: answers[criteriaId],
        })),
      })
      toast.success("Candidatura realizada com sucesso!")
      handleHide()
    } catch (e) {
      toast.error("Ocorreu um erro ao confirmar sua candidatura.")
    } finally {
      setIsLoading(false)
    }
  }

  const dialogProps: DialogProps = useMemo(
    () => ({
      onHide: handleHide,
      visible: visible,
      header: "Confirmar Candidatura",
      dismissableMask: false,
      blockScroll: true,
      focusOnShow: false,
      draggable: false,
      footer: () => (
        <div className="flex justify-end pt-10">
          <Button
            className="p-button-rounded"
            label="Confirmar"
            type="submit"
            disabled={shouldDisableSubmitButton()}
            loading={isLoading}
            onClick={() => handleSubmit(answers)}
          />
        </div>
      ),
    }),
    [visible, answers, jobPosting, isLoading]
  )

  const getSliderValue = (e: SliderChangeParams): number =>
    parseInt(e.value.toString())

  const handleSliderChange = (criteriaId: string, value: number): void => {
    setAnswers(prev => {
      return { ...prev, [criteriaId]: value }
    })
  }

  return (
    <Dialog {...dialogProps}>
      <form className="w-[80vw] md:w-[50vw] lg:w-[20vw] flex flex-col gap-10">
        {jobPosting.criterias.map(criteria => (
          <div key={criteria.id}>
            <div className="mb-2 flex justify-between block">
              <label>{criteria.title}</label>
              {answers[criteria.id]}
            </div>
            <Slider
              value={answers[criteria.id]}
              max={5}
              min={1}
              defaultValue={1}
              onChange={e => handleSliderChange(criteria.id, getSliderValue(e))}
            />
          </div>
        ))}
      </form>
    </Dialog>
  )
}

export default JobApplicationDialog
