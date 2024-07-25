import { ColumnDef } from "@tanstack/react-table"
import { format } from "date-fns"
import { Link } from "react-router-dom"

import {
  ITrainingProgramPreview,
  ITrainingProgramStatus
} from "@/types/training-program.interface"

import { convertMinutesToHoursAndMinutes } from "@/lib/utils"

import Chip from "@/components/global/atoms/chip"
import { DataTableColumnHeader } from "@/components/global/molecules/data-table/data-table-column-header"

import { DataTableRowActions } from "./data-table-row-actions"

export const columns: ColumnDef<ITrainingProgramPreview>[] = [
  {
    accessorKey: "trainingProgramCode",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="ID" />
    ),
    cell: ({ row }) => {
      return <div>{row.getValue("trainingProgramCode")}</div>
    },
    enableHiding: false
  },
  {
    accessorKey: "name",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Name" />
    ),
    cell: ({ row }) => {
      return (
        <div className="max-w-[500px] truncate font-semibold">
          <Link
            to={`/training-programs/${row.getValue("trainingProgramCode")}`}
          >
            {row.getValue("name")}
          </Link>
        </div>
      )
    }
  },
  {
    accessorKey: "createDate",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Created on" />
    ),
    cell: ({ row }) => {
      const createdOnDate = new Date(row.getValue("createDate"))
      return <div>{format(createdOnDate, "dd/MM/yyyy")}</div>
    }
  },
  {
    accessorKey: "createBy",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Create by" />
    )
  },
  {
    accessorKey: "duration",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Duration" />
    ),
    cell: ({ row }) => {
      const duration = row.getValue("duration") as number
      const { hours, minutes } = convertMinutesToHoursAndMinutes(duration)
      return (
        <div>
          {hours}h {minutes}m{" "}
        </div>
      )
    }
  },
  {
    accessorKey: "status",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Status" />
    ),
    cell: ({ row }) => {
      const type = row.getValue("status") as ITrainingProgramStatus
      const color =
        type === ITrainingProgramStatus.Active ? "#2F855A" : "#E53E3E"
      const content =
        type === ITrainingProgramStatus.Active ? "Active" : "Inactive"
      return <Chip content={content} color={color} />
    },
    filterFn: (row, id, value) => {
      return value.includes(row.getValue(id))
    }
  },
  {
    id: "actions",
    cell: ({ row }) => {
      return <DataTableRowActions row={row} />
    }
  }
]
