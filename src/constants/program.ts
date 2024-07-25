import { Program, ProgramPreview } from "@/lib/types"

import { syllabusData } from "./syllabus"

export const programData: Program[] = [
  {
    trainingProgramCode: "1",
    name: "DevOps Foundation",
    status: "Active",
    startTime: "21/07/2022",
    createdDate: "15/07/2022",
    createdBy: "ADLS ASLJDN",
    modifiedBy: "ADLS ASLJDN",
    modifiedDate: "23/07/2022",
    duration: 5,
    syllabuses: syllabusData
  },
  {
    trainingProgramCode: "2",
    name: "Data Analytics",
    status: "Active",
    startTime: "21/07/2022",
    createdDate: "15/07/2022",
    createdBy: "ADLS ASLJDN",
    modifiedBy: "ADLS ASLJDN",
    modifiedDate: "23/07/2022",
    duration: 8,
    syllabuses: syllabusData
  }
]

export const programPreviewData: ProgramPreview[] = [
  {
    trainingProgramCode: "10",
    name: "DevOps Foundation",
    status: "Active",
    startTime: "07-21-2022",
    createdOn: "07-15-2022",
    createdBy: "ADLS ASLJDN",
    modifiedBy: "ADLS ASLJDN",
    Date: "07-23-2022",
    duration: 2
  }
]
