import * as z from "zod"

export const FilterSchema = z.object({
  location: z.array(
    z.object({
      label: z.string(),
      value: z.string()
    })
  )
  //   from: z.date(),
  //   to: z.date(),
  //   classTime: z.enum(["Morning", "Noon", "Evening", "Online"]),
  //   status: z.enum(["Planning", "Opening", "Close"]),
  //   attendee: z.enum(["Intern", "Fresher", "Online fee-fresher", "Offline fee-fresher"]),
  //   fsu: z.string(),
  //   trainer: z.string()
})
