import { useState } from "react"

export function useLoader() {
  const [loading, setLoading] = useState<boolean>(true)

  const withLoader = async (callback: () => Promise<any>, rethrow = false) => {
    try {
      setLoading(true)
      await callback()
    } catch (e) {
      if (rethrow) {
        throw e
      }
    } finally {
      setLoading(false)
    }
  }

  return { loading, withLoader }
}
