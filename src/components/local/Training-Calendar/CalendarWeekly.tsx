import { useEffect, useState } from "react"

import { eachDayOfInterval, format } from "date-fns"
import { DateRange } from "react-day-picker"

import { ICalendar } from "@/types/calendar.interface"

import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "@/components/global/atoms/collapsible"
import CalendarEventTab from "@/components/local/Training-Calendar/CalendarEventTab"

interface CalendarWeeklyProps {
  timeOfDay: "Morning" | "Noon" | "Night"
  date: DateRange
  calendarData: ICalendar[]
}

function CalendarWeekly({
  timeOfDay,
  date,
  calendarData
}: CalendarWeeklyProps) {
  const [open, setOpen] = useState(true)
  const daysInWeek: Date[] = eachDayOfInterval({
    start: date.from,
    end: date.to
  })

  const timeSlotRange = {
    Morning: { start: "08:00", end: "12:00" },
    Noon: { start: "13:00", end: "17:00" },
    Night: { start: "18:00", end: "22:00" }
  }

  const getTimeOfDay = (time: string) => {
    if (
      time >= timeSlotRange.Morning.start &&
      time <= timeSlotRange.Morning.end
    )
      return "Morning"
    if (time >= timeSlotRange.Noon.start && time <= timeSlotRange.Noon.end)
      return "Noon"
    if (time >= timeSlotRange.Night.start && time <= timeSlotRange.Night.end)
      return "Night"
    return null
  }

  const [classCodeDayIndices, setClassCodeDayIndices] = useState<
    Map<string, Map<string, number>>
  >(new Map())

  useEffect(() => {
    const dayIndices = new Map<string, Map<string, number>>()

    calendarData.forEach((event) => {
      const formattedDate = format(new Date(event.date), "yyyy-MM-dd")
      if (!dayIndices.has(event.classCode)) {
        dayIndices.set(event.classCode, new Map())
      }
      const dates = dayIndices.get(event.classCode)
      if (!dates.has(formattedDate)) {
        const index = dates.size + 1
        dates.set(formattedDate, index)
      }
    })

    setClassCodeDayIndices(dayIndices)
  }, [calendarData])

  return (
    <Collapsible open={open} onOpenChange={setOpen}>
      <CollapsibleTrigger asChild>
        <button className="flex hover:bg-primary/90 transition-all duration-300 w-full items-center rounded-[10px] bg-primary px-4 py-1 text-white">
          <span className="font-semibold mr-2">{timeOfDay}</span> (
          <span>{timeSlotRange[timeOfDay].start}</span>
          <span className="mx-1">-</span>
          <span>{timeSlotRange[timeOfDay].end}</span>)
        </button>
      </CollapsibleTrigger>
      <CollapsibleContent>
        <div className="mt-2">
          <table className="min-w-full">
            <thead>
              <tr>
                {daysInWeek.map((day, index) => (
                  <th
                    key={index}
                    className="max-w-[50px] border p-2 text-center font-normal"
                  >
                    <p className="text-muted-foreground mb-2">
                      {format(day, "ccc")}
                    </p>
                    <p className="font-semibold text-primary">
                      {format(day, "d")}
                    </p>
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              <tr className="border-b">
                {daysInWeek.map((day, dayIndex) => {
                  const dayKey = format(day, "yyyy-MM-dd")
                  const events = calendarData.filter(
                    (event) =>
                      format(new Date(event.date), "yyyy-MM-dd") === dayKey
                  )

                  return (
                    <td
                      key={dayIndex}
                      className="max-w-[50px] space-y-2 border-x p-2"
                    >
                      {events.map((event, eventIndex) => {
                        const dayIndexMap = classCodeDayIndices.get(
                          event.classCode
                        )
                        const eventTimeOfDay = getTimeOfDay(event.time)
                        const currentDay = dayIndexMap
                          ? dayIndexMap.get(dayKey)
                          : 1
                        const totalDays = dayIndexMap ? dayIndexMap.size : 1
                        if (eventTimeOfDay === timeOfDay) {
                          return (
                            <CalendarEventTab
                              key={eventIndex}
                              attendee={event.attendee}
                              classCode={event.classCode}
                              trainingProgramCode={event.trainingProgramCode}
                              locationId={event.locationId}
                              trainer={event.trainer}
                              admin={event.admin}
                              currentDay={currentDay}
                              totalDays={totalDays}
                              type="week"
                            />
                          )
                        } else {
                          return null
                        }
                      })}
                    </td>
                  )
                })}
              </tr>
            </tbody>
          </table>
        </div>
      </CollapsibleContent>
    </Collapsible>
  )
}

export default CalendarWeekly
