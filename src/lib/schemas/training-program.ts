import * as z from "zod"

import { ITrainingProgramStatus } from "@/types/training-program.interface"

import { SyllabusSchema } from "./syllabus"

export const TrainingProgramNewSchema = z.object({
  name: z.string().min(1, { message: "Please enter a title" })
})

export const TrainingProgramSchema = z.object({
  trainingProgramCode: z.string().min(1, { message: "Please enter a title" }),
  name: z.string().min(1, { message: "Please enter a title" }),
  status: z.nativeEnum(ITrainingProgramStatus),
  duration: z.coerce.number().positive(),
  modifyBy: z.string(),
  modifyDate: z
    .string()
    .min(1, { message: "Please enter a title" })
    .or(z.date()),
  syllabuses: z.array(SyllabusSchema)
})
