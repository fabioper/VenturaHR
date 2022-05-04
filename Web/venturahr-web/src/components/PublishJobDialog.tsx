import React from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Editor } from "primereact/editor"
import { Dialog } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"
import InputCurrency from "./InputCurrency"
import useForm from "../hooks/useForm"
import { CreateJobPostingDto } from "../core/dtos/CreateJobPostingDto"
import { createJobPostingValidator } from "../core/validations/create-job-posting.validator"

interface PublishJobDialogProps {
  visible: boolean
  onHide: () => any
}

const initialValues = {
  role: "",
  compensation: 0,
  location: "",
  description: "",
  endDate: new Date(),
}

const PublishJobDialog: React.FC<PublishJobDialogProps> = ({
  visible,
  onHide,
}) => {
  const { form, renderError, isValid } = useForm<CreateJobPostingDto>(
    initialValues,
    createJobPostingValidator,
    values => {
      console.log(values)
    }
  )

  function handleHide(): void {
    form.resetForm()
    onHide()
  }

  return (
    <Dialog
      onHide={() => handleHide()}
      visible={visible}
      header="Publicar vaga"
      dismissableMask
      blockScroll
      focusOnShow={false}
      draggable={false}
      footer={() => (
        <div className="flex justify-end pt-10">
          <Button
            className="p-button-rounded"
            label="Publicar vaga"
            type="submit"
            disabled={!form.isValid}
            onClick={() => form.handleSubmit()}
          />
        </div>
      )}
    >
      <form className="w-full max-w-3xl" onSubmit={form.handleSubmit}>
        <div className="flex flex-col md:grid md:grid-cols-5 gap-5">
          <div className="flex flex-col col-span-3">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="role"
            >
              Cargo
            </label>
            <InputText
              id="role"
              value={form.values.role}
              onChange={form.handleChange}
              onBlur={form.handleBlur}
              className={!isValid("role") ? "p-invalid" : ""}
            />
            {renderError("role")}
          </div>

          <div className="flex flex-col  col-span-2">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="compensation"
            >
              Remuneração
            </label>
            <InputCurrency
              id="compensation"
              name="compensation"
              initialValue={form.values.compensation}
              onChange={value => {
                form.setFieldValue("compensation", value)
              }}
              onBlur={form.handleBlur}
              className={!isValid("compensation") ? "p-invalid" : ""}
            />
            {renderError("compensation")}
          </div>

          <div className="flex flex-col col-span-3">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="location"
            >
              Localização
            </label>
            <div className="p-input-icon-left w-full">
              <i className={PrimeIcons.MAP_MARKER}></i>
              <InputText
                id="location"
                value={form.values.location}
                onChange={form.handleChange}
                onBlur={form.handleBlur}
                className={!isValid("location") ? "w-full p-invalid" : "w-full"}
              />
            </div>
            {renderError("location")}
          </div>

          <div className="flex flex-col col-span-2">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="endDate"
            >
              Data Limite
            </label>
            <Calendar
              inputId="endDate"
              value={form.values.endDate}
              onChange={form.handleChange}
              onBlur={form.handleBlur}
              showIcon
              inputClassName={!isValid("endDate") ? "p-invalid" : ""}
            />
            {renderError("endDate")}
          </div>

          <div className="flex flex-col col-span-5">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="description"
            >
              Descrição
            </label>
            <Editor
              id="description"
              name="description"
              value={form.values.description}
              onTextChange={e => form.setFieldValue("description", e.htmlValue)}
              onBlur={() => {
                form.setFieldTouched("description")
              }}
              style={{ height: "220px" }}
              className={!isValid("description") ? "p-invalid" : ""}
            />
            {renderError("description")}
          </div>
        </div>
      </form>
    </Dialog>
  )
}

export default PublishJobDialog
