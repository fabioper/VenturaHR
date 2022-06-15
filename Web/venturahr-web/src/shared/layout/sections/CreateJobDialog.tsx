import React, { useMemo } from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Dialog, DialogProps } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"
import InputCurrency from "../../components/InputCurrency"
import useForm from "../../hooks/useForm"
import { CreateJobRequest } from "../../../core/dtos/requests/CreateJobRequest"
import { createJobSchema } from "../../../core/validations/CreateJobSchema"
import { postJob } from "../../../core/services/JobPostingsService"
import MDEditor from "../../components/MDEditor/MDEditor"

interface PostJobDialogProps {
  visible: boolean
  onHide: () => any
}

const CreateJobDialog: React.FC<PostJobDialogProps> = ({ visible, onHide }) => {
  const { form, renderError, isValid, field } = useForm<CreateJobRequest>({
    onSubmit,
    schema: createJobSchema,
  })

  async function onSubmit(values: CreateJobRequest): Promise<void> {
    try {
      await postJob(values)
    } catch (e) {
      console.log(e)
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
            onClick={() => form.handleSubmit()}
          />
        </div>
      ),
    }),
    [visible, form]
  )

  return (
    <Dialog {...dialogProps}>
      <form className="w-full min-w-[50vw]" onSubmit={form.handleSubmit}>
        <div className="flex flex-col md:grid md:grid-cols-5 gap-5">
          <div className="flex flex-col col-span-3">
            <label htmlFor="title">Cargo</label>
            <InputText {...field("title")} />
            {renderError("title")}
          </div>

          <div className="flex flex-col col-span-2">
            <label htmlFor="salary">Remuneração</label>
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

          <div className="flex flex-col col-span-3">
            <label htmlFor="location">Localização</label>
            <div className="p-input-icon-left w-full">
              <i className={PrimeIcons.MAP_MARKER}></i>
              <InputText {...field("location")} />
            </div>
            {renderError("location")}
          </div>

          <div className="flex flex-col col-span-2">
            <label htmlFor="expirationDate">Data Limite</label>
            <Calendar
              {...field("expirationDate", { idField: "inputId" })}
              showButtonBar
              dateFormat="dd/mm/yy"
              minDate={new Date()}
            />
            {renderError("expirationDate")}
          </div>

          <div className="flex flex-col col-span-5">
            <label htmlFor="description">Descrição</label>
            <MDEditor {...field("description")} />
            {renderError("description")}
          </div>
        </div>
      </form>
    </Dialog>
  )
}

export default CreateJobDialog
