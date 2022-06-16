import React from "react"
import { CriteriaRequest } from "../../../../core/dtos/requests/CriteriaRequest"
import { DesiredProfile } from "../../../../core/enums/DesiredProfile"
import { InputText } from "primereact/inputtext"
import { SelectItem } from "primereact/selectitem"
import { Dropdown } from "primereact/dropdown"
import { InputNumber } from "primereact/inputnumber"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"
import { useCriteriaForm } from "./useCriteriaForm"

interface CriteriaFormProps {
  onChange: (criterias: CriteriaRequest[]) => void
}

const desiredProfileOptions: SelectItem[] = [
  { label: "Desejável", value: DesiredProfile.Desirable },
  { label: "Diferencial", value: DesiredProfile.Differential },
  { label: "Mandatório", value: DesiredProfile.Mandatory },
  { label: "Relevante", value: DesiredProfile.Relevant },
  { label: "Muito importante", value: DesiredProfile.VeryRelevant },
]

const CriteriaForm: React.FC<CriteriaFormProps> = ({ onChange }) => {
  const criterias = useCriteriaForm(onChange)

  if (!criterias.values.length) {
    return (
      <div className="flex flex-col gap-5 items-center">
        <div className="p-10 text-center text-slate-600 text-sm">
          <span>Nenhum critério adicionado</span>
        </div>
        <Button
          icon={PrimeIcons.PLUS}
          className="p-button-sm p-button-rounded p-button-outlined"
          label="Novo critério"
          onClick={() => criterias.add()}
        />
      </div>
    )
  }

  return (
    <div className="flex flex-col gap-5 items-center">
      <div className="flex flex-col gap-5 py-5">
        {criterias.values.map((criteria, index) => (
          <div
            className="grid grid-cols-[1fr_1fr_1fr_60px_auto] gap-2 items-end"
            key={index}
          >
            <div>
              <label className="text-xs">Título</label>
              <InputText
                value={criteria.title}
                onChange={e =>
                  criterias.update(index, {
                    ...criteria,
                    title: e.target.value,
                  })
                }
              />
            </div>
            <div>
              <label className="text-xs">Descrição</label>
              <InputText
                value={criteria.description}
                onChange={e =>
                  criterias.update(index, {
                    ...criteria,
                    description: e.target.value,
                  })
                }
              />
            </div>
            <div>
              <label className="text-xs">Perfil Desejado</label>
              <Dropdown
                className="w-full"
                value={criteria.desiredProfile}
                options={desiredProfileOptions}
                onChange={e =>
                  criterias.update(index, {
                    ...criteria,
                    desiredProfile: e.target.value,
                  })
                }
              />
            </div>
            <div>
              <label className="text-xs">Peso</label>
              <InputNumber
                value={criteria.weight}
                min={1}
                max={5}
                onChange={e =>
                  criterias.update(index, { ...criteria, weight: e.value || 1 })
                }
              />
            </div>

            <div>
              <Button
                icon={PrimeIcons.TRASH}
                className="p-button-sm p-button-text p-button-danger"
                onClick={() => criterias.remove(index)}
              />
            </div>
          </div>
        ))}
      </div>
      <Button
        icon={PrimeIcons.PLUS}
        className="p-button-sm p-button-rounded p-button-outlined"
        label="Novo critério"
        onClick={() => criterias.add()}
      />
    </div>
  )
}

export default CriteriaForm
