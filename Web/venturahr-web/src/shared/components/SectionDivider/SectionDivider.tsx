import React from "react"

interface DividerProps {
  children: React.ReactNode
}

const SectionDivider: React.FC<DividerProps> = ({ children }) => (
  <div className="my-7 grid grid-cols-[1fr_auto_1fr] gap-x-2 items-center">
    <span className="h-0.5 bg-slate-800" />
    <span className="text-slate-500 text-sm font-normal">{children}</span>
    <span className="h-0.5 bg-slate-800" />
  </div>
)

export default SectionDivider
