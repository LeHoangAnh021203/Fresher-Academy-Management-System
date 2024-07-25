import * as z from "zod"

import { IDeliveryType, ITrainingFormat } from "@/types/syllabus.interface"

export const TrainingContentCreateSchema = z.object({
  content: z.string().min(1, { message: "Please enter a name" }),
  duration: z.string().min(1, { message: "Please enter a duration" }),
  code: z.string(),
  deliveryType: z.nativeEnum(IDeliveryType),
  trainingFormat: z.nativeEnum(ITrainingFormat)
})

export const TrainingContentSchema = z.object({
  contentId: z.string(),
  content: z.string(),
  code: z.string(),
  deliveryType: z.nativeEnum(IDeliveryType),
  duration: z.number().positive(),
  trainingFormat: z.nativeEnum(ITrainingFormat),
  note: z.string(),
  unitCode: z.string()
})
