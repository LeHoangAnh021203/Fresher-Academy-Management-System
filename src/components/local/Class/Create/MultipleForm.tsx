import { useState } from "react"

import axios from "axios"
import { MoreHorizontal, PlusCircle, Search } from "lucide-react"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router"
import { toast } from "sonner"

import {
  IClass,
  IClassAttendee,
  IClassFSU,
  IClassLocation,
  IClassStatus
} from "@/types/class.interface"

import {
  calculateTotalDurationForSelectedProgram,
  generateClassCode
} from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import Chip from "@/components/global/atoms/chip"
import { Input } from "@/components/global/atoms/input"
import { Loader } from "@/components/global/atoms/loader"
import { SyllabusCard } from "@/components/global/molecules/SyllabusCard"
import Title from "@/components/global/organisms/Title"
import Attendee from "@/components/local/Class/Create/Attendee"
import General from "@/components/local/Class/Create/General"
import Header from "@/components/local/Class/Create/Header"
import TimeFrame from "@/components/local/Class/Create/TimeFrame"
import TrainingProgram from "@/components/local/Class/Create/TrainingProgram"

function MultiStepCreateClass() {
  const navigate = useNavigate()
  const [step, setStep] = useState(1)
  const totalSteps = 3
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [selectedProgram, setSelectedProgram] = useState(null)
  const currentDate = new Date()
  const isoFormattedDate = currentDate.toISOString()

  const [newClassData, setNewClassData] = useState({
    className: "",
    classCode: "",
    duration: 0,
    status: IClassStatus.Planning,
    locationId: IClassLocation.L001,
    fsuId: IClassFSU.F001,
    dates: [],
    createdBy: "",
    createdDate: isoFormattedDate,
    modifiedBy: "",
    modifiedDate: "",
    attendee: IClassAttendee.Fresher,
    trainingProgramCode: ""
  })

  const mapClassDataToCalendarData = (classData: IClass) => {
    const {
      classCode,
      locationId,
      startClassTime,
      attendee,
      trainingProgramCode,
      dates,
      admins,
      trainers
    } = classData
    const adminsLength = admins.length
    const trainersLength = trainers.length

    const newCalendarData = dates.map((date, index) => ({
      calendarId: date,
      date: date,
      admin: admins[index % adminsLength],
      trainer: trainers[index % trainersLength],
      classCode: classCode,
      locationId: locationId,
      time: startClassTime,
      attendee: attendee,
      trainingProgramCode: trainingProgramCode
    }))

    return newCalendarData
  }

  const {
    register,
    handleSubmit,
    trigger,
    watch,
    formState: { errors }
  } = useForm()

  const newClassName = watch("className")

  const onHandleSubmit = async (data) => {
    setIsSubmitting(true)

    try {
      const classResponse = await axios.post(
        "https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/classes",
        newClassData
      )
      console.log("Class submitted successfully", classResponse.data)

      const newCalendarData = mapClassDataToCalendarData(classResponse.data)
      console.log(
        `Add calendar of ${classResponse.data.classCode} successfully`
      )

      const calendarResponses = await Promise.all(
        newCalendarData.map((calendarEntry) =>
          axios.post(
            "https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/calendars",
            calendarEntry
          )
        )
      )

      // Log the response data of each calendar entry creation
      calendarResponses.forEach((response) => console.log(response.data))

      toast.success("Created new class successfully!")

      navigate("/classes")
    } catch (error) {
      console.error("Submission error", error)
      toast.error("Failed to create new class.")
    } finally {
      setIsSubmitting(false)
    }
  }

  // GENERAL
  const handleDataChange = (key: keyof IClass, value: IClass[keyof IClass]) => {
    setNewClassData((prevData) => ({ ...prevData, [key]: value }))
  }

  // ATTENDEE
  const handleAttendeeChange = (attendeeType: number) => {
    setNewClassData((prevData) => ({
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
    setNewClassData((prevData) => ({
      ...prevData,
      dates: selectedDatesStrings
    }))
  }

  // TRAINING PROGRAM
  const handleProgramChange = (program) => {
    // Assuming calculateTotalDurationForSelectedProgram returns { totalDays, totalLearningTime }
    const { totalLearningTime } =
      calculateTotalDurationForSelectedProgram(program)

    // Generating classCode based on program name and a random incremental number
    const classCode = generateClassCode(newClassData.fsuId, program.name)

    setNewClassData((prevData) => ({
      ...prevData,
      trainingProgramCode: program.trainingProgramCode,
      duration: totalLearningTime,
      classCode
    }))

    setSelectedProgram(program)
  }

  const [totalDays, setTotalDays] = useState(0)
  const [totalLearningTime, setTotalLearningTime] = useState(0)

  const handleProgramSelection = (days: number, hours: number) => {
    setTotalDays(days)
    setTotalLearningTime(hours)
  }

  const handleCancel = () => {
    navigate("/classes")
  }

  const nextStep = async () => {
    const isStepValid = await trigger()

    window.scrollTo({ top: 0 })

    if (isStepValid && step < totalSteps) {
      setStep(step + 1)
    }
  }

  const previousStep = () => {
    window.scrollTo({ top: 0 })

    if (step > 1) {
      setStep(step - 1)
    }
  }

  const isLastStep = step === totalSteps

  if (isSubmitting) {
    return <Loader />
  }

  return (
    <form onSubmit={handleSubmit(onHandleSubmit)} className="h-full w-full">
      <div className="flex h-full w-full flex-col">
        {step === 1 && (
          <div className="flex w-full flex-col justify-between">
            <Title title="New Class" />
            <div className="mx-5 flex min-h-[600px] items-center justify-center">
              <div className="flex items-center justify-center">
                <div>
                  <h1 className="text-2xl font-semibold tracking-[4px]">
                    Name of new Class
                  </h1>
                  <p className="text-sm text-slate-600">
                    What would you like to name your class? Don't worry, it can
                    be changed later.
                  </p>

                  <div className="mt-4">
                    <Input
                      placeholder="e.g: Fresher Develop Operation"
                      type="text"
                      {...register("className", {
                        required: true,
                        onChange: (e) =>
                          handleDataChange("className", e.target.value)
                      })}
                      className="rounded-[8px] border-2 px-4 py-[10px] text-xs italic"
                    />
                    {errors.className && (
                      <p className="ml-2 mt-2 text-sm text-red-500">
                        Class name is required.
                      </p>
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
        )}

        {step === 2 && (
          <div className="flex h-fit w-full flex-col gap-[30px]">
            <Header
              className={newClassName}
              totalDays={totalDays}
              totalLearningTime={totalLearningTime}
            />

            <div className="flex flex-col justify-between gap-5 px-5 sm:gap-[30px] md:flex-col lg:flex-row">
              <div className="flex flex-col gap-[30px]">
                <General handleDataChange={handleDataChange} />
                <Attendee onAttendeeChange={handleAttendeeChange} />
              </div>

              <div className="sm:w-full lg:w-2/3">
                <TimeFrame onSelectedDatesChange={handleSelectedDatesChange} />
              </div>
            </div>

            <div className="px-5">
              <TrainingProgram
                onProgramSelect={handleProgramChange}
                onTotalDurationChange={handleProgramSelection}
                register={register}
                errors={errors}
              />
            </div>
          </div>
        )}

        {step === 3 && selectedProgram && (
          <div className="flex h-fit w-full flex-col">
            <div className="flex flex-col gap-[10px] border-b-[1px] border-black p-5 text-primary">
              <h3 className="text-2xl font-semibold leading-9 tracking-[4.8px]">
                Training program of {newClassName}
              </h3>
              <div className="flex justify-between">
                <div className="flex items-center gap-5">
                  <h2 className="text-[32px] font-semibold leading-[48px] tracking-[6.4px]">
                    {selectedProgram?.name}
                  </h2>
                  <Chip content="Inactive" color="#B9B9B9" />
                </div>
                <MoreHorizontal size={40} className="cursor-pointer" />
              </div>
            </div>
            <div className="flex h-fit flex-col gap-[5px] border-b-[1px] border-black p-5 text-black">
              <span className="text-2xl font-semibold leading-9 tracking-[4.8px]">
                0 days
                <span className="text-base font-normal leading-6 -tracking-normal">
                  (0 hours)
                </span>
              </span>
              <span className="text-sm font-medium leading-[22px]">
                Modified on {selectedProgram?.modifiedOn} by{" "}
                {selectedProgram?.modifiedBy}
              </span>
            </div>
            <div className="px-5 pt-[10px]">
              <h4 className="mb-[10px] text-[16px] font-semibold">Content</h4>
              {selectedProgram?.syllabuses.map((syllabus) => (
                <div
                  key={syllabus.topicCode}
                  className="bg-white pb-5 pt-[10px] text-[#2D3748]"
                >
                  <div className="flex items-center justify-between rounded-[20px] bg-[#2D3748] shadow-custom">
                    <div className="flex w-1/5 items-center justify-center">
                      <img src="../../../../public/training-program.svg" />
                    </div>
                    <div className="w-4/5">
                      <SyllabusCard syllabus={syllabus} />
                    </div>
                  </div>
                </div>
              ))}
            </div>
            <div className="mb-20 mt-5 flex items-center gap-5 p-5">
              <Button variant={"primary"}>
                <PlusCircle size={20} /> Add Syllabus
              </Button>
              <span>or</span>
              <div className="relative">
                <Search size={16} className="absolute left-3 top-3" />
                <Input placeholder="select" className="w-[300px] pl-9 italic" />
              </div>
            </div>
          </div>
        )}

        {/* FOOTER */}
        <div className="flex w-full justify-between p-5">
          <Button
            onClick={previousStep}
            variant="primary"
            className="px-7"
            disabled={step === 1}
          >
            Back
          </Button>

          <div className="flex gap-2">
            <Button
              type="button"
              variant="link"
              className="px-7 font-bold text-[#E74A3B] underline hover:no-underline"
              onClick={handleCancel}
            >
              Cancel
            </Button>

            {/* <Button
              variant="draft"
              type="button"
              className="px-7"
              disabled={step === 1}
            >
              Save as draft
            </Button> */}

            {!isLastStep && (
              <Button
                onClick={nextStep}
                variant="primary"
                type="button"
                className="px-7"
                data-testid="save-btn"
                disabled={step === 2 && !selectedProgram}
              >
                Next
              </Button>
            )}

            {isLastStep && (
              <Button variant="primary" type="submit" className="px-7">
                {isSubmitting ? "Saving" : "Save"}
              </Button>
            )}
          </div>
        </div>
      </div>
    </form>
  )
}

export default MultiStepCreateClass
