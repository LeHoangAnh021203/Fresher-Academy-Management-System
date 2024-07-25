import * as React from "react"

import { CalendarIcon } from "@radix-ui/react-icons"
import { endOfWeek, format, startOfWeek } from "date-fns"
import { DateRange } from "react-day-picker"

import { cn } from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import { Calendar } from "@/components/global/atoms/calendar"
import {
  Popover,
  PopoverContent,
  PopoverTrigger
} from "@/components/global/atoms/popover"

function CalendarDateWeekPicker({
  className,
  onChange,
  initialDate
}: {
  className?: React.HTMLAttributes<HTMLDivElement>
  onChange?: (selectedDate: DateRange) => void
  initialDate?: DateRange | undefined
}) {
  const today = new Date()
  const startOfTodayWeek = startOfWeek(today)
  const endOfTodayWeek = endOfWeek(today)

  const [date, setDate] = React.useState<DateRange | undefined>({
    from: initialDate?.from || startOfTodayWeek,
    to: initialDate?.to || endOfTodayWeek
  })

  const handleSelect = (selectedDate: Date) => {
    try {
      if (!selectedDate) {
        console.log("Cannot pick the date in the same week")
        return // Do not update the state if it's the same week
      }

      const startOfWeekDate = startOfWeek(selectedDate)
      const endOfWeekDate = endOfWeek(selectedDate)

      setDate({
        from: startOfWeekDate,
        to: endOfWeekDate
      })

      // Call the onChange prop with the updated date range
      if (onChange) {
        onChange({
          from: startOfWeekDate,
          to: endOfWeekDate
        })
      }
    } catch (error) {
      console.error(
        "An error occurred while handling the date selection:",
        error
      )
      // You can add additional error handling or logging here
    }
  }

  return (
    <div className={cn("grid gap-2", className)}>
      <Popover>
        <PopoverTrigger asChild>
          <Button
            id="date"
            variant={"outline"}
            className={cn(
              "w-full justify-start text-left font-normal",
              !date && "text-muted-foreground"
            )}
          >
            <CalendarIcon className="mr-2 h-4 w-4" />
            {date?.from ? (
              date.to ? (
                <>
                  {format(date.from, "EEE, LLL dd, y")} -{" "}
                  {format(date.to, "EEE, LLL dd, y")}
                </>
              ) : (
                format(date.from, "EEE, LLL dd, y")
              )
            ) : (
              <span>Pick a date</span>
            )}
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-auto p-0" align="end">
          <Calendar
            initialFocus
            mode="single"
            defaultMonth={date?.from}
            selected={date}
            onSelect={handleSelect}
            numberOfMonths={2}
          />
        </PopoverContent>
      </Popover>
    </div>
  )
}

export default CalendarDateWeekPicker
