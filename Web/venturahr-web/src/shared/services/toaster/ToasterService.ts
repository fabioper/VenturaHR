import { toast, ToastOptions } from "react-toastify"

export class ToasterService {
  private options: ToastOptions = {
    theme: "dark",
    icon: true,
    position: "top-right",
  }

  error(message: string): void {
    toast.error(message, this.options)
  }

  info(message: string): void {
    toast.info(message, this.options)
  }

  success(message: string): void {
    toast.success(message, this.options)
  }

  warn(message: string): void {
    toast.warn(message, this.options)
  }
}
