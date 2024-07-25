import React from "react"

import { PlusCircle } from "lucide-react"
import { v4 as uuid } from "uuid"

import { ITrainingContent, ITrainingUnit } from "@/types/syllabus.interface"

import { Button } from "@/components/global/atoms/button"
import { TrainingUnitList } from "@/components/global/molecules/TrainingUnitList"

interface OutlineTabProps {
  trainingUnits: ITrainingUnit[]
  setTrainingUnits: React.Dispatch<React.SetStateAction<ITrainingUnit[]>>
}

const OutlineTab = ({ trainingUnits, setTrainingUnits }: OutlineTabProps) => {
  const handleAddDay = () => {
    const maxDay = trainingUnits.reduce((max, unit) => {
      return unit.dayNumber > max ? unit.dayNumber : max
    }, 0)
    setTrainingUnits((prev) => {
      return [
        ...prev,
        {
          unitCode: `Unit ${prev.length + 1}`,
          unitName: `Example Unit name`,
          trainingContents: [],
          dayNumber: maxDay + 1,
          topicCode: `T0789`
        }
      ]
    })
  }

  const handleDeleteDay = (dayNumber: number) => {
    setTrainingUnits((prevUnits) => {
      // Filter out the units that belong to the specified dayNumber
      const updatedUnits = prevUnits.filter(
        (unit) => unit.dayNumber !== dayNumber
      )

      // Adjust day numbers of the remaining units
      const updatedUnitsWithAdjustedDayNumbers = updatedUnits.map((unit) => {
        if (unit.dayNumber > dayNumber) {
          return { ...unit, dayNumber: unit.dayNumber - 1 }
        }
        return unit
      })

      return updatedUnitsWithAdjustedDayNumbers
    })
  }

  const handleSetNameUnit = (unitCode: string, unitName: string) => {
    setTrainingUnits((unitList) => {
      const unit = unitList.find((item) => item.unitCode === unitCode)
      if (unit) {
        unit.unitName = unitName
      }
      return unitList
    })
  }

  const handleSetContentUnit = (
    unitCode: string,
    content: ITrainingContent
  ) => {
    setTrainingUnits((unitList) => {
      const unit = unitList.find((item) => item.unitCode === unitCode)
      if (unit) {
        const contentWithId = { ...content, contentId: uuid() }
        unit.trainingContents.push(contentWithId)
      }
      return unitList
    })
  }

  const handleDeleteContentUnit = (contentId: string) => {
    setTrainingUnits((unitList) => {
      // Map over each unit in the unitList
      const updatedUnits = unitList.map((unit) => {
        // Filter out the content with the specified contentId from each unit
        unit.trainingContents = unit.trainingContents.filter(
          (content) => content.contentId !== contentId
        )
        return unit
      })
      return updatedUnits
    })
  }

  const handleAddUnit = (dayNumber: number, unitName: string) => {
    setTrainingUnits((prev) => {
      return [
        ...prev,
        {
          unitCode: `Unit ${prev.length + 1}`,
          unitName: unitName,
          trainingContents: [],
          dayNumber: dayNumber,
          topicCode: `T0789`
        }
      ]
    })
  }

  const handleDeleteUnit = (unitCode: string) => {
    setTrainingUnits((prevUnits) => {
      // Find the unit to delete
      const unitToDelete = prevUnits.find((unit) => unit.unitCode === unitCode)
      if (!unitToDelete) return prevUnits

      // Remove the unit
      const updatedUnits = prevUnits.filter(
        (unit) => unit.unitCode !== unitCode
      )

      // Update the unitCodes of the remaining units
      const updatedUnitsWithAdjustedUnitCodes = updatedUnits.map(
        (unit, index) => {
          // Adjust unitCode to maintain sequence
          return { ...unit, unitCode: `Unit ${index + 1}` }
        }
      )

      return updatedUnitsWithAdjustedUnitCodes
    })
  }

  return (
    <div className="mb-2">
      <TrainingUnitList
        unitList={trainingUnits}
        isUpdate={true}
        handleSetNameUnit={handleSetNameUnit}
        handleSetContentUnit={handleSetContentUnit}
        handleDeleteContentUnit={handleDeleteContentUnit}
        handleAddUnit={handleAddUnit}
        handleDeleteUnit={handleDeleteUnit}
        handleDeleteDay={handleDeleteDay}
        mode="create"
        isOpen={true}
      />
      <br />
      <Button onClick={() => handleAddDay()} title="aa" type="button">
        <PlusCircle className="h-4 w-4" /> Add day
      </Button>
    </div>
  )
}

export default OutlineTab
