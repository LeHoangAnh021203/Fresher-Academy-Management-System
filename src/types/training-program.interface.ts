import { ISyllabus } from "./syllabus.interface"

export interface ITrainingProgramPreview {
  trainingProgramCode: string
  name: string
  userId: string
  startTime: string
  duration: number //in minutes
  createBy: string
  modifyBy: string
  createDate: Date | string
  modifyDate: Date | string
  status: ITrainingProgramStatus
  class: null //can be remove
  user: null //can be remove
  trainingProgramSyllabuses: null //TODO: apply data type for it
}

export interface ITrainingProgram {
  trainingProgramCode: string
  name: string
  status: ITrainingProgramStatus
  duration: number
  modifyBy: string | null
  modifyDate: Date | string
  syllabuses: ISyllabus[]
}

export enum ITrainingProgramStatus {
  Active = 0,
  Inactive = 1
}
