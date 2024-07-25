import { useEffect, useState } from "react"

import {
  eachDayOfInterval,
  format,
  getDay,
  isBefore,
  startOfDay
} from "date-fns"
import { addMonths } from "date-fns"
import {
  CalendarIcon,
  ChevronDownCircle,
  ChevronLeftCircle,
  X
} from "lucide-react"

import { Button } from "@/components/global/atoms/button"
import { Input } from "@/components/global/atoms/input"
import { Calendar } from "@/components/local/Class/Create/Calendar"

interface TimeFrameProps {
  onSelectedDatesChange: (dates: Date[]) => void
}

function TimeFrame({ onSelectedDatesChange }: TimeFrameProps) {
  const [isTimeFrameVisible, setIsTimeFrameVisible] = useState(true)
  const today = startOfDay(new Date())
  const [currentMonth, setCurrentMonth] = useState(new Date())
  const [nextMonth, setNextMonth] = useState(addMonths(new Date(), 1))

  const toggleTimeFrameVisibility = () => {
    setIsTimeFrameVisible(!isTimeFrameVisible)
  }

  const [startDate, setStartDate] = useState<Date | null>(null)
  const [endDate, setEndDate] = useState<Date | null>(null)

  const [selectedDates, setSelectedDates] = useState<Date[]>([])

  const isWeekdayEven = (date: Date) => {
    const day = getDay(date)
    return day === 1 || day === 3 || day === 5
  }

  const findMatchingWeekdays = (
    startDate: Date,
    endDate: Date,
    isEven: boolean
  ) => {
    const dates = eachDayOfInterval({ start: startDate, end: endDate })
    return dates.filter((date) => isWeekdayEven(date) === isEven)
  }

  const handleStartDateSelect = (date: Date) => {
    if (!isBefore(date, today) && getDay(date) !== 0) {
      setStartDate(date)
      if (endDate && getDay(date) !== 0) {
        const evenWeekday = isWeekdayEven(date)
        const matchingWeekdays = findMatchingWeekdays(
          date,
          endDate,
          evenWeekday
        )
        setSelectedDates(matchingWeekdays.filter((date) => getDay(date) !== 0))
      } else {
        setSelectedDates([date].filter((date) => getDay(date) !== 0))
      }
    }
  }

  const handleEndDateSelect = (date: Date) => {
    if (!isBefore(date, today) && getDay(date) !== 0) {
      setEndDate(date)
      if (startDate && getDay(date) !== 0) {
        const evenWeekday = isWeekdayEven(startDate)
        const matchingWeekdays = findMatchingWeekdays(
          startDate,
          date,
          evenWeekday
        )
        setSelectedDates(matchingWeekdays.filter((date) => getDay(date) !== 0)) // Filter out Sundays
      } else {
        setSelectedDates([date].filter((date) => getDay(date) !== 0)) // Filter out Sundays
      }
    }
  }

  useEffect(() => {
    if (selectedDates.length > 0) {
      const sortedDates = selectedDates.sort(
        (a, b) => a.getTime() - b.getTime()
      )
      const minDate = sortedDates[0]
      const maxDate = sortedDates[sortedDates.length - 1]
      setStartDate(minDate)
      setEndDate(maxDate)
    }

    // console.log(selectedDates)
  }, [selectedDates])

  useEffect(() => {
    onSelectedDatesChange(selectedDates)
  }, [selectedDates])

  const handleCurrentMonthChange = (newMonth: Date) => {
    setCurrentMonth(newMonth)
  }

  const handleNextMonthChange = (newMonth: Date) => {
    setNextMonth(newMonth)
  }

  const clearAllDates = () => {
    setStartDate(null)
    setEndDate(null)
    setSelectedDates([])
  }

  return (
    <div className="h-fit overflow-auto rounded-[10px] shadow-xl">
      <div
        className={`flex min-h-[52px] cursor-pointer select-none items-center justify-between px-4 py-2 text-white transition-all duration-200 ${
          isTimeFrameVisible ? "bg-primary" : "bg-[#8B8B8B]"
        }`}
      >
        <div className="flex items-center justify-between gap-[10px]">
          <CalendarIcon />
          <span className="mr-2">Time frame</span>

          <div className="relative flex items-center">
            <Input
              value={startDate ? format(startDate, "dd/MM/yyyy") : ""}
              readOnly
              className="h-8 w-[140px] rounded-[5px] px-4 py-[5px] text-black"
              placeholder="--/--/----"
            />
            <CalendarIcon
              size={20}
              className="absolute right-2 cursor-pointer text-primary"
            />
          </div>
          <span className="text-[#DFDEDE]">to</span>
          <div className="relative flex items-center">
            <Input
              value={endDate ? format(endDate, "dd/MM/yyyy") : ""}
              readOnly
              className="h-8 w-[140px] rounded-[5px] px-4 py-[5px] text-black"
              placeholder="--/--/----"
            />
            <CalendarIcon
              size={20}
              className="absolute right-2 cursor-pointer text-primary"
            />
          </div>
          <Button
            type="button"
            className="ml-3 h-8 rounded-[5px] bg-white px-3 text-primary hover:bg-white/90"
            onClick={clearAllDates}
          >
            <X size={16} />
          </Button>
        </div>
        <div
          className="p-1"
          onClick={toggleTimeFrameVisibility}
          data-testid="toggle-visibility"
        >
          {isTimeFrameVisible ? <ChevronDownCircle /> : <ChevronLeftCircle />}
        </div>
      </div>
      {isTimeFrameVisible && (
        <div className="flex w-full flex-col justify-between gap-[30px] p-[10px] md:flex-row md:gap-0">
          <Calendar
            className="flex justify-center lg:p-2"
            onDayClick={handleStartDateSelect}
            selected={selectedDates}
            month={currentMonth}
            onMonthChange={handleCurrentMonthChange}
          />
          <Calendar
            className="flex justify-center lg:p-2"
            onDayClick={handleEndDateSelect}
            selected={selectedDates}
            month={nextMonth}
            onMonthChange={handleNextMonthChange}
          />
        </div>
      )}
    </div>
  )
}

export default TimeFrame
