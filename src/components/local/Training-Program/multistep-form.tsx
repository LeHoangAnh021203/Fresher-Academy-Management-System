import { useEffect, useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { zodResolver } from "@hookform/resolvers/zod"
import { format } from "date-fns"
import { motion } from "framer-motion"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import { toast } from "sonner"
import { z } from "zod"

import { ISyllabusPreview } from "@/types/syllabus.interface"
import { ITrainingProgramStatus } from "@/types/training-program.interface"

import { useGetAllSyllabus } from "@/apis/syllabus-routes"
import { useAddNewTrainingProgram } from "@/apis/training-program-routes"

import { TrainingProgramNewSchema } from "@/lib/schemas/training-program"
import { convertMinutesToHoursAndMinutes } from "@/lib/utils"

import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList
} from "@/components/global/atoms/command"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import { Input } from "@/components/global/atoms/input"
import { Loader } from "@/components/global/atoms/loader"
import { Skeleton } from "@/components/global/atoms/skeleton"
import { SyllabusCardPreview } from "@/components/global/molecules/SyllabusCardPreview"
import Title from "@/components/global/organisms/Title"

import { NavigationBar } from "./navigation-bar"
import { ProgramHeader } from "./program-header"

const steps = [
  {
    id: "Step 1",
    name: "Program name",
    fields: ["name"]
  },
  {
    id: "Step 2",
    name: "Add syllabus",
    fields: ["syllabus"]
  }
]

export default function MultiStepForm() {
  const { data, isLoading } = useGetAllSyllabus()
  const { mutateAsync } = useAddNewTrainingProgram()
  const [previousStep, setPreviousStep] = useState(0)
  const [currentStep, setCurrentStep] = useState(0)
  const [syllabuses, setSyllabuses] = useState<ISyllabusPreview[]>([])
  const [selectedSyllabuses, setSelectedSyllabuses] = useState<
    ISyllabusPreview[]
  >([])
  const { setIsEdit } = useSyllabusDetailContext()
  const navigate = useNavigate()

  const delta = currentStep - previousStep

  setIsEdit(false)

  useEffect(() => {
    if (data && !isLoading) {
      setSyllabuses(data)
    }
  }, [data, isLoading])

  const calculateTotalDuration = (syllabuses: ISyllabusPreview[]) => {
    let totalDuration = 0
    for (const syllabus of syllabuses) {
      totalDuration += syllabus.duration
    }
    return totalDuration
  }

  const handleSyllabusSelect = (syllabus: ISyllabusPreview) => {
    setSelectedSyllabuses((prev) => [...prev, syllabus])
    setSyllabuses((prev) =>
      prev.filter((s) => s.topicCode !== syllabus.topicCode)
    )
  }

  const handleSyllabusDeselect = (syllabus: ISyllabusPreview) => {
    setSelectedSyllabuses((prev) =>
      prev.filter((s) => s.topicCode !== syllabus.topicCode)
    )
    setSyllabuses((prev) => [...prev, syllabus])
  }

  const form = useForm<z.infer<typeof TrainingProgramNewSchema>>({
    resolver: zodResolver(TrainingProgramNewSchema),
    defaultValues: {
      name: ""
    }
  })

  const onSubmit = async (values: z.infer<typeof TrainingProgramNewSchema>) => {
    try {
      const data: { name: string; syllabuses: string[]; duration: number } = {
        name: values.name,
        syllabuses: selectedSyllabuses.map((syllabus) => syllabus.topicCode),
        duration: calculateTotalDuration(selectedSyllabuses)
      }
      await mutateAsync(data)
      toast.success("Training program created successfully")
      navigate("/training-programs")
    } catch (error) {
      toast.error("Failed to create new training program")
    }
  }

  const next = async () => {
    const field = steps[currentStep].fields
    // @ts-expect-error field
    const output = await form.trigger(field)

    if (!output) return

    if (currentStep < steps.length - 1) {
      setPreviousStep(currentStep)
      setCurrentStep((step) => step + 1)
    }
  }

  const prev = () => {
    if (currentStep > 0) {
      setPreviousStep(currentStep)
      setCurrentStep((step) => step - 1)
    }
  }

  if (isLoading) {
    return <Loader />
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <section className="flex h-full min-h-screen w-full flex-col ">
          {/* Form */}
          {currentStep === 0 && (
            <motion.div
              initial={{ x: delta >= 0 ? "50%" : "-50%", opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.3, ease: "easeInOut" }}
              className="flex h-full min-h-screen w-full flex-col justify-between"
            >
              <Title title="New Training Program" />

              <div className="flex items-center justify-center">
                <div>
                  <h1 className="text-2xl font-semibold tracking-[4px]">
                    Name of new Training Program
                  </h1>
                  <p className="text-sm text-slate-600">
                    What would you like to name your training program?
                    Don&apos;t worry, it can be changed later
                  </p>

                  <div className="mt-4">
                    <FormField
                      control={form.control}
                      name="name"
                      render={({ field }) => (
                        <FormItem>
                          <FormControl>
                            <Input
                              placeholder="e.g: DevOps Foundation"
                              type="text"
                              {...field}
                            />
                          </FormControl>

                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                </div>
              </div>
              <NavigationBar
                currentStep={currentStep}
                steps={steps}
                next={next}
                prev={prev}
              />
            </motion.div>
          )}

          {currentStep === 1 && (
            <motion.div
              initial={{ x: delta >= 0 ? "50%" : "-50%", opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.3, ease: "easeInOut" }}
              className="flex h-full min-h-screen w-full flex-col justify-between"
            >
              <div>
                {/* Header */}
                <ProgramHeader
                  title={form.getValues("name")}
                  status={ITrainingProgramStatus.Inactive}
                  mode="create"
                />
                {/* Sub header */}

                <div className="p-5">
                  <h1 className="text-xl font-bold">Content</h1>
                  <div className="max-h-[500px] space-y-4 overflow-auto py-2">
                    {selectedSyllabuses.map((syllabus) => (
                      <SyllabusCardPreview
                        syllabus={syllabus}
                        onDelete={handleSyllabusDeselect}
                      />
                    ))}
                  </div>
                  <div className="mt-5">
                    <h2 className="text-base font-semibold leading-7 text-gray-900">
                      Select syllabus
                    </h2>

                    <Command className="mt-2 w-[50%] min-w-[500px] rounded-lg border shadow-md">
                      <CommandInput placeholder="Enter syllabus you want to search" />

                      <CommandList>
                        <CommandEmpty>No results found.</CommandEmpty>
                        <CommandGroup heading="Syllabus">
                          {isLoading && (
                            <>
                              {Array.from({ length: 5 }).map((_, index) => (
                                <div
                                  className="flex w-full flex-col space-y-2 border-b p-2"
                                  key={index}
                                >
                                  <Skeleton className="h-4 w-44" />
                                  <div className="flex w-full items-center justify-between">
                                    <Skeleton className="h-3 w-10" />
                                    <Skeleton className="h-3 w-28" />
                                  </div>
                                </div>
                              ))}
                            </>
                          )}

                          {syllabuses?.map((syllabus) => {
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
                                onSelect={() => handleSyllabusSelect(syllabus)}
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
                                      {format(
                                        new Date(modifiedDate),
                                        "dd/MM/yyyy"
                                      )}{" "}
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
                </div>
              </div>

              <NavigationBar
                currentStep={currentStep}
                steps={steps}
                next={next}
                prev={prev}
              />
            </motion.div>
          )}
        </section>
      </form>
    </Form>
  )
}
