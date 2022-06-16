import { useEffect, useState } from "react"
import { CriteriaRequest } from "../../../../core/dtos/requests/CriteriaRequest"
import { DesiredProfile } from "../../../../core/enums/DesiredProfile"

export function useCriteriaForm(
  onChange: (criterias: CriteriaRequest[]) => void
) {
  const [criterias, setCriterias] = useState<CriteriaRequest[]>([])

  useEffect(() => {
    onChange(criterias)
  }, [criterias])

  function getEmptyCriteria(): CriteriaRequest {
    return {
      title: "",
      description: "",
      weight: 1,
      desiredProfile: DesiredProfile.Desirable,
    }
  }

  function update(index: number, updatedCriteria: CriteriaRequest) {
    setCriterias(originalCriterias =>
      originalCriterias.map((original, i) => {
        return i === index ? updatedCriteria : original
      })
    )
  }

  function add() {
    setCriterias(prev => [...prev, getEmptyCriteria()])
  }

  function remove(criteriaIndex: number) {
    return setCriterias(prev => {
      return prev.filter((c, index) => index !== criteriaIndex)
    })
  }

  return { values: criterias, update, add, remove }
}
