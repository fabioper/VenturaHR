import React from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Editor } from "primereact/editor"
import { Dialog } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"
import InputCurrency from "./InputCurrency"

interface PublishJobDialogProps {
  visible: boolean
  onHide: () => any
}

const PublishJobDialog: React.FC<PublishJobDialogProps> = ({
  visible,
  onHide,
}) => (
  <Dialog
    onHide={onHide}
    visible={visible}
    header="Publicar vaga"
    dismissableMask
    blockScroll
    focusOnShow={false}
    draggable={false}
    footer={() => (
      <div className="flex justify-end pt-10">
        <Button className="p-button-rounded" label="Publicar vaga" />
      </div>
    )}
  >
    <form className="max-w-3xl">
      <div className="grid grid-cols-5 gap-5">
        <div className="flex flex-col gap-2 col-span-3">
          <label className="text-slate-300 text-base font-display">Cargo</label>
          <InputText />
        </div>

        <div className="flex flex-col gap-2  col-span-2">
          <label className="text-slate-300 text-base font-display">
            Remuneração
          </label>
          <InputCurrency
            id="compensation"
            name="compensation"
            onChange={() => {}}
          />
        </div>

        <div className="flex flex-col gap-2 col-span-3">
          <label className="text-slate-300 text-base font-display">
            Localização
          </label>
          <div className="p-inputgroup">
            <span className="p-inputgroup-addon">
              <i className={PrimeIcons.MAP_MARKER}></i>
            </span>
            <InputText />
          </div>
        </div>

        <div className="flex flex-col gap-2 col-span-2">
          <label className="text-slate-300 text-base font-display">
            Data Limite
          </label>
          <Calendar showIcon />
        </div>

        <div className="flex flex-col gap-2 col-span-5">
          <label className="text-slate-300 text-base font-display">
            Descrição
          </label>
          <Editor style={{ height: "220px" }} />
        </div>
      </div>
    </form>
  </Dialog>
)

export default PublishJobDialog
