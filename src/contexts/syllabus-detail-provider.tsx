import React, { ReactNode, createContext, useContext, useState } from "react"

import { Syllabus, TrainingUnit } from "@/lib/types"

interface SyllabusDetailContextType {
  isEdit: boolean
  setIsEdit: React.Dispatch<React.SetStateAction<boolean>>
  trainingUnits: TrainingUnit[]
  setTrainingUnits: React.Dispatch<React.SetStateAction<TrainingUnit[]>>
  syllabus: Syllabus | null
  setSyllabus: React.Dispatch<React.SetStateAction<Syllabus | null>>
}

export const SyllabusDetailContext = createContext<
  SyllabusDetailContextType | undefined
>(undefined)

export const useSyllabusDetailContext = (): SyllabusDetailContextType => {
  const context = useContext(SyllabusDetailContext)
  if (!context) {
    throw new Error(
      "useSyllabusDetailContext must be used within a SyllabusDetailProvider"
    )
  }
  return context
}

export const SyllabusDetailProvider: React.FC<{ children: ReactNode }> = ({
  children
}) => {
  const [isEdit, setIsEdit] = useState<boolean>(false)
  const [syllabus, setSyllabus] = useState<Syllabus | null>(null)
  const [trainingUnits, setTrainingUnits] = useState<TrainingUnit[]>([])

  return (
    <SyllabusDetailContext.Provider
      value={{
        isEdit,
        setIsEdit,
        syllabus,
        setSyllabus,
        trainingUnits,
        setTrainingUnits
      }}
    >
      {children}
    </SyllabusDetailContext.Provider>
  )
}
