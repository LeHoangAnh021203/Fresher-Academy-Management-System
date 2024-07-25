import { useState } from "react"

import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import { toast } from "sonner"
import { z } from "zod"

import { ITrainingUnit } from "@/types/syllabus.interface"

import { useCreateSyllabus } from "@/apis/syllabus-routes"

import { SyllabusNewSchema } from "@/lib/schemas/syllabus"
import { calculateTimeAllocation } from "@/lib/utils"

import { Form } from "@/components/global/atoms/form"
import { Loader } from "@/components/global/atoms/loader"
import { NavigationBar } from "@/components/global/molecules/NavigationBar"
import ProgressBar from "@/components/global/molecules/ProgressBar"
import SyllabusTab from "@/components/global/molecules/SyllabusTab"
import TimeAllocationTable from "@/components/global/molecules/TimeAllocation"
import CreateSyllabusHeader from "@/components/local/Syllabus/CreateSyllabusHeader"
import GeneralTab from "@/components/local/Syllabus/GeneralTab"
import OtherTab from "@/components/local/Syllabus/OtherTab"
import OutlineTab from "@/components/local/Syllabus/OutlineTab"

const tabs = ["General", "Outline", "Others"]
const stateDiagram = ["disabled", "default", "hide"]

const CreateSyllabusPage = () => {
  const [tab, setTab] = useState(1)
  const [trainingUnits, setTrainingUnits] = useState<ITrainingUnit[]>([])
  const { isPending, mutateAsync } = useCreateSyllabus()
  const navigate = useNavigate()

  console.log("trainingUnits", trainingUnits)

  const timeAllocation = calculateTimeAllocation(trainingUnits)

  const handleGeneralForm = () => {
    return form.trigger([
      "topicCode",
      "topicName",
      "technicalRequirement",
      "courseObjective"
    ])
  }

  const handleOutlineForm = () => {
    if (trainingUnits.length === 0) {
      toast.error("Please add at least one training unit")
      return false
    }

    for (const unit of trainingUnits) {
      if (unit.trainingContents.length === 0) {
        toast.error(`Training unit ${unit.unitName} has no training contents`)
        return false
      }
    }

    return true
  }

  const next = async () => {
    let output

    if (tab === 1) {
      output = await handleGeneralForm()
    }

    if (tab === 2) {
      output = await handleOutlineForm()
    }

    if (!output) return
    if (tab < tabs.length) {
      setTab((step) => step + 1)
    }
  }

  const form = useForm<z.infer<typeof SyllabusNewSchema>>({
    resolver: zodResolver(SyllabusNewSchema),
    values: {
      topicCode: "",
      topicName: "",
      technicalRequirement: "",
      courseObjective: "",
      trainingPrinciple: "",
      assessment: {
        assessmentID: "ASSE1",
        quizCount: 0,
        quizPercent: 15,
        assignmentCount: 0,
        assignmentPercent: 15,
        finalTheoryPercent: 35,
        finalPracticePercent: 35
      }
    }
  })

  const onSubmit = async (values: z.infer<typeof SyllabusNewSchema>) => {
    try {
      //@ts-expect-error type error
      await mutateAsync({ ...values, trainingUnits })
      toast.success("Created syllabus successfully!")
      form.reset()
      navigate("/syllabus")
    } catch (error) {
      console.error("Error creating syllabus:", error)
      toast.error("Failed to create syllabus")
    }
  }

  if (isPending) {
    return <Loader />
  }

  return (
    <div className="min-h-[90vh] w-full overflow-hidden">
      <div className="flex w-full items-center gap-8 border-b-[1px] border-primary px-2 py-4">
        <p className="text-xl font-semibold text-primary">Syllabus</p>
        <div className="mt-6">
          <ProgressBar currentStep={tab} />
        </div>
      </div>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="w-full">
          <div className="w-full px-6 py-4">
            <CreateSyllabusHeader form={form} />
            <SyllabusTab tabs={tabs} currentTab={tab} />
            <div className="mb-4 flex w-full gap-4">
              <div className="flex-1 overflow-hidden rounded-b-md rounded-r-md border-t-[1px]  bg-white px-4 py-5 shadow-md">
                {tab === 1 && <GeneralTab form={form} />}
                {tab === 2 && (
                  <OutlineTab
                    trainingUnits={trainingUnits}
                    setTrainingUnits={setTrainingUnits}
                  />
                )}
                {tab === 3 && (
                  <OtherTab form={form} timeAllocation={timeAllocation} />
                )}
              </div>
              {tab !== 3 && (
                <TimeAllocationTable
                  title="Time allocation"
                  horizontal={false}
                  items={{
                    values: Object.values(timeAllocation),
                    labels: Object.keys(timeAllocation)
                  }}
                  state={stateDiagram[tab - 1]}
                />
              )}
            </div>
            <NavigationBar
              currentStep={tab}
              maxStep={3}
              prev={() => setTab((prev) => prev - 1)}
              next={next}
            />
          </div>
        </form>
      </Form>
    </div>
  )
}

export default CreateSyllabusPage
