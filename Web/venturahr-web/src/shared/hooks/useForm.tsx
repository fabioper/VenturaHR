import { useFormik } from "formik"
import { SchemaOf } from "yup"
import React from "react"

interface UseFormParams<T> {
  onSubmit: (values: T) => void
  schema?: SchemaOf<T>
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

  return { form, isValid, renderError }
}

export default useForm
