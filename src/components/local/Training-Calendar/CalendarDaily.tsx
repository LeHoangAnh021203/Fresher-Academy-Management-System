import { useEffect, useState } from "react"

import { format } from "date-fns"

import { ICalendar } from "@/types/calendar.interface"

import { generateTimeSlots } from "@/lib/utils"

import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "@/components/global/atoms/collapsible"

import CalendarEventTab from "./CalendarEventTab"

interface CalendarDailyProps {
  timeOfDay: "Morning" | "Noon" | "Night"
  date: Date
  calendarData: ICalendar[]
}

function CalendarDaily({ timeOfDay, date, calendarData }: CalendarDailyProps) {
  const [open, setOpen] = useState(true)
  const currentDate = format(new Date(date), "yyyy-MM-dd")
  const [classCodeDatesMap, setClassCodeDatesMap] = useState(
    new Map<string, string[]>()
  )
  const timeSlotRange = {
    Morning: { start: "08:00", end: "12:00" },
    Noon: { start: "13:00", end: "17:00" },
    Night: { start: "18:00", end: "22:00" }
  }

  const timeSlots: string[] = generateTimeSlots(timeOfDay)

  const filteredCalendarData = calendarData.filter(
    (event) => event.date === currentDate
  )

  // console.log(filteredCalendarData)

  useEffect(() => {
    const tempMap = new Map<string, string[]>()

    calendarData.forEach((event) => {
      const formattedDate = format(new Date(event.date), "yyyy-MM-dd")
      const dates = tempMap.get(event.classCode) || []
      if (!dates.includes(formattedDate)) {
        tempMap.set(event.classCode, [...dates, formattedDate])
      }
    })

    setClassCodeDatesMap(tempMap)
  }, [calendarData])

  return (
    <Collapsible open={open} onOpenChange={setOpen}>
      <CollapsibleTrigger asChild>
        <button className="flex hover:bg-primary/90 transition-all duration-300 w-full items-center rounded-[10px] bg-primary px-4 py-1 text-white">
          <span className="mr-2">{timeOfDay}</span> (
          <span>{timeSlotRange[timeOfDay].start}</span>
          <span className="mx-1">-</span>
          <span>{timeSlotRange[timeOfDay].end}</span>)
        </button>
      </CollapsibleTrigger>
      <CollapsibleContent>
        <div className="mt-2">
          <table className="w-full">
            <tbody>
              {timeSlots.map((timeSlot, index) => (
                <tr key={index} className="border-b">
                  <td className="min-h-[40px] w-[7%] border-r px-4 py-2 text-center">
                    {timeSlot}
                  </td>
                  <td className="flex min-h-[40px] w-full flex-wrap gap-4 px-4 py-2">
                    {filteredCalendarData
                      .filter((event) => event.time === timeSlot)
                      .map((event) => {
                        const eventDateFormatted = format(
                          new Date(event.date),
                          "yyyy-MM-dd"
                        )
                        const dates =
                          classCodeDatesMap.get(event.classCode) || []
                        const totalDays = dates.length
                        const currentDay = dates.indexOf(eventDateFormatted) + 1

                        return (
                          <CalendarEventTab
                            key={event.calendarId}
                            attendee={event.attendee}
                            classCode={event.classCode}
                            trainingProgramCode={event.trainingProgramCode}
                            currentDay={currentDay}
                            totalDays={totalDays}
                            locationId={event.locationId}
                            trainer={event.trainer}
                            admin={event.admin}
                            type="day"
                          />
                        )
                      })}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </CollapsibleContent>
    </Collapsible>
  )
}

export default CalendarDaily
