import { IClassAttendee } from "./class.interface"

export interface ICalendar {
  calendarId: string
  date: string
  admin: string
  trainer: string
  classCode: string
  locationId: string
  time: string
  attendee: IClassAttendee
  trainingProgramCode: string
}
