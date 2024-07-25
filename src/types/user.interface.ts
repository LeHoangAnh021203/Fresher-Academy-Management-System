export interface IUser {
  userId: string
  name: string
  email: string
  password: string
  phoneNum: string
  role: string
  dob: Date
  gender: IUserGender
  status: IUserStatus
  createdBy: string
  createdOn: string
  modifiedBy: string
  modifiedOn: string
  permissionId: IUserRole
}

export interface IJwtPayload {
  UserId: string
  UserName: string
  Email: string
  Role: IUserRole
  exp: number
}

export enum IUserStatus {
  Active = 0,
  Inactive = 1,
  Suspended = 2
}

export enum IUserRole {
  SuperAdmin = 1,
  ClassAdmin = 2,
  Trainer = 3
}

export enum IUserGender {
  Other = 0,
  Male = 1,
  Female = 2
}
