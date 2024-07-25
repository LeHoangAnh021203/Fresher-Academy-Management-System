import * as z from "zod"

import { IPublishStatus } from "@/types/syllabus.interface"

export const SyllabusNewSchema = z
  .object({
    topicCode: z
      .string()
      .min(3, { message: "Syllabus code must be at least 3 characters" }),
    topicName: z.string().min(1, { message: "Syllabus name is required" }),
    technicalRequirement: z
      .string()
      .min(1, { message: "Technical requirement is required" }),
    courseObjective: z
      .string()
      .min(1, { message: "Course objective is required" }),
    trainingPrinciple: z.string().min(1, { message: "Principle is required" }),
    assessment: z.object({
      assessmentID: z.string(),
      quizCount: z.coerce
        .number()
        .positive()
        .min(0, { message: "Quiz count is required" }),
      quizPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Quiz percent is required" }),
      assignmentCount: z.coerce
        .number()
        .positive()
        .min(0, { message: "Assignment count is required" }),
      assignmentPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Assignment percent is required" }),
      finalTheoryPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Final theory percent is required" }),
      finalPracticePercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Final practice percent is required" })
    })
  })
  .refine(
    (data) => {
      const {
        assignmentPercent,
        quizPercent,
        finalTheoryPercent,
        finalPracticePercent
      } = data.assessment

      return (
        assignmentPercent +
          quizPercent +
          finalTheoryPercent +
          finalPracticePercent ===
        100
      )
    },
    {
      message: "The sum of all percentages must be equal to 100",
      path: ["assessment"]
    }
  )

export const SyllabusSchema = z
  .object({
    topicCode: z
      .string()
      .min(3, { message: "Syllabus code must be at least 3 characters" }),
    topicName: z.string().min(1, { message: "Syllabus name is required" }),
    version: z.coerce.number(),
    technicalGroup: z.string().min(1, { message: "Group is required" }),
    trainingAudience: z.string().min(1, { message: "Audience is required" }),
    topicOutline: z.string().min(1, { message: "Outline is required" }),
    trainingMaterials: z.string().min(1, { message: "Materials is required" }),
    createdBy: z.string().min(1, { message: "Created by is required" }),
    createdDate: z
      .string()
      .min(1, { message: "Created date is required" })
      .or(z.date()),
    modifiedBy: z.string().min(1, { message: "Modified by is required" }),
    modifiedDate: z
      .string()
      .min(1, { message: "Modified date is required" })
      .or(z.date()),
    publishStatus: z.nativeEnum(IPublishStatus),
    technicalRequirement: z
      .string()
      .min(1, { message: "Technical requirement is required" }),
    courseObjective: z
      .string()
      .min(1, { message: "Course objective is required" }),
    trainingPrinciple: z.string().min(1, { message: "Principle is required" }),
    assessment: z.object({
      assessmentID: z.string(),
      quizCount: z.coerce
        .number()
        .positive()
        .min(0, { message: "Quiz count is required" }),
      quizPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Quiz percent is required" }),
      assignmentCount: z.coerce
        .number()
        .positive()
        .min(0, { message: "Assignment count is required" }),
      assignmentPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Assignment percent is required" }),
      finalTheoryPercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Final theory percent is required" }),
      finalPracticePercent: z.coerce
        .number()
        .positive()
        .min(0, { message: "Final practice percent is required" })
    }),
    trainingUnits: z.array(z.any())
  })
  .refine(
    (data) => {
      const {
        assignmentPercent,
        quizPercent,
        finalTheoryPercent,
        finalPracticePercent
      } = data.assessment

      return (
        assignmentPercent +
          quizPercent +
          finalTheoryPercent +
          finalPracticePercent ===
        100
      )
    },
    {
      message: "The sum of all percentages must be equal to 100",
      path: ["assessment"]
    }
  )
