import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

import { IClassFSU } from "@/types/class.interface"
import {
  IDeliveryType,
  ISyllabus,
  ITrainingUnit
} from "@/types/syllabus.interface"
import { ITrainingProgram } from "@/types/training-program.interface"
import {
  IRouteToAccess,
  IUserPermission,
  IUserRoleConfig
} from "@/types/user-permission.interface"
import { IJwtPayload, IUserRole } from "@/types/user.interface"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

type TimeConversion = {
  days: number
  hours: number
}

export function convertToDaysAndHours(input: number): TimeConversion {
  if (input < 0) {
    throw new Error("Invalid input. Please provide a non-negative number.")
  }

  const days = Math.floor(input / 24)
  const hours = input % 24

  return {
    days,
    hours
  }
}

export function calculateTotalDurationTrainingUnit(
  trainingUnit: ITrainingUnit
): number {
  let totalDuration = 0

  trainingUnit.trainingContents.forEach((content) => {
    // Add duration to total
    totalDuration += content.duration
  })

  return totalDuration / 60
}

export function calculateTotalDurationOfSyllabus(syllabus: ISyllabus): {
  days: number
  learningTime: number
} {
  let learningTime = 0
  let days = 0

  syllabus.trainingUnits.forEach((unit: ITrainingUnit) => {
    learningTime += calculateTotalDurationTrainingUnit(unit)
    days = Math.max(days, unit.dayNumber)
  })

  learningTime = +learningTime.toFixed(2)

  return { learningTime, days }
}

export function calculateTotalDurationOfTrainingProgram(
  syllabuses: ISyllabus[]
): {
  days: number
  learningTime: number
} {
  let totalDays = 0
  let totalHours = 0

  syllabuses.forEach((syllabus: ISyllabus) => {
    const temp = calculateTotalDurationOfSyllabus(syllabus)

    totalDays += temp.days
    totalHours += temp.learningTime
  })

  return { days: totalDays, learningTime: totalHours }
}

export function generateTimeSlots(timeOfDay: "Morning" | "Noon" | "Night") {
  const timeSlots: string[] = []
  let startTime, endTime

  if (timeOfDay === "Morning") {
    startTime = 8
    endTime = 12
  } else if (timeOfDay === "Noon") {
    startTime = 13
    endTime = 17
  } else if (timeOfDay === "Night") {
    startTime = 18
    endTime = 22
  } else {
    // Handle other cases if necessary
    return timeSlots
  }

  for (let hour = startTime; hour < endTime; hour++) {
    // Thay đổi điều kiện từ '<=' thành '<'
    for (let minute = 0; minute < 60; minute += 30) {
      const formattedHour = hour < 10 ? `0${hour}` : hour
      const formattedMinute = minute === 0 ? "00" : minute
      const time = `${formattedHour}:${formattedMinute}`

      timeSlots.push(time)
    }
  }

  return timeSlots
}

export function calculateTimeAllocation(trainingUnits: ITrainingUnit[]) {
  const timeAllocation: { [key in IDeliveryType]: number } = {
    [IDeliveryType.AssignmentLab]: 0,
    [IDeliveryType.ConceptLecture]: 0,
    [IDeliveryType.GuideReview]: 0,
    [IDeliveryType.TestQuiz]: 0,
    [IDeliveryType.Exam]: 0,
    [IDeliveryType.SeminarWorkshop]: 0
  }

  for (const unit of trainingUnits) {
    for (const content of unit.trainingContents) {
      timeAllocation[content.deliveryType] += content.duration
    }
  }

  // Filter out entries where value is 0
  const nonZeroTimeAllocation = Object.fromEntries(
    Object.entries(timeAllocation).filter(([, value]) => value !== 0)
  )

  return nonZeroTimeAllocation
}

// Format Date Vietnam
export const formatDateVN = (datetimeStr: string): string => {
  const date = new Date(datetimeStr)
  const day = date.getDate().toString().padStart(2, "0")
  const month = (date.getMonth() + 1).toString().padStart(2, "0")
  const year = date.getFullYear()
  return `${day}/${month}/${year}`
}

export function calculateTotalDurationForSelectedProgram(
  program: ITrainingProgram
): {
  totalDays: number
  totalLearningTime: number
} {
  let totalDays = 0
  let totalLearningTime = 0

  if (Array.isArray(program.syllabuses)) {
    program.syllabuses.forEach((syllabus) => {
      const syllabusDuration = calculateTotalDurationOfSyllabus(syllabus)

      totalDays += syllabusDuration.days
      totalLearningTime += syllabusDuration.learningTime
    })
  }

  return { totalDays, totalLearningTime }
}

// Automatic generate Class Code when created a new Class by Rule <<Location Code>>+<<Current Year(yy)>>+<<Incremental Number (01-99)>>
function getRandomIncrementalNumber() {
  return Math.floor(Math.random() * 99) + 1
}

export function generateClassCode(fsuId, programName) {
  const year = new Date().getFullYear().toString().slice(2)

  const fsuCode = IClassFSU[fsuId] || ""

  const programCode = programName.split(" ")[0]

  const randomNumber = getRandomIncrementalNumber().toString().padStart(2, "0")

  // Cấu trúc mã lớp: HCM<YY>_<FSU>_<PROGRAM CODE>_<INCREMENTAL NUMBER>
  const classCode = `HCM${year}_${fsuCode}_${programCode}_${randomNumber}`

  return classCode
}

export function convertMinutesToHoursAndMinutes(minutes: number): {
  hours: number
  minutes: number
} {
  const hours = Math.floor(minutes / 60)
  const minutesRemaining = minutes % 60
  return { hours, minutes: minutesRemaining }
}

export function jwtDecode(token: string): IJwtPayload {
  const base64Url = token.split(".")[1]
  const base64 = base64Url.replace("-", "+").replace("_", "/")
  return JSON.parse(window.atob(base64))
}

export function hasPermission({
  inputRole,
  routeToAccess,
  roleConfig,
  funcToAccess
}: {
  inputRole: IUserRole
  routeToAccess: IRouteToAccess
  roleConfig: IUserRoleConfig
  funcToAccess: IUserPermission
}): boolean {
  const role = roleConfig.find((item) => item.permissionId === inputRole)
  if (role && role[routeToAccess]) {
    const userPermissionLevel: number = role[routeToAccess]
    return userPermissionLevel >= funcToAccess
  }
  return false
}
