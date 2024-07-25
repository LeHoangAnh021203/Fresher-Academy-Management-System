import { useEffect, useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { zodResolver } from "@hookform/resolvers/zod"
import { format } from "date-fns"
import { useForm } from "react-hook-form"
import { useParams } from "react-router-dom"
import { toast } from "sonner"
import { z } from "zod"

import { ISyllabus, ISyllabusPreview } from "@/types/syllabus.interface"
import { ITrainingProgramStatus } from "@/types/training-program.interface"

import { useGetAllSyllabus } from "@/apis/syllabus-routes"
import {
  useAddSyllabusToTrainingProgram,
  useDeleteSyllabusFromTrainingProgram,
  useGetTrainingProgramById
} from "@/apis/training-program-routes"

import { TrainingProgramSchema } from "@/lib/schemas/training-program"
import {
  calculateTotalDurationOfTrainingProgram,
  convertMinutesToHoursAndMinutes
} from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList
} from "@/components/global/atoms/command"
import { Loader } from "@/components/global/atoms/loader"
import { SyllabusCard } from "@/components/global/molecules/SyllabusCard"
import { ProgramHeader } from "@/components/local/Training-Program/program-header"
import { ProgramInfo } from "@/components/local/Training-Program/program-info"
import { ProgramSubHeader } from "@/components/local/Training-Program/program-subheader"

const TrainingProgramDetail = () => {
  const { id } = useParams()

  const { data, isLoading } = useGetTrainingProgramById(id!)
  const { mutateAsync: mutateDeleteSyllabus } =
    useDeleteSyllabusFromTrainingProgram(id!)
  const { mutateAsync: mutateAddSyllabus } = useAddSyllabusToTrainingProgram(
    id!
  )
  const { setIsEdit: setSyllabusEdit } = useSyllabusDetailContext()
  const { data: allSyllabuses } = useGetAllSyllabus()

  const [isEdit, setIsEdit] = useState<boolean>(false)
  const [trainingProgramSyllabuses, setTrainingProgramSyllabuses] = useState<
    ISyllabus[]
  >([])
  const [filteredSyllabuses, setFilteredSyllabuses] = useState<
    ISyllabusPreview[]
  >([])

  setSyllabusEdit(false)

  const form = useForm<z.infer<typeof TrainingProgramSchema>>({
    resolver: zodResolver(TrainingProgramSchema),
    values: {
      trainingProgramCode: data?.trainingProgramCode || "",
      name: data?.name || "",
      status: data?.status as ITrainingProgramStatus,
      duration: data?.duration || 0,
      modifyBy: data?.modifyBy || "unknown",
      modifyDate: data?.modifyDate || "",
      syllabuses: data?.syllabuses || []
    }
  })

  useEffect(() => {
    const filterSyllabuses = allSyllabuses?.filter(
      (syllabus) =>
        !trainingProgramSyllabuses.some(
          (trainingProgramSyllabus) =>
            trainingProgramSyllabus.topicCode === syllabus.topicCode
        )
    )
    if (data) {
      setTrainingProgramSyllabuses(data.syllabuses)
      setFilteredSyllabuses(filterSyllabuses || [])
    }
  }, [data, allSyllabuses, trainingProgramSyllabuses])

  const { days, learningTime } = calculateTotalDurationOfTrainingProgram(
    data?.syllabuses || []
  )

  const handleAddSyllabus = async (topicCode: string) => {
    try {
      await mutateAddSyllabus(topicCode)
      toast.success("Add new syllabus to training program successfully!")
    } catch (error) {
      toast.error("Failed to add syllabus to training program!")
    }
  }

  const handleDeleteSyllabus = async (topicCode: string) => {
    try {
      await mutateDeleteSyllabus(topicCode)
      toast.success("Delete syllabus from training program successfully!")
    } catch (error) {
      toast.error("Failed to delete syllabus from training program!")
    }
  }

  if (isLoading) {
    return <Loader />
  }

  return (
    <section className="w-full">
      <div className="flex h-full min-h-screen w-full flex-col">
        <ProgramHeader
          title={form.watch("name")}
          status={form.watch("status")}
          onEdit={() => setIsEdit(true)}
          mode="edit"
        />
        <ProgramSubHeader
          modifiedBy={form.watch("modifyBy")}
          modifiedOn={form.getValues("modifyDate")}
          hours={learningTime}
          days={days}
        />

        <div className="px-5 ">
          <ProgramInfo
            id={form.watch("trainingProgramCode")}
            name={form.watch("name")}
          />
        </div>
        <div className="p-5">
          <h1 className="text-2xl font-bold">Content</h1>

          <div className="mt-2 space-y-4 ">
            {data?.syllabuses.map((syllabus) => (
              <SyllabusCard
                syllabus={syllabus}
                isEdit={isEdit}
                onDelete={() => handleDeleteSyllabus(syllabus.topicCode)}
              />
            ))}
          </div>

          {isEdit && (
            <div className="mt-5">
              <h2 className="text-base font-semibold leading-7 text-gray-900">
                Select syllabus
              </h2>

              <Command className="mt-2 w-[50%] min-w-[500px] rounded-lg border shadow-md">
                <CommandInput placeholder="Enter syllabus you want to search" />

                <CommandList>
                  <CommandEmpty>No results found.</CommandEmpty>
                  <CommandGroup heading="Syllabus">
                    {filteredSyllabuses?.map((syllabus) => {
                      const { hours, minutes } =
                        convertMinutesToHoursAndMinutes(syllabus.duration)
                      const modifiedDate =
                        syllabus.modifiedDate === "0001-01-01T00:00:00"
                          ? syllabus.createdDate
                          : syllabus.modifiedDate
                      const modifiedBy =
                        syllabus.modifiedBy ?? syllabus.createdBy
                      return (
                        <CommandItem
                          key={syllabus.topicCode}
                          className="cursor-pointer"
                          onSelect={() => handleAddSyllabus(syllabus.topicCode)}
                        >
                          <div className="flex w-full flex-col">
                            <p className="truncate font-medium">
                              {syllabus.topicName}
                            </p>
                            <div className="inline-flex items-center justify-between text-muted-foreground">
                              <div>
                                {hours}h {minutes}m
                              </div>
                              <div>
                                Modified on{" "}
                                {format(new Date(modifiedDate), "dd/MM/yyyy")}{" "}
                                by {modifiedBy}
                              </div>
                            </div>
                          </div>
                        </CommandItem>
                      )
                    })}
                  </CommandGroup>
                </CommandList>
              </Command>
            </div>
          )}
          {isEdit && (
            <div className="mt-4 flex items-center justify-end space-x-2">
              <Button variant="ghost" onClick={() => setIsEdit(!isEdit)}>
                Cancel
              </Button>
              <Button>Save</Button>
            </div>
          )}
        </div>
      </div>
    </section>
  )
}

export default TrainingProgramDetail
