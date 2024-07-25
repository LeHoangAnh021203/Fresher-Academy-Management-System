import * as z from "zod"

import { IUserGender, IUserRole, IUserStatus } from "@/types/user.interface"

export const UserSchema = z.object({
  permissionId: z.enum([IUserRole[1], IUserRole[2], IUserRole[3]]),
  name: z.string().min(1, { message: "Name is required" }),
  email: z.string().min(1, { message: "Email is required" }).email(),
  dob: z.date().refine(
    (value) => {
      const today = new Date()
      const eighteenYearsAgo = new Date(
        today.getFullYear() - 18,
        today.getMonth(),
        today.getDate()
      )
      return value <= eighteenYearsAgo
    },
    { message: "You must be at least 18 years old" }
  ),
  gender: z.enum([IUserGender[0], IUserGender[1], IUserGender[2]]),
  password: z.string(),
  status: z.enum([IUserStatus[0], IUserStatus[1], IUserStatus[2]]),
  phone: z.string().refine((value) => /^\d{10}$/.test(value), {
    message: "Phone number must be formatted 0xxxxxxxxx"
  })
})
