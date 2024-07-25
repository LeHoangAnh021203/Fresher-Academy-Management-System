export interface IClass {
  classID: string //check
  className: string //check
  classCode: string //check
  duration: number //check
  status: IClassStatus //number //check
  startClassTime: string
  endClassTime: string
  locationId: string //check
  trainers: string[]
  admins: string[]
  fsuId: string //check
  email: string
  dates: string[]
  createdBy: string //check
  createdDate: string //check
  modifiedBy: string //check
  modifiedDate: string //check
  attendee: IClassAttendee //number
  trainingProgramCode: string //check
}

export enum IClassStatus {
  Planning = "Planning",
  Scheduled = "Scheduled",
  Opening = "Opening",
  Completed = "Completed"
}

export enum IClassLocation {
  L001 = "FTown 1",
  L002 = "FTown 2",
  L003 = "FTown 3",
  L004 = "FTown 4"
}

export enum IClassFSU {
  F001 = "FHM",
  F002 = "FDM",
  F003 = "FSE",
  F004 = "FWB",
  F005 = "FWA"
}

export const IClassEmail = ["HuyQuac@fsoft.com.vn", "DatNT135H@fpt.com"]

export enum IClassAttendee {
  Fresher = 0,
  OnlineFeeFresher = 1,
  OfflineFeeFresher = 2,
  Intern = 3
}
