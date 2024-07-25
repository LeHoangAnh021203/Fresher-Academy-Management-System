import { useEffect, useState } from "react"

import { getClassByClassId } from "@/graphql/queries/GetClass"
import { useQuery } from "@tanstack/react-query"
import axios from "axios"
import { useParams } from "react-router-dom"

import { IClass } from "@/types/class.interface"

import {
  calculateTotalDurationForSelectedProgram,
  generateClassCode
} from "@/lib/utils"

import { Loader } from "@/components/global/atoms/loader"
import Attendee from "@/components/local/Class/Detail/Attendee"
import Footer from "@/components/local/Class/Detail/Footer"
import General from "@/components/local/Class/Detail/General"
import Header from "@/components/local/Class/Detail/Header"
import TimeFrame from "@/components/local/Class/Detail/TimeFrame"
import TrainingProgram from "@/components/local/Class/Detail/TrainingProgram"

function ClassDetailPage() {
  const { id } = useParams()
  const [editing, setEditing] = useState(false)
  const [totalDays, setTotalDays] = useState(0)
  const [totalLearningTime, setTotalLearningTime] = useState(0)

  // const { data, isLoading } = useQuery<IClass>({
  //   queryKey: ["class", id],
  //   queryFn: () => getClassByClassId(id)
  // })

  // useEffect(() => {
  //   if (data && !isLoading) {
  //     setClassData(data.class)
  //   }
  // }, [data, isLoading])

  // if (isLoading || !classData) {
  //   return <Loader />
  // }

  // console.log(classData)

  const [classData, setClassData] = useState<IClass[]>([])
  const [updatedClassData, setUpdatedClassData] = useState<IClass[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const fetchClassData = async () => {
      setIsLoading(true)
      try {
        const response = await axios.get(
          `https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/classes/${id}`
        )
        setClassData(response.data)
      } catch (error) {
        console.error("Error fetching data:", error)
      } finally {
        setIsLoading(false)
      }
    }

    fetchClassData()
  }, [])

  console.log(classData)

  if (isLoading) {
    return <Loader />
  }

  const handleTotalDurationChange = (days: number, hours: number) => {
    setTotalDays(days)
    setTotalLearningTime(hours)
  }

  // GENERAL
  const handleDataChange = (fieldName, value) => {
    setClassData((prevState) => ({
      ...prevState,
      [fieldName]: value
    }))
  }

  // ATTENDEE
  const handleAttendeeChange = (attendeeType) => {
    setClassData((prevData) => ({
      ...prevData,
      attendee: attendeeType
    }))
  }

  // TIME FRAME
  const handleSelectedDatesChange = (dates: Date[]) => {
    const selectedDatesStrings = dates.map((date) => {
      const updatedDate = new Date(date.getTime() + 7 * 60 * 60 * 1000)
      return updatedDate.toISOString()
    })
    setClassData((prevData) => ({
      ...prevData,
      dates: selectedDatesStrings
    }))
  }

  // TRAINING PROGRAM
  const handleProgramChange = (program) => {
    const { totalDays, totalLearningTime } =
      calculateTotalDurationForSelectedProgram(program)
    const classCode = generateClassCode(classData.fsuId, program.name)
    setClassData((prevData) => ({
      ...prevData,
      trainingProgramCode: program.trainingProgramCode,
      duration: totalLearningTime,
      classCode
    }))
  }

  const handleEdit = () => setEditing(true)
  const handleCancel = () => setEditing(false)
  const handleSave = async () => {
    setIsLoading(true)
    try {
      const response = await axios.put(
        `https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/classes/${id}`,
        classData
      )
      console.log("Update class successful", response.data)
    } catch (error) {
      console.error("Error saving class data:", error)
    } finally {
      setIsLoading(false)
      setEditing(false)
    }
  }

  return (
    <div className="mb-[30px] flex h-fit w-full flex-col gap-[30px]">
      {/* HEADER */}
      <Header
        className={classData.className}
        classCode={classData.classCode}
        status={classData.status}
        days={totalDays}
        hours={totalLearningTime}
        onEdit={handleEdit}
        isEditing={editing}
      />

      {/* BODY */}
      <div className="flex flex-col justify-between gap-5 px-5 sm:gap-[30px] md:flex-col lg:flex-row">
        <div className="flex flex-col gap-[30px]">
          {/* GENERAL */}
          <General
            startClassTime={classData.startClassTime}
            endClassTime={classData.endClassTime}
            location={classData.locationId}
            trainers={classData.trainers}
            admins={classData.admins}
            fsu={classData.fsuId}
            email={classData.email}
            createdBy={classData.createdBy}
            createdOn={classData.createdDate}
            handleDataChange={handleDataChange}
            isEditing={editing}
          />

          {/* ATTENDEE  */}
          <Attendee
            attendee={classData.attendee}
            isEditing={editing}
            onAttendeeChange={handleAttendeeChange}
          />
        </div>
        <div className="sm:w-full lg:w-2/3">
          {/* TIME FRAME */}
          <TimeFrame
            dates={classData.dates}
            isEditing={editing}
            onSelectedDatesChange={handleSelectedDatesChange}
          />
        </div>
      </div>

      {/* TRAINING PROGRAM */}
      <TrainingProgram
        trainingProgramCode={classData.trainingProgramCode}
        onProgramSelect={handleProgramChange}
        onTotalDurationChange={handleTotalDurationChange}
        isEditing={editing}
      />

      {editing && <Footer onCancel={handleCancel} onSave={handleSave} />}
    </div>
  )
}

export default ClassDetailPage
