import { useContext } from "react"
import { ToastContext } from "../contexts/ToastContext"

export const useToaster = () => useContext(ToastContext)
