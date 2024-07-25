import { ColumnDef } from "@tanstack/react-table"
import { format } from "date-fns"
import { Link } from "react-router-dom"

import { IPublishStatus, ISyllabusPreview } from "@/types/syllabus.interface"

import { convertMinutesToHoursAndMinutes } from "@/lib/utils"

import Chip from "@/components/global/atoms/chip"
import { DataTableColumnHeader } from "@/components/global/molecules/data-table/data-table-column-header"

import { DataTableRowActions } from "./data-table-row-actions"

export const columns: ColumnDef<ISyllabusPreview>[] = [
  {
    accessorKey: "topicName",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Syllabus" />
    ),
    cell: ({ row }) => {
      return (
        <Link to={`/syllabus/${row.getValue("topicCode")}`}>
          <div>{row.getValue("topicName")}</div>
        </Link>
      )
    },
    enableHiding: false
  },
  {
    accessorKey: "topicCode",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Code" />
    ),
    cell: ({ row }) => {
      return (
        <div className="max-w-[500px] truncate font-semibold">
          {row.getValue("topicCode")}
        </div>
      )
    }
  },
  {
    accessorKey: "createdDate",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Created on" />
    ),
    cell: ({ row }) => {
      const createdOnDate = new Date(row.getValue("createdDate"))
      return <div>{format(createdOnDate, "dd/MM/yyyy")}</div>
    }
  },
  {
    accessorKey: "createdBy",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Create by" />
    ),
    cell: ({ row }) => {
      return <div>{row.getValue("createdBy")}</div>
    }
  },
  {
    accessorKey: "duration",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Duration" />
    ),
    cell: ({ row }) => {
      const duration = row.getValue("duration") as number
      const { hours, minutes } = convertMinutesToHoursAndMinutes(duration)
      return <div>{`${hours}h ${minutes}m`}</div>
    }
  },
  {
    accessorKey: "outputStandard",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Output standard" />
    ),
    cell: ({ row }) => {
      const outputStandard: string[] = row.getValue("outputStandard")
      return (
        <div className="flex gap-2">
          {outputStandard.map((standard, i) => (
            <Chip content={standard} color="#2D3748" key={i} />
          ))}
        </div>
      )
    }
  },
  {
    accessorKey: "publishStatus",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Status" />
    ),
    cell: ({ row }) => {
      const type = row.getValue("publishStatus") as IPublishStatus
      let color = "#B9B9B9"

      switch (type) {
        case IPublishStatus.Denied:
          color = "#B84848"
          break
        case IPublishStatus.Editing:
          color = "#285D9A"
          break
        case IPublishStatus.Pending:
          color = "#FFA500"
          break
        case IPublishStatus.Published:
          color = "#4DB848"
          break
        default:
          break
      }

      return <Chip content={IPublishStatus[type]} color={color} />
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
