import { CircleUserRound, FolderOpen } from "lucide-react"

import { TrainingContent } from "@/lib/types"
import { cn } from "@/lib/utils"

interface CollapsibleTrainingContentTabProps {
  data: TrainingContent
  index: number
}

export const CollapsibleTrainingContentTab = ({
  data,
  index
}: CollapsibleTrainingContentTabProps) => {
  return (
    <div
      className="flex w-full justify-between rounded-sm bg-zinc-100 px-3 py-1"
      key={index}
    >
      <p className="font-semibold">{data.content}</p>
      <div className="flex items-center justify-center space-x-4 text-sm">
        <span className="items-center rounded-sm bg-primary px-2.5 py-1 text-xs font-medium text-white">
          K6SD
        </span>
        <span>{data.duration} mins</span>
        <span
          className={cn(
            "items-center rounded-sm border border-green-400 bg-green-100 px-2.5 py-1 text-xs font-medium text-green-800",
            data.trainingFormat === "Offline"
              ? "border-red-400 bg-red-100 text-red-800"
              : "border-green-400 bg-green-100 text-green-800"
          )}
        >
          {data.trainingFormat}
        </span>
        <button>
          <CircleUserRound className="h-5 w-5" />
        </button>
        <button>
          <FolderOpen className="h-5 w-5" />
        </button>
      </div>
    </div>
  )
}
