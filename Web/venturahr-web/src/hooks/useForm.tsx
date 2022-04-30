import { FormikErrors, useFormik } from "formik"
import { SchemaOf } from "yup"

function useForm<T>(
  initialValues: T,
  validationSchema: SchemaOf<T>,
  onSubmit: (values: T) => void,
  validate?: (values: T) => FormikErrors<T>
) {
  const form = useFormik({
    initialValues,
    validationSchema,
    onSubmit,
    validate,
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
