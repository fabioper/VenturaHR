import React, { useEffect, useRef } from "react"

export function useHeaderTransparency(): React.RefObject<HTMLDivElement> {
  const header = useRef<HTMLDivElement>(null)

  useEffect(() => {
    window.addEventListener("scroll", () => {
      if (header.current) {
        if (window.scrollY > 100) {
          header.current.classList.remove("bg-transparent")
        } else {
          header.current.classList.add("bg-transparent")
        }
      }
    })
  }, [])

  return header
}
