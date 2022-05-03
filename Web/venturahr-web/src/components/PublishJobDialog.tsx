import React from "react"
import { Button } from "primereact/button"
import { InputText } from "primereact/inputtext"
import { Calendar } from "primereact/calendar"
import { Editor } from "primereact/editor"
import { Dialog } from "primereact/dialog"
import { PrimeIcons } from "primereact/api"

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
        <Button
          className="p-button-rounded"
          label="Publicar vaga"
          icon={PrimeIcons.CHECK}
          iconPos="right"
        />
      </div>
    )}
  >
    <form className="max-w-3xl">
      <div className="grid grid-cols-2 gap-5">
        <div className="flex flex-col gap-2">
          <label className="text-slate-300 text-base font-display">Cargo</label>
          <InputText placeholder="Ex.: Desenvolvedor Front-end" />
        </div>

        <div className="flex flex-col gap-2">
          <label className="text-slate-300 text-base font-display">
            Remuneração
          </label>
          <InputText placeholder="Ex.: R$ 14.000,00" />
        </div>

        <div className="flex flex-col gap-2">
          <label className="text-slate-300 text-base font-display">
            Localização
          </label>
          <div className="p-inputgroup">
            <InputText placeholder="Ex.: Rio de Janeiro, RJ, Brasil" />
            <span className="p-inputgroup-addon">
              <i className={PrimeIcons.MAP_MARKER}></i>
            </span>
          </div>
        </div>

        <div className="flex flex-col gap-2">
          <label className="text-slate-300 text-base font-display">
            Data Limite
          </label>
          <Calendar showIcon />
        </div>

        <div className="flex flex-col gap-2 col-span-2">
          <label className="text-slate-300 text-base font-display">
            Descrição
          </label>
          <Editor style={{ height: "320px" }} />
        </div>
      </div>
    </form>
  </Dialog>
)

export default PublishJobDialog
