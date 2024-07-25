import { useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { PlusCircle } from "lucide-react"
import { toast } from "sonner"

import { ITrainingContent, ITrainingUnit } from "@/types/syllabus.interface"

import {
  useAddNewTrainingContent,
  useDeleteTrainingContent
} from "@/apis/training-content-routes"
import {
  useAddNewTrainingUnit,
  useDeleteAllUnitByDay,
  useDeleteTrainingUnit,
  useEditTrainingUnit
} from "@/apis/training-unit-routes"

import { Button } from "@/components/global/atoms/button"
import { TrainingUnitList } from "@/components/global/molecules/TrainingUnitList"

import { TrainingUnitAddModal } from "./TrainingUnitAddModal"

export const OutlinePreviewTab = ({ form }: { form: any }) => {
  const [showAddModal, setShowAddModal] = useState(false)
  const topicCode = form.watch("topicCode") as string
  const { isEdit } = useSyllabusDetailContext()

  const { mutateAsync: mutateAddTrainingUnit } =
    useAddNewTrainingUnit(topicCode)
  const { mutateAsync: mutateDeleteTrainingContent } =
    useDeleteTrainingContent(topicCode)
  const { mutateAsync: mutateEditTrainingUnit } = useEditTrainingUnit(topicCode)
  const { mutateAsync: mutateAddTrainingContent } =
    useAddNewTrainingContent(topicCode)
  const { mutateAsync: mutateDeleteAllUnitByDay } =
    useDeleteAllUnitByDay(topicCode)
  const { mutateAsync: mutateDeleteTrainingUnit } =
    useDeleteTrainingUnit(topicCode)

  const maxDayNumber = Math.max(
    ...form.watch("trainingUnits").map((unit: ITrainingUnit) => unit.dayNumber)
  )
  const newDayNumber = maxDayNumber + 1

  const handleDeleteDay = async (dayNumber: number) => {
    try {
      await mutateDeleteAllUnitByDay(dayNumber)
      toast.success("Delete day successfully !")
    } catch (error) {
      toast.error("Failed to delete day !")
    }
  }

  const handleSetNameUnit = async (unitCode: string, unitName: string) => {
    try {
      await mutateEditTrainingUnit({
        unitCode: unitCode,
        unitName: unitName
      })
      toast.success("Edit unit name successfully !")
    } catch (error) {
      toast.error("Failed to edit unit name !")
    }
  }

  const handleSetContentUnit = async (
    unitCode: string,
    content: ITrainingContent
  ) => {
    try {
      await mutateAddTrainingContent({
        ...content,
        unitCode: unitCode
      })
      toast.success("Add new content successfully !")
    } catch (error) {
      toast.error("Failed to add new content !")
    }
  }

  const handleDeleteContentUnit = async (contentId: string) => {
    try {
      await mutateDeleteTrainingContent(contentId)
      toast.success("Delete content successfully !")
    } catch (error) {
      toast.error("Failed to delete content !")
    }
  }

  const handleAddUnit = async (dayNumber: number, unitName: string) => {
    try {
      await mutateAddTrainingUnit({
        unitName: unitName,
        dayNumber: dayNumber,
        topicCode: form.watch("topicCode")
      })
      toast.success("Add new unit successfully !")
    } catch (error) {
      toast.error("Failed to add new unit !")
    }
  }

  const handleDeleteUnit = async (unitCode: string) => {
    try {
      await mutateDeleteTrainingUnit(unitCode)
      toast.success("Delete unit successfully!")
    } catch (error) {
      toast.error("Failed to delete unit!")
    }
  }

  return (
    <div className="mb-2">
      <TrainingUnitList
        unitList={form.watch("trainingUnits")}
        handleDeleteDay={handleDeleteDay}
        handleSetNameUnit={handleSetNameUnit}
        handleAddUnit={handleAddUnit}
        handleDeleteUnit={handleDeleteUnit}
        handleSetContentUnit={handleSetContentUnit}
        handleDeleteContentUnit={handleDeleteContentUnit}
        isUpdate={isEdit}
        mode={isEdit ? "edit" : "view"}
        isOpen={true}
      />
      {isEdit && (
        <>
          <br />
          <Button onClick={() => setShowAddModal(true)} type="button">
            <PlusCircle className="h-4 w-4" /> Add day
          </Button>
          <TrainingUnitAddModal
            isOpen={showAddModal}
            onClose={() => setShowAddModal(false)}
            dayNumber={newDayNumber}
            handleAddUnit={handleAddUnit}
          />
        </>
      )}
    </div>
  )
}
