import { format } from "date-fns"

import { Syllabus } from "@/lib/types"

import { Card } from "@/components/global/atoms/card"
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "@/components/global/atoms/collapsible"

import { CollapsibleUnitDayTab } from "./collapsible-unit-day-tab"

interface CollapsibleSyllabusCardProps {
  data: Syllabus
  cardIndex: number
}

export const CollapsibleSyllabusCard = ({
  data,
  cardIndex
}: CollapsibleSyllabusCardProps) => {
  // const { day, hours } = convertToDaysAndHours(data?.duration)
  return (
    <Collapsible>
      <Card key={cardIndex}>
        <div className="flex h-full flex-col">
          <CollapsibleTrigger className="flex flex-col p-4">
            <div className="flex w-full items-center justify-between">
              <div className="flex items-center space-x-4">
                <p className="text-2xl">{data.topicName}</p>
                <span className="me-2 rounded bg-gray-100 px-2.5 py-0.5 text-xs font-medium text-gray-800">
                  {data.publishStatus}
                </span>
              </div>
            </div>
            <div className="mt-2 flex space-x-3">
              <span>{data.version}</span>
              <span>|</span>
              <span>12 days (23 hours)</span>
              <span>|</span>
              <span className="">
                Modified on {format(new Date(data?.modifiedOn), "MM/dd/yyyy")}{" "}
                by {data?.modifiedBy}
              </span>
            </div>
          </CollapsibleTrigger>
          <CollapsibleContent>
            <div className="space-y-2">
              {data.trainingUnits.map((unit, index) => (
                <CollapsibleUnitDayTab data={unit} index={index} />
              ))}
            </div>
          </CollapsibleContent>
        </div>
      </Card>
    </Collapsible>
  )
}
