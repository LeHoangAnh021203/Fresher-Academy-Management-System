import * as z from "zod"

import { TrainingContentSchema } from "./training-content"

export const TrainingUnitSchema = z.object({
  unitCode: z.string(),
  unitName: z.string(),
  dayNumber: z.number().positive(),
  topicCode: z.string(),
  trainingContents: z.array(TrainingContentSchema)
})
