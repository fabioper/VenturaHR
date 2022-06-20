import React, { useCallback, useMemo } from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Dialog, DialogProps } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"
import useForm from "../../../hooks/useForm"
import { CreateJobRequest } from "../../../../core/dtos/requests/CreateJobRequest"
import { createJobSchema } from "../../../../core/validations/CreateJobSchema"
import { postJob } from "../../../../core/services/JobPostingsService"
import MDEditor from "../../../components/MDEditor/MDEditor"
import CriteriaForm from "../CriteriaForm/CriteriaForm"
import { CriteriaRequest } from "../../../../core/dtos/requests/CriteriaRequest"
import { useToaster } from "../../../hooks/useToaster"
import InputCurrency from "../../../components/InputCurrency/InputCurrency"

interface PostJobDialogProps {
  visible: boolean
  onHide: () => any
}

const CreateJobDialog: React.FC<PostJobDialogProps> = ({ visible, onHide }) => {
  const { toast } = useToaster()
  const { form, renderError, isValid, field } = useForm<CreateJobRequest>({
    onSubmit,
    schema: createJobSchema,
  })

  async function onSubmit(values: CreateJobRequest): Promise<void> {
    try {
      await postJob(values)
      toast.success("Vaga publicada com sucesso!")
      handleHide()
    } catch (e) {
      toast.error("Ocorreu um erro ao publicar esta vaga.")
    }
  }

  function handleHide(): void {
    form.resetForm()
    onHide()
  }

  const dialogProps: DialogProps = useMemo(
    () => ({
      onHide: handleHide,
      visible: visible,
      header: "Publicar vaga",
      dismissableMask: true,
      blockScroll: true,
      focusOnShow: false,
      draggable: false,
      footer: () => (
        <div className="flex justify-end pt-10">
          <Button
            className="p-button-rounded"
            label="Publicar vaga"
            type="submit"
            disabled={!form.isValid}
            loading={form.isSubmitting || form.isValidating}
            onClick={() => form.handleSubmit()}
          />
        </div>
      ),
    }),
    [visible, form]
  )

  const updateCriterias = useCallback((criterias: CriteriaRequest[]): void => {
    form.setFieldValue("criterias", criterias)
  }, [])

  return (
    <Dialog {...dialogProps}>
      <form className="w-full min-w-[20vw]" onSubmit={form.handleSubmit}>
        <div className="flex flex-col md:grid md:grid-cols-5 gap-5">
          <div className="flex flex-col col-span-5">
            <label htmlFor="title">Título</label>
            <InputText {...field("title")} />
            {renderError("title")}
          </div>

          <div className="flex flex-col col-span-3">
            <label htmlFor="location">Localização</label>
            <div className="p-input-icon-left w-full">
              <i className={PrimeIcons.MAP_MARKER}></i>
              <InputText {...field("location")} />
            </div>
            {renderError("location")}
          </div>

          <div className="flex flex-col col-span-2">
            <label htmlFor="salary">Salário</label>
            <InputCurrency
              id="salary"
              name="salary"
              onBlur={form.handleBlur}
              onChange={value => form.setFieldValue("salary", value)}
              className={!isValid("salary") ? "p-invalid" : ""}
              initialValue={form.values.salary}
            />
            {renderError("salary")}
          </div>

          <div className="flex flex-col col-span-5">
            <label htmlFor="description">Descrição</label>
            <MDEditor {...field("description")} />
            {renderError("description")}
          </div>

          <div className="flex flex-col col-span-5">
            <label>Critérios</label>
            <CriteriaForm onChange={criterias => updateCriterias(criterias)} />
          </div>

          <div className="flex flex-col col-span-5">
            <label htmlFor="expirationDate">Data Limite</label>
            <Calendar
              {...field("expirationDate", { idField: "inputId" })}
              showButtonBar
              dateFormat="dd/mm/yy"
              minDate={new Date()}
            />
            {renderError("expirationDate")}
          </div>
        </div>
      </form>
    </Dialog>
  )
}

export default CreateJobDialog
