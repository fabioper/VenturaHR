import React, { useEffect, useState } from "react"
import CurrencyInput from "react-currency-input-field"
import currency from "currency.js"
import { InputText } from "primereact/inputtext"

interface InputCurrencyProps {
  id: string
  name: string
  initialValue?: number
  disabled?: boolean
  className?: string
  autoFocus?: boolean
  onChange: (value: number | undefined) => void
  onBlur?: React.FocusEventHandler<HTMLInputElement>
}

const InputCurrency: React.FC<InputCurrencyProps> = ({
  initialValue,
  id,
  name,
  onChange,
  disabled,
  className,
  autoFocus,
  onBlur,
}) => {
  const [maskedValue, setMaskedValue] = useState<string>()
  const [currentValue, setCurrentValue] = useState<number>()

  function fromCurrency(value: string | undefined) {
    if (value === undefined) return value
    const opts: currency.Options = { decimal: ",", separator: "." }
    return currency(value, opts).value
  }

  useEffect(() => setCurrentValue(fromCurrency(maskedValue)), [maskedValue])
  useEffect(() => onChange(currentValue), [currentValue])

  useEffect(() => {
    initialValue &&
      !initialValue.toString().endsWith(",") &&
      setMaskedValue(initialValue.toString())
  }, [])

  return (
    <CurrencyInput
      id={id}
      name={name}
      autoComplete="off"
      intlConfig={{ locale: "pt-BR", currency: "BRL" }}
      decimalsLimit={2}
      decimalScale={2}
      value={maskedValue}
      inputMode="tel"
      customInput={InputText}
      disabled={disabled}
      className={className}
      autoFocus={autoFocus}
      onBlur={onBlur}
      onValueChange={value => setMaskedValue(value)}
    />
  )
}

export default InputCurrency
