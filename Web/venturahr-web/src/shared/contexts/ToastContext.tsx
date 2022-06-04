import React, { createContext, useMemo } from "react"
import { ToastContainer } from "react-toastify"
import { ToasterService } from "../services/toaster/ToasterService"

export const ToastContext = createContext<ToasterService>(new ToasterService())

const ToastProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const service = useMemo(() => new ToasterService(), [])

  return (
    <ToastContext.Provider value={service}>
      {children}
      <ToastContainer />
    </ToastContext.Provider>
  )
}

export default ToastProvider
