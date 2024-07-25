import { useState } from "react"

import { format } from "date-fns"
import { XCircle } from "lucide-react"

import { IPublishStatus, ISyllabus } from "@/types/syllabus.interface"

import { calculateTotalDurationOfSyllabus } from "@/lib/utils"

import { Card } from "../atoms/card"
import Chip from "../atoms/chip"
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "../atoms/collapsible"
import { TrainingUnitList } from "./TrainingUnitList"

interface SyllabusCardProps {
  syllabus: ISyllabus
  onDelete?: (syllabus: ISyllabus) => void
  isOpen?: boolean
  isEdit: boolean
}

export const SyllabusCard = ({
  syllabus,
  onDelete,
  isOpen = false,
  isEdit = false
}: SyllabusCardProps) => {
  const [open, setOpen] = useState<boolean>(isOpen)
  const { learningTime, days } = calculateTotalDurationOfSyllabus(syllabus)
  const modifiedDate =
    syllabus.modifiedDate === "0001-01-01T00:00:00"
      ? syllabus.createdDate
      : syllabus.modifiedDate
  const modifiedBy = syllabus.modifiedBy ?? syllabus.createdBy

  return (
    <Collapsible open={open} onOpenChange={setOpen}>
      <Card className="overflow-hidden rounded-2xl border shadow-md">
        <CollapsibleTrigger className="flex w-full flex-col p-4">
          <div className="flex w-full items-center justify-between ">
            <div className="flex items-center space-x-4">
              <p className="text-2xl font-medium">{syllabus.topicName}</p>
              <Chip
                content={getStatusContent(
                  syllabus.pulishStatus as IPublishStatus
                )}
                color={"hsl(var(--primary))"}
              />
            </div>

            {onDelete && isEdit && (
              <button
                type="button"
                className="hover:text-red-800"
                onClick={() => onDelete(syllabus)}
              >
                <XCircle className="h-6 w-6" />
              </button>
            )}
          </div>

          <div className="mt-2 flex space-x-3">
            <span>
              {syllabus.topicCode} v{syllabus.version}
            </span>
            <span>|</span>
            <span>
              {days} days ({learningTime} hours)
            </span>

            <span>|</span>
            <span>
              Modified on{" "}
              {syllabus.modifiedDate
                ? format(new Date(modifiedDate), "dd/MM/yyyy")
                : "Unknown"}{" "}
              by {modifiedBy || "Unknown"}
            </span>
          </div>
        </CollapsibleTrigger>
        <CollapsibleContent>
          <div className="space-y-2">
            <TrainingUnitList unitList={syllabus.trainingUnits} mode="view" />
          </div>
        </CollapsibleContent>
      </Card>
    </Collapsible>
  )
}

const getStatusContent = (status: IPublishStatus): string => {
  switch (status) {
    case IPublishStatus.Denied:
      return "Denied"
    case IPublishStatus.Editing:
      return "Editing"
    case IPublishStatus.Pending:
      return "Pending"
    case IPublishStatus.Published:
      return "Published"
    default:
      return "Unknown" // Or any default content you want
  }
}
