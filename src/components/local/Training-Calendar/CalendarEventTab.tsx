import { programData } from "@/constants/program"
import { Home, Star, User } from "lucide-react"

import { IClass, IClassAttendee, IClassLocation } from "@/types/class.interface"

import {
  Popover,
  PopoverContent,
  PopoverTrigger
} from "@/components/global/atoms/popover"

interface CalendarEventTabProps extends IClass {
  currentDay: number
  totalDays: number
  trainer: string
  admin: string
  type: string
}

function CalendarEventTab({
  attendee,
  classCode,
  trainingProgramCode,
  currentDay,
  totalDays,
  locationId,
  trainer,
  admin,
  type
}: CalendarEventTabProps) {
  const program = programData.find(
    (program) => program.trainingProgramCode === trainingProgramCode
  )

  const locationName = IClassLocation[locationId]

  let buttonColor = "bg-primary"
  switch (attendee) {
    case IClassAttendee.Intern:
      buttonColor = "bg-primary"
      break
    case IClassAttendee.Fresher:
      buttonColor = "bg-[#FF7568]"
      break
    case IClassAttendee.OnlineFeeFresher:
      buttonColor = "bg-[#2F903F]"
      break
    case IClassAttendee.OfflineFeeFresher:
      buttonColor = "bg-[#D45B13]"
      break
    default:
      buttonColor = "bg-primary"
  }

  let content
  switch (type) {
    case "day":
      content = (
        <div className="px-3 py-1 text-sm text-white">
          {classCode} | {program ? program.name : trainingProgramCode}
        </div>
      )
      break
    case "week":
      content = (
        <div className="px-3 py-1 text-[12px] text-white">{classCode}</div>
      )
      break
    default:
      content = <>{classCode}</>
      break
  }

  return (
    <Popover>
      <PopoverTrigger asChild>
        <button className={`rounded-md ${buttonColor}`}>{content}</button>
      </PopoverTrigger>
      <PopoverContent className="w-[300px] space-y-2 shadow-xl rounded-md">
        <p className="font-semibold mb-4">
          Day {currentDay} of {totalDays}
        </p>
        <div className="grid grid-cols-2 gap-7 text-sm">
          <div className="col-span-1 font-semibold flex gap-2 flex-col">
            <p className="flex items-center ">
              <Home size={16} strokeWidth={2.5} className="mr-2" />
              Location
            </p>
            <p className="flex items-center">
              <User size={16} strokeWidth={2.5} className="mr-2" />
              Trainer
            </p>
            <p className="flex items-center">
              <Star size={16} strokeWidth={2.5} className="mr-2" />
              Admin
            </p>
          </div>
          <div className="col-span-1 gap-2 flex flex-col">
            <p className="font-semibold">
              {locationName ? locationName : locationId}
            </p>
            <p className="underline cursor-pointer">{trainer}</p>
            <p className="underline cursor-pointer">{admin}</p>
          </div>
        </div>
      </PopoverContent>
    </Popover>
  )
}

export default CalendarEventTab
