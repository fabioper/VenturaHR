import { useState } from "react"

export function useLoader() {
  const [loading, setLoading] = useState<boolean>(true)

  const withLoader = async (callback: () => Promise<any>) => {
    try {
      setLoading(true)
      await callback()
    } catch (e) {
      console.log(e)
    } finally {
      setLoading(false)
    }
  }
  return { loading, withLoader }
}
