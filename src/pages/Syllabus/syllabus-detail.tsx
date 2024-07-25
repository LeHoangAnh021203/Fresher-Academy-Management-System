import { useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import NotFound from "@/pages/Not-Found/not-found"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { useParams } from "react-router-dom"
import { toast } from "sonner"
import * as z from "zod"

import { ITrainingUnit } from "@/types/syllabus.interface"

import { useEditAssignment } from "@/apis/assignment-routes"
import { useGetSyllabusById, useUpdateSyllabus } from "@/apis/syllabus-routes"

import { SyllabusSchema } from "@/lib/schemas/syllabus"
import {
  calculateTimeAllocation,
  calculateTotalDurationOfSyllabus,
  cn
} from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import { Form } from "@/components/global/atoms/form"
import { Loader } from "@/components/global/atoms/loader"
import SyllabusTab from "@/components/global/molecules/SyllabusTab"
import TimeAllocationTable from "@/components/global/molecules/TimeAllocation"
import { GeneralPreviewTab } from "@/components/local/Syllabus/GeneralPreviewTab"
import { OthersPreviewTab } from "@/components/local/Syllabus/OthersPreviewTab"
import { OutlinePreviewTab } from "@/components/local/Syllabus/OutlinePreviewTab"
import { SyllabusHeader } from "@/components/local/Syllabus/SyllabusHeader"
import { ProgramSubHeader } from "@/components/local/Training-Program/program-subheader"

const tabs = ["General", "Outline", "Others"]
const stateDiagram = ["disabled", "default", "hide"]

const SyllabusDetail = () => {
  const { id } = useParams()
  const [tab, setTab] = useState(1)
  const { isEdit, setIsEdit } = useSyllabusDetailContext()

  const { mutateAsync: mutateUpdateSyllabus } = useUpdateSyllabus(id!)
  const { mutateAsync: mutateEditAssignment } = useEditAssignment(id!)
  const { isLoading, isError, data } = useGetSyllabusById(id!)

  const timeAllocation = calculateTimeAllocation(data?.trainingUnits || [])

  const form = useForm<z.infer<typeof SyllabusSchema>>({
    resolver: zodResolver(SyllabusSchema),
    values: {
      topicCode: data?.topicCode || "",
      topicName: data?.topicName || "",
      version: data?.version || 1,
      technicalGroup: data?.technicalGroup || "",
      topicOutline: data?.topicOutline || "",
      trainingAudience: data?.trainingAudience || "",
      trainingMaterials: data?.trainingMaterials || "",
      createdBy: data?.createdBy || "unknown",
      createdDate: data?.createdDate || "",
      modifiedBy: data?.modifiedBy || "unknown",
      modifiedDate: data?.modifiedDate || "",
      pulishStatus: data?.pulishStatus || 0,
      technicalRequirement: data?.technicalRequirement || "",
      courseObjective: data?.courseObjective || "",
      trainingPrinciple: data?.trainingPrinciple || "",
      assessment: {
        assessmentID: data?.assessment?.assessmentID || "",
        quizCount: data?.assessment?.quizCount || 0,
        quizPercent: data?.assessment?.quizPercent || 0,
        assignmentCount: data?.assessment?.assignmentCount || 0,
        assignmentPercent: data?.assessment?.assignmentPercent || 0,
        finalTheoryPercent: data?.assessment?.finalTheoryPercent || 0,
        finalPracticePercent: data?.assessment?.finalPracticePercent || 0
      },
      trainingUnits: data?.trainingUnits || []
    }
  })

  if (isLoading) {
    return <Loader />
  }

  if (isError) {
    return <NotFound />
  }

  const { learningTime, days } = calculateTotalDurationOfSyllabus(data!)

  const onSubmit = async (values: z.infer<typeof SyllabusSchema>) => {
    const {
      topicCode,
      topicName,
      technicalGroup,
      technicalRequirement,
      courseObjective,
      trainingAudience,
      topicOutline,
      trainingMaterials,
      trainingPrinciple
    } = values

    const formData = {
      topicCode,
      topicName,
      technicalGroup,
      technicalRequirement,
      courseObjective,
      trainingAudience,
      topicOutline,
      trainingMaterials,
      trainingPrinciple
    }

    try {
      await mutateUpdateSyllabus(formData)
      await mutateEditAssignment(values.assessment)
      setIsEdit(false)
      toast.success("Syllabus updated successfully!")
    } catch (error) {
      toast.error("Syllabus creation failed!")
    }
  }

  return (
    <section className="w-full">
      <div className="flex h-full min-h-screen w-full flex-col">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <SyllabusHeader form={form} onEdit={() => setIsEdit(true)} />
            <ProgramSubHeader
              days={days}
              hours={learningTime}
              modifiedBy={form.watch("modifiedBy")}
              modifiedOn={form.watch("modifiedDate")}
            />
            <div className="p-4">
              <SyllabusTab
                tabs={tabs}
                currentTab={tab}
                onTabClick={(index) => setTab(index)}
              />

              <div className="flex w-full">
                <div
                  className={cn(
                    "flex-1 overflow-hidden rounded-b-md rounded-r-md border-t-[1px]  bg-white p-6 shadow-md",
                    tab === 2 && "mr-4"
                  )}
                >
                  {tab === 1 && <GeneralPreviewTab form={form} />}
                  {tab === 2 && <OutlinePreviewTab form={form} />}
                  {tab === 3 && (
                    <OthersPreviewTab
                      form={form}
                      timeAllocation={calculateTimeAllocation(
                        form.watch("trainingUnits") as ITrainingUnit[]
                      )}
                    />
                  )}
                </div>
                {tab === 2 && (
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
              {isEdit && (
                <div className="mt-4 flex items-center justify-end space-x-2">
                  <Button variant="ghost" onClick={() => setIsEdit(false)}>
                    Cancel
                  </Button>
                  <Button>Save</Button>
                </div>
              )}
            </div>
          </form>
        </Form>
      </div>
    </section>
  )
}

export default SyllabusDetail
