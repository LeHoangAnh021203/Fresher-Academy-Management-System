import * as z from "zod"

import {
  IUserPermission,
  IUserRoleName
} from "@/types/user-permission.interface"
import { IUserRole } from "@/types/user.interface"

export const UserRoleConfig = z.object({
  permissionId: z.nativeEnum(IUserRole),
  permissionName: z.nativeEnum(IUserRoleName),
  syllabus: z.nativeEnum(IUserPermission),
  learningMaterial: z.nativeEnum(IUserPermission),
  trainingProgram: z.nativeEnum(IUserPermission),
  class: z.nativeEnum(IUserPermission),
  userManagement: z.nativeEnum(IUserPermission),
  version: z.nativeEnum(IUserPermission),
  users: z.any()
})

export const UserRoleConfigSchema = z.array(UserRoleConfig)
