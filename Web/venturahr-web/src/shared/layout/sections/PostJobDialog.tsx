import React from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Editor } from "primereact/editor"
import { Dialog } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"
import InputCurrency from "../../components/InputCurrency"
import useForm from "../../hooks/useForm"
import { PostJobModel } from "../../../core/dtos/jobposting/PostJobModel"
import { postJobValidator } from "../../../core/validations/post-job.validator"
import { postJob } from "../../../core/services/JobPostingsService"

interface PostJobDialogProps {
  visible: boolean
  onHide: () => any
}

const PostJobDialog: React.FC<PostJobDialogProps> = ({ visible, onHide }) => {
  const { form, renderError, isValid } = useForm<PostJobModel>({
    onSubmit,
    schema: postJobValidator,
  })

  async function onSubmit(values: PostJobModel): Promise<void> {
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

  return (
    <Dialog
      onHide={handleHide}
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
              htmlFor="title"
            >
              Cargo
            </label>
            <InputText
              id="title"
              value={form.values.title}
              onChange={form.handleChange}
              onBlur={form.handleBlur}
              className={!isValid("title") ? "p-invalid" : ""}
            />
            {renderError("title")}
          </div>

          <div className="flex flex-col  col-span-2">
            <label
              className="text-slate-300 text-base font-display mb-2"
              htmlFor="salary"
            >
              Remuneração
            </label>
            <InputCurrency
              id="salary"
              name="salary"
              initialValue={form.values.salary}
              onChange={value => {
                form.setFieldValue("salary", value)
              }}
              onBlur={form.handleBlur}
              className={!isValid("salary") ? "p-invalid" : ""}
            />
            {renderError("salary")}
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
              htmlFor="expirationDate"
            >
              Data Limite
            </label>
            <Calendar
              inputId="expirationDate"
              value={form.values.expirationDate}
              onChange={form.handleChange}
              onBlur={form.handleBlur}
              showButtonBar
              dateFormat="dd/mm/yy"
              minDate={new Date()}
              inputClassName={!isValid("expirationDate") ? "p-invalid" : ""}
            />
            {renderError("expirationDate")}
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

export default PostJobDialog
