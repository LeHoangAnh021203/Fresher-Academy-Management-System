import { useEffect, useState } from "react"

import axios from "axios"
import { endOfWeek, format, parseISO, startOfWeek } from "date-fns"
import { DateRange } from "react-day-picker"

import { ICalendar } from "@/types/calendar.interface"

import { DatePicker } from "@/components/global/atoms/date-picker"
import { Loader } from "@/components/global/atoms/loader"
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger
} from "@/components/global/atoms/tabs"
import Title from "@/components/global/organisms/Title"
import CalendarDateWeekPicker from "@/components/local/Training-Calendar/CalendarDateWeekPicker"
import CalendarWeekly from "@/components/local/Training-Calendar/CalendarWeekly"

import CalendarDaily from "../../components/local/Training-Calendar/CalendarDaily"

function TrainingCalendarPage() {
  const [isLoading, setIsLoading] = useState(true)
  const [calendarData, setCalendarData] = useState<ICalendar[]>([])
  const [date, setDate] = useState(format(new Date(), "yyyy-MM-dd"))
  const [week, setWeek] = useState({
    from: startOfWeek(new Date()),
    to: endOfWeek(new Date())
  })

  const handleWeekChange = (selectedDate: DateRange) => {
    setWeek(selectedDate)
  }

  useEffect(() => {
    const fetchCalendarData = async () => {
      setIsLoading(true)
      try {
        const calendarResponse = await axios.get(
          "https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/calendars"
        )
        const formattedAndSortedData = calendarResponse.data
          .map((item) => ({
            ...item,
            date: format(parseISO(item.date), "yyyy-MM-dd")
          }))
          .sort(
            (a, b) => new Date(a.date).getTime() - new Date(b.date).getTime()
          )

        setCalendarData(formattedAndSortedData)
      } catch (error) {
        console.error("Error fetching data:", error)
      } finally {
        setIsLoading(false)
      }
    }
    fetchCalendarData()
  }, [])

  console.log(calendarData)

  if (isLoading) {
    return <Loader />
  }

  return (
    <section className="min-h-[90vh] mb-[30px] w-full">
      <Title title="Training Calendar" />
      <div className="p-5">
        <Tabs defaultValue="day" className="w-full">
          <div className="flex items-center justify-between">
            <TabsList className="gap-4">
              <TabsTrigger
                value="day"
                className="transition-all duration-300 hover:bg-primary/90 w-32 rounded-full py-[6px]"
              >
                Day
              </TabsTrigger>
              <TabsTrigger
                value="week"
                className="transition-all duration-300 hover:bg-primary/90 w-32 rounded-full py-[6px]"
              >
                Week
              </TabsTrigger>
            </TabsList>
            <TabsContent value="day">
              <DatePicker initialDate={date} onChange={setDate} />
            </TabsContent>
            <TabsContent value="week">
              <CalendarDateWeekPicker
                initialDate={week}
                onChange={handleWeekChange}
              />
            </TabsContent>
          </div>
          <TabsContent value="day">
            <div className="mt-4 w-full space-y-4">
              <CalendarDaily
                timeOfDay="Morning"
                date={date}
                calendarData={calendarData}
              />
              <CalendarDaily
                timeOfDay="Noon"
                date={date}
                calendarData={calendarData}
              />
              <CalendarDaily
                timeOfDay="Night"
                date={date}
                calendarData={calendarData}
              />
            </div>
          </TabsContent>
          <TabsContent value="week">
            <div className="mt-4 w-full space-y-4">
              <CalendarWeekly
                timeOfDay="Morning"
                date={week}
                calendarData={calendarData}
              />
              <CalendarWeekly
                timeOfDay="Noon"
                date={week}
                calendarData={calendarData}
              />
              <CalendarWeekly
                timeOfDay="Night"
                date={week}
                calendarData={calendarData}
              />
            </div>
          </TabsContent>
        </Tabs>
      </div>
    </section>
  )
}

export default TrainingCalendarPage
