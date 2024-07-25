import { format } from "date-fns"

import { Skeleton } from "@/components/global/atoms/skeleton"

type Props = {
  days: number
  hours: number
  modifiedBy: string | undefined
  modifiedOn: string | Date
}

export const ProgramSubHeader = ({
  days,
  hours,
  modifiedBy,
  modifiedOn
}: Props) => {
  const formattedDate = modifiedOn
    ? format(new Date(modifiedOn), "dd/MM/yyyy")
    : ""

  return (
    <div className="flex flex-col gap-[5px] p-5">
      <p>
        <span className="text-2xl font-semibold">{days}</span> days{" "}
        <span className="italic">({hours.toFixed(2)} hours)</span>
      </p>

      {modifiedOn && modifiedBy ? (
        <p>
          Modified on <span className="italic">{formattedDate}</span> by{" "}
          <span className="font-semibold">{modifiedBy}</span>
        </p>
      ) : (
        <Skeleton className="h-5 w-64" />
      )}
    </div>
  )
}
