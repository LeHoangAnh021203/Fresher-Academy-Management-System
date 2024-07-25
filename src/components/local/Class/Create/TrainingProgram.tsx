import { useEffect, useState } from "react"

import { Pencil, Search, X } from "lucide-react"

import { ITrainingProgram } from "@/types/training-program.interface"

import { useGetAllTrainingPrograms } from "@/apis/training-program-routes"

import {
  calculateTotalDurationForSelectedProgram,
  formatDateVN
} from "@/lib/utils"

import { useDebounce } from "@/hooks/useDebounce"

import { Input } from "@/components/global/atoms/input"
import { Skeleton } from "@/components/global/atoms/skeleton"
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger
} from "@/components/global/atoms/tabs"
import { SyllabusCard } from "@/components/global/molecules/SyllabusCard"

interface TrainingProgramProps {
  onProgramSelect: (program: ITrainingProgram) => void
  onTotalDurationChange: (days: number, hours: number) => void
}

function TrainingProgram({
  onProgramSelect,
  onTotalDurationChange,
  register,
  errors
}: TrainingProgramProps) {
  const [searchQuery, setSearchQuery] = useState("")
  const [debouncedSearchQuery, isDebouncing] = useDebounce(searchQuery, 300)

  const [selectedProgram, setSelectedProgram] =
    useState<ITrainingProgram | null>(null)

  const { data: trainingProgramData } = useGetAllTrainingPrograms()

  // console.log(trainingProgramData)

  const filteredPrograms = trainingProgramData?.filter((program) =>
    program.name.toLowerCase().includes(debouncedSearchQuery.toString())
  )

  // console.log("filteredPrograms", filteredPrograms)

  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value)
  }

  const handleSearchClear = () => {
    setSearchQuery("")
    setSelectedProgram(null)
  }

  const handleProgramSelect = (program: ITrainingProgram) => {
    setSearchQuery(program.name)
    setSelectedProgram(program)

    const { totalDays, totalLearningTime } =
      calculateTotalDurationForSelectedProgram(program)

    onProgramSelect(program)

    onTotalDurationChange(totalDays, totalLearningTime)
  }

  const changeProgramSelect = () => {
    setSelectedProgram(null)
    setSearchQuery("")
  }

  let totalDays = 0
  let totalLearningTime = 0
  if (selectedProgram) {
    const { totalDays: days, totalLearningTime: learningTime } =
      calculateTotalDurationForSelectedProgram(selectedProgram)
    totalDays = days
    totalLearningTime = learningTime
  }

  useEffect(() => {
    if (selectedProgram && typeof onTotalDurationChange === "function") {
      const { totalDays, totalLearningTime } =
        calculateTotalDurationForSelectedProgram(selectedProgram)
      onTotalDurationChange(totalDays, totalLearningTime)
    }
  }, [selectedProgram, onTotalDurationChange])

  return (
    <Tabs
      defaultValue="training-program"
      className="w-full transition-all duration-200"
    >
      <TabsList className="sm:h-9">
        <TabsTrigger value="training-program">Training program</TabsTrigger>
        <TabsTrigger value="attendee-list">Attendee list</TabsTrigger>
        <TabsTrigger value="budget">Budget</TabsTrigger>
        <TabsTrigger value="others">Others</TabsTrigger>
      </TabsList>
      <TabsContent value="training-program">
        {!selectedProgram && (
          <div className="flex gap-[15px] rounded-tr-[20px] bg-[#2D3748] px-[30px] py-5 text-white shadow-xl">
            <span className="mt-2">Training Program name</span>
            <div className="relative flex items-center text-primary">
              <Search size={16} className="absolute left-3 top-3" />

              <Input
                placeholder="Select program"
                type="text"
                {...register("programName", {
                  required: true
                })}
                value={searchQuery}
                onChange={handleSearchChange}
                className="w-[400px] pl-9 italic"
              />

              {errors.programName && (
                <p className="ml-4 text-sm text-red-500">
                  Program name is required.
                </p>
              )}

              {searchQuery && (
                <X
                  size={16}
                  className="absolute right-3 top-3 cursor-pointer"
                  onClick={handleSearchClear}
                  data-testid="clear"
                />
              )}

              {isDebouncing ? (
                <div className="search-suggestion absolute left-0 right-0 top-[40px] z-10 max-h-[210px] overflow-auto overflow-y-scroll rounded-[10px] shadow-md">
                  <div className="flex cursor-pointer duration-200 transition-all flex-col gap-[5px] bg-white px-4 py-[10px] hover:bg-[#DFDEDE]">
                    <Skeleton className="rounded-md h-6 w-36"></Skeleton>
                    <div className="flex gap-2 mt-1">
                      <Skeleton className="rounded-md h-5 w-28"></Skeleton>
                      <Skeleton className="rounded-md h-5 w-48"></Skeleton>
                    </div>
                  </div>
                </div>
              ) : (
                searchQuery && (
                  <div className="search-suggestion absolute left-0 right-0 top-[40px] z-10 max-h-[210px] overflow-auto overflow-y-scroll rounded-[10px] shadow-md">
                    {filteredPrograms.map((program) => (
                      <div
                        key={program.trainingProgramCode}
                        className="flex cursor-pointer duration-200 transition-all flex-col gap-[5px] bg-white px-4 py-[10px] hover:bg-[#DFDEDE]"
                        onClick={() => handleProgramSelect(program)}
                      >
                        <span className="font-bold">{program.name}</span>
                        <span className="text-sm">
                          {totalDays} days{" "}
                          <span className="italic">
                            ({totalLearningTime}hrs)
                          </span>{" "}
                          <span className="ml-2 italic">
                            {formatDateVN(program.modifyDate)} by{" "}
                          </span>
                          <span className="font-bold italic">
                            {program.modifyBy || "Unknown"}
                          </span>
                        </span>
                      </div>
                    ))}
                  </div>
                )
              )}
            </div>
          </div>
        )}

        {selectedProgram && (
          <>
            <div className="rounded-tr-[20px] bg-[#2D3748] px-[30px] pb-5 pt-[10px] text-white shadow-xl">
              <div className="mb-[10px] flex items-center gap-5">
                <h3 className="text-2xl font-semibold leading-9 tracking-[4.8px]">
                  {selectedProgram.name}
                </h3>
                <Pencil
                  size={16}
                  className="mb-[-2px] cursor-pointer"
                  onClick={changeProgramSelect}
                />
              </div>
              <span className="mr-[10px] border-r-2 pr-[10px] text-sm font-normal">
                {totalDays} days {totalLearningTime} hours
              </span>
              <span className="text-sm font-normal">
                Modified on {selectedProgram.modifiedOn} by{" "}
                {selectedProgram.modifiedBy}
              </span>
            </div>

            <div className="mt-5">
              {selectedProgram.syllabuses &&
                selectedProgram.syllabuses.map((syllabus) => (
                  <div
                    key={syllabus.topicCode}
                    className="bg-white pb-5 pt-[10px] text-[#2D3748]"
                  >
                    <div className="flex items-center justify-between rounded-[20px] bg-[#2D3748] shadow-custom">
                      <div className="flex w-1/5 items-center justify-center">
                        <img
                          src="../../../../public/training-program.svg"
                          className="cursor-pointer"
                        />
                      </div>
                      <div className="w-4/5">
                        <SyllabusCard syllabus={syllabus} />
                      </div>
                    </div>
                  </div>
                ))}
            </div>
          </>
        )}
      </TabsContent>
      <TabsContent value="attendee-list">
        <div className="flex h-20 gap-[15px] rounded-tr-[20px] bg-[#2D3748] px-[30px] py-5 text-white shadow-xl">
          <span className="mt-2">Attendee list</span>
          <div className="relative flex items-center text-primary"></div>
        </div>
      </TabsContent>
      <TabsContent value="budget">
        <div className="flex h-20 gap-[15px] rounded-tr-[20px] bg-[#2D3748] px-[30px] py-5 text-white shadow-xl">
          <span className="mt-2">Budget</span>
          <div className="relative flex items-center text-primary"></div>
        </div>
      </TabsContent>
      <TabsContent value="others">
        <div className="flex h-20 gap-[15px] rounded-tr-[20px] bg-[#2D3748] px-[30px] py-5 text-white shadow-xl">
          <span className="mt-2">Others</span>
          <div className="relative flex items-center text-primary"></div>
        </div>
      </TabsContent>
    </Tabs>
  )
}

export default TrainingProgram
