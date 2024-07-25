import { ChevronDownCircle, Pencil, Trash } from "lucide-react"

import { TrainingContent, TrainingUnit } from "@/lib/types"

import { Button } from "@/components/global/atoms/button"
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "@/components/global/atoms/collapsible"
import {
  Dialog,
  DialogContent,
  DialogTrigger
} from "@/components/global/atoms/dialog"

import { CollapsibleTrainingContentTab } from "./collapsible-training-content-tab"

interface CollapsibleUnitDayTabProps {
  data: TrainingUnit
  index: number
}

function getTotalDuration(trainingContents: TrainingContent[]): {
  totalHours: string
} {
  let totalDurationInMinutes = 0

  trainingContents.forEach((content) => {
    totalDurationInMinutes += content.duration
  })

  const totalHours = (totalDurationInMinutes / 60).toFixed(1)

  return { totalHours }
}

export const CollapsibleUnitDayTab = ({
  data,
  index
}: CollapsibleUnitDayTabProps) => {
  const { totalHours } = getTotalDuration(data.trainingContents)
  return (
    <Collapsible key={index}>
      <CollapsibleTrigger className="flex w-full text-left">
        <button className="w-full rounded-t-lg bg-primary px-5 py-2 text-left font-semibold text-white">
          Day {data.dayNumber}
        </button>
      </CollapsibleTrigger>
      <CollapsibleContent>
        <div className="flex w-full p-5">
          <div className="w-[10%] min-w-[10%]">
            <Dialog>
              <DialogTrigger asChild>
                <button className="text-nowrap rounded-sm px-2 py-1 font-semibold hover:bg-zinc-100">
                  {data.unitCode}
                </button>
              </DialogTrigger>
              <DialogContent>
                <div className="flex w-full flex-col justify-between space-y-4">
                  <div className="flex space-x-7 font-semibold">
                    <p className="">Unit 8</p>
                    <p className="">.NET Introduction</p>
                  </div>
                  <div className="flex flex-col rounded-md bg-zinc-100 px-2.5 py-1.5">
                    <p className="text-sm font-semibold">.NET Introduction</p>
                    <div className="mt-2 space-y-4">
                      <div className="flex items-center justify-between text-xs">
                        <a href="" className="text-[#0C4DA2] underline">
                          .NET Introduction overview.pdf
                        </a>
                        <div className="flex items-center justify-between space-x-4 text-xs">
                          <p className="text-muted-foreground">
                            by John Doe on 2022-01-01
                          </p>
                          <button>
                            <Pencil className="h-4 w-4" />
                          </button>

                          <button>
                            <Trash className="h-4 w-4 " />
                          </button>
                        </div>
                      </div>
                      <div className="flex items-center justify-between text-xs">
                        <a href="" className="text-[#0C4DA2] underline">
                          .NET Introduction overview.pdf
                        </a>
                        <div className="flex items-center justify-between space-x-4 text-xs">
                          <p className="text-muted-foreground">
                            by John Doe on 2022-01-01
                          </p>
                          <button>
                            <Pencil className="h-4 w-4" />
                          </button>
                          <button>
                            <Trash className="h-4 w-4 " />
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="flex w-full justify-end">
                    <Button size={"sm"}>Upload new</Button>
                  </div>
                </div>
              </DialogContent>
            </Dialog>
          </div>
          <div className="flex w-full flex-col ">
            <Collapsible>
              <div className="flex items-start justify-between">
                <div>
                  <h1 className="font-semibold">{data.unitName}</h1>
                  <span className="text-sm text-muted-foreground">
                    {totalHours}hrs
                  </span>
                </div>
                <CollapsibleTrigger>
                  <ChevronDownCircle className="h-5 w-5" />
                </CollapsibleTrigger>
              </div>
              <CollapsibleContent className="mt-2 space-y-2">
                {data.trainingContents.map((content, index) => (
                  <CollapsibleTrainingContentTab data={content} index={index} />
                ))}
              </CollapsibleContent>
            </Collapsible>
          </div>
        </div>
      </CollapsibleContent>
    </Collapsible>
  )
}
