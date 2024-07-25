import { format } from "date-fns"
import { XCircle } from "lucide-react"

import { IPublishStatus, ISyllabusPreview } from "@/types/syllabus.interface"

import { convertMinutesToHoursAndMinutes } from "@/lib/utils"

import { Card } from "../atoms/card"
import Chip from "../atoms/chip"

interface SyllabusCardPreviewProps {
  syllabus: ISyllabusPreview
  onDelete?: (syllabus: ISyllabusPreview) => void
}

export const SyllabusCardPreview = ({
  syllabus,
  onDelete
}: SyllabusCardPreviewProps) => {
  const { hours, minutes } = convertMinutesToHoursAndMinutes(syllabus.duration)
  const modifiedDate =
    syllabus.modifiedDate === "0001-01-01T00:00:00"
      ? syllabus.createdDate
      : syllabus.modifiedDate
  const modifiedBy = syllabus.modifiedBy ?? syllabus.createdBy
  return (
    <Card className="overflow-hidden rounded-2xl border shadow-md">
      <div className="flex w-full flex-col p-4">
        <div className="flex w-full items-center justify-between ">
          <div className="flex items-center space-x-4">
            <p className="text-2xl font-medium">{syllabus.topicName}</p>
            <Chip
              content={getStatusContent(
                syllabus.publishStatus as IPublishStatus
              )}
              color={"hsl(var(--primary))"}
            />
          </div>

          {onDelete && (
            <button
              type="button"
              className="hover:text-red-800"
              onClick={() => onDelete(syllabus)}
            >
              <XCircle className="h-6 w-6" />
            </button>
          )}
        </div>

        {/* TEST GQL */}
        <div className="mt-2 flex space-x-3">
          <span>{syllabus.topicCode}</span>
          <span>|</span>
          <span>
            {hours}h {minutes}m
          </span>

          <span>|</span>
          <span>
            Modified on {format(new Date(modifiedDate), "dd/MM/yyyy")} by{" "}
            {modifiedBy}
          </span>
        </div>
      </div>
    </Card>
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
