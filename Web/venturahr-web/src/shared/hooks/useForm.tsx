import { useFormik } from "formik"
import { SchemaOf } from "yup"
import React from "react"

interface UseFormParams<T> {
  onSubmit: (values: T) => void
  schema?: SchemaOf<T>
}

interface FieldOptions {
  idField: string
  className: string
}

function useForm<T>({ onSubmit, schema }: UseFormParams<T>) {
  const form = useFormik({
    initialValues: {} as T,
    validationSchema: schema,
    onSubmit,
    validateOnMount: true,
  })

  const isValid = (field: keyof T): boolean => {
    return !form.dirty || !form.touched[field] || !form.errors[field]
  }

  const renderError = (name: keyof T) =>
    !isValid(name) && (
      <small className="p-error">{form.errors[name]?.toString()}</small>
    )

  const field = (field: keyof T, options?: Partial<FieldOptions>) => ({
    [options?.idField || "id"]: field.toString(),
    name: field.toString(),
    value: (form.values[field] as any) || "",
    onChange: form.handleChange,
    onBlur: form.handleBlur,
    className: `${options?.className} ${!isValid(field) ? "p-invalid" : ""}`,
  })

  return { form, isValid, renderError, field }
}

export default useForm
