import { IUserRole } from "./user.interface"

export interface IUserRoleConfigItem {
  permissionId: IUserRole
  permissionName: IUserRoleName
  syllabus: IUserPermission
  learningMaterial: IUserPermission
  trainingProgram: IUserPermission
  class: IUserPermission
  userManagement: IUserPermission
  version: number
}

export type IUserRoleConfig = IUserRoleConfigItem[]

export enum IUserPermission {
  AccessDenied = 0,
  View = 1,
  Modify = 2,
  Create = 3,
  FullAccess = 4
}

export enum IUserRoleName {
  SuperAdmin = "Super Admin",
  ClassAdmin = "Class Admin",
  Trainer = "Trainer"
}

export enum IRouteToAccess {
  Syllabus = "syllabus",
  TrainingProgram = "trainingProgram",
  Class = "class",
  UserManagement = "userManagement",
  LearningMaterial = "learningMaterial"
}
