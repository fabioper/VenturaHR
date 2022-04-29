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
  })

  const isValid = (field: keyof T): boolean =>
    !form.touched[field] || !form.errors[field]

  const getFormErrorMessage = (name: keyof T) =>
    !isValid(name) && (
      <small className="p-error">{form.errors[name]?.toString()}</small>
    )

  return { form, isValid, getFormErrorMessage }
}

export default useForm
