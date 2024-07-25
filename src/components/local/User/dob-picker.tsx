import { useState } from "react"

import { format } from "date-fns"
import { Calendar as CalendarIcon } from "lucide-react"

import { cn } from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import { Calendar } from "@/components/global/atoms/calendar"
import {
  Popover,
  PopoverContent,
  PopoverTrigger
} from "@/components/global/atoms/popover"

interface DatePickerProps {
  initialDate?: Date
  onChange?: (date: Date) => void
}

export function DatePicker({ initialDate, onChange }: DatePickerProps) {
  const [date, setDate] = useState<Date>(initialDate || new Date())

  const handleSelect = (selectedDate: Date) => {
    if (!selectedDate) return
    setDate(selectedDate)
    if (onChange) {
      onChange(selectedDate)
    }
  }

  return (
    <div className="flex gap-2">
      <Popover>
        <PopoverTrigger asChild>
          <Button
            variant={"outline"}
            className={cn(
              "w-fit h-10 justify-start text-center font-normal border-black rounded-md",
              !date && "text-muted-foreground"
            )}
          >
            <CalendarIcon className="h-4 w-4" />
            {date ? format(date, "PPP") : <span>Pick a date</span>}
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-auto p-0" align="start">
          <Calendar
            classNames={{ caption_label: "hidden" }}
            mode="single"
            selected={date}
            captionLayout="dropdown-buttons"
            //@ts-expect-error date type error
            onSelect={handleSelect}
            defaultMonth={date}
            fromYear={1960}
            toYear={new Date().getFullYear() + 1} 
          />
        </PopoverContent>
      </Popover>
    </div>
  )
}
