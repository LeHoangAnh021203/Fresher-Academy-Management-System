import { z } from "zod"

export const CreateClassSchema = z.object({
  ClassName: z.string().min(1, "Class name is required"),
  startClassTime: z.string().min(1, "Start time is required"),
  endClassTime: z.string().min(1, "End time is required"),
  location: z.string().optional(),
  trainers: z.array(z.string()).optional(),
  admins: z.array(z.string()).optional(),
  fsu: z.string().optional(),
  email: z.string().email().optional(),
  programName: z.string().min(1, "Program name is required"),
  syllabuses: z.array(z.string()).optional()
})
