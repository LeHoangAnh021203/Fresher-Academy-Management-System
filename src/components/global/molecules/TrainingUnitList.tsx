import { useEffect, useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { MinusCircle } from "lucide-react"

import { ITrainingContent, ITrainingUnit } from "@/types/syllabus.interface"

import {
  Collapsible,
  CollapsibleTrigger
} from "@/components/global/atoms/collapsible"
import { TrainingUnitAddModal } from "@/components/local/Syllabus/TrainingUnitAddModal"

import { Button } from "../atoms/button"
import { CollapsibleContent } from "../atoms/collapsible"
import { TrainingUnit } from "./TrainingUnit"

interface TrainingUnitListProps {
  unitList: ITrainingUnit[]
  isUpdate?: boolean
  handleSetNameUnit: (unitCode: string, unitName: string) => void
  handleSetContentUnit: (unitCode: string, content: ITrainingContent) => void
  handleDeleteContentUnit: (contentId: string) => void
  handleAddUnit: (dayNumber: number, unitName: string) => void
  handleDeleteUnit: (unitCode: string) => void
  handleDeleteDay: (dayNumber: number) => void
  mode: "view" | "edit" | "create"
  isOpen?: boolean
}

export const TrainingUnitList = ({
  unitList,
  isUpdate,
  handleSetNameUnit,
  handleSetContentUnit,
  handleDeleteContentUnit,
  handleAddUnit,
  handleDeleteUnit,
  handleDeleteDay,
  mode = "view",
  isOpen = false
}: TrainingUnitListProps) => {
  const [openCollapsibles, setOpenCollapsibles] = useState<boolean[]>(
    Array(unitList.length).fill(isOpen)
  )
  const [openAddModal, setOpenAddModal] = useState(false)
  const [unitDayList, setUnitDayList] = useState<ITrainingUnit[][]>([])
  const { isEdit } = useSyllabusDetailContext()

  useEffect(() => {
    const getMaxDayNumber = (units: ITrainingUnit[]) => {
      return units.reduce(
        (maxDay, unit) => (unit.dayNumber > maxDay ? unit.dayNumber : maxDay),
        0
      )
    }

    const maxDayNumber = getMaxDayNumber(unitList)
    const temp: ITrainingUnit[][] = Array.from(
      { length: maxDayNumber },
      () => []
    )

    unitList.forEach((item) => {
      temp[item.dayNumber - 1].push(item)
    })

    setUnitDayList(temp)
  }, [unitList])

  const handleCollapsibleOpenChange = (index: number, isOpen: boolean) => {
    const newOpenCollapsibles = [...openCollapsibles]
    newOpenCollapsibles[index] = isOpen
    setOpenCollapsibles(newOpenCollapsibles)
  }

  return (
    <div className="max-h-[600px] space-y-2 overflow-y-scroll">
      {unitDayList.map((unitList, index) => {
        const dayNumber = index + 1
        return (
          <Collapsible
            key={index}
            className="mt-[1px]"
            open={
              openCollapsibles[index] || mode === "edit" || mode === "create"
            }
            onOpenChange={(isOpen) =>
              handleCollapsibleOpenChange(index, isOpen)
            }
          >
            <CollapsibleTrigger className="flex w-full cursor-pointer text-left">
              <div className="flex w-full space-x-4 bg-primary px-4 py-2 text-left font-medium text-white">
                <p>Day {dayNumber}</p>
                {(isEdit || mode === "edit" || mode === "create") && (
                  <button
                    onClick={() => {
                      if (mode === "create" || mode === "edit") {
                        handleDeleteDay && handleDeleteDay(index + 1)
                      }
                    }}
                    className="text-red-600"
                    role="DeleteDay"
                  >
                    <MinusCircle className="h-5 w-5 " />
                  </button>
                )}
              </div>
            </CollapsibleTrigger>
            <CollapsibleContent>
              <div className="flex w-full flex-col p-4">
                {unitList.map((item) => (
                  <TrainingUnit
                    unit={item}
                    key={item.unitCode}
                    isUpdate={isUpdate}
                    handleSetNameUnit={handleSetNameUnit}
                    handleSetContentUnit={handleSetContentUnit}
                    handleDeleteUnit={handleDeleteUnit}
                    handleDeleteContentUnit={handleDeleteContentUnit}
                    mode={mode}
                  />
                ))}
              </div>
            </CollapsibleContent>
            {(mode === "edit" || mode === "create") && (
              <Button
                type="button"
                className="mt-2"
                role="AddUnit"
                onClick={() => setOpenAddModal(true)}
              >
                Add Unit
              </Button>
            )}
            <TrainingUnitAddModal
              isOpen={openAddModal}
              onClose={() => setOpenAddModal(false)}
              dayNumber={dayNumber}
              handleAddUnit={handleAddUnit}
            />
          </Collapsible>
        )
      })}
    </div>
  )
}
