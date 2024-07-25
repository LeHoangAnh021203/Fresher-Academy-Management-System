import { ColumnDef } from "@tanstack/react-table"
import { Link } from "react-router-dom"

import {
  IClass,
  IClassAttendee,
  IClassFSU,
  IClassLocation
} from "@/types/class.interface"

import { formatDateVN } from "@/lib/utils"

import Chip from "@/components/global/atoms/chip"
import { DataTableColumnHeader } from "@/components/global/molecules/data-table/data-table-column-header"

import { DataTableRowActions } from "./data-table-row-actions"

const attendeeColorMapping = {
  [IClassAttendee.Fresher]: "#FF7568",
  [IClassAttendee.OnlineFeeFresher]: "#2F903F",
  [IClassAttendee.OfflineFeeFresher]: "#D45B13",
  [IClassAttendee.Intern]: "#2D3748"
}

export const columns: ColumnDef<IClass>[] = [
  {
    accessorKey: "className",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Class" />
    ),
    cell: ({ row }) => {
      return (
        <div className="max-w-[500px] truncate font-semibold">
          <Link to={`/class/${row.original.classID}`}>
            {row.original.className}
          </Link>
        </div>
      )
    }
  },
  {
    accessorKey: "classCode",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Class code" />
    ),
    cell: ({ row }) => {
      return (
        <div>
          {row.getValue("classCode") ? row.getValue("classCode") : "NaN"}
        </div>
      )
    },
    enableHiding: false
  },
  {
    accessorKey: "createdDate",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Create on" />
    ),
    cell: ({ row }) => {
      return <div>{formatDateVN(row.original.createdDate)}</div>
    }
  },
  {
    accessorKey: "createdBy",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Created by" />
    ),
    cell: ({ row }) => {
      return (
        <div>{row.original.createdBy ? row.original.createdBy : "NaN"}</div>
      )
    }
  },
  {
    accessorKey: "duration",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Duration" />
    ),
    cell: ({ row }) => {
      const duration = row.original.duration
      if (duration > 1) return <div>{duration} days</div>
      else return <div>{duration} day</div>
    }
  },
  {
    accessorKey: "attendee",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Attendee" />
    ),
    cell: ({ row }) => {
      const attendeeValue = row.getValue("attendee") as IClassAttendee

      const attendeeLabel = (attendee: IClassAttendee): string => {
        switch (attendee) {
          case IClassAttendee.Fresher:
            return "Fresher"
          case IClassAttendee.OnlineFeeFresher:
            return "Online fee-fresher"
          case IClassAttendee.OfflineFeeFresher:
            return "Offline fee-fresher"
          case IClassAttendee.Intern:
            return "Intern"
          default:
            return "Unknown"
        }
      }

      const content = attendeeLabel(attendeeValue)
      const color = attendeeColorMapping[attendeeValue] || "gray"

      return <Chip content={content} color={color} />
    }
  },
  {
    accessorKey: "locationId",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Location" />
    ),
    cell: ({ row }) => {
      const locationName = IClassLocation[row.original.locationId]
      return <div>{locationName || "NaN"}</div>
    }
  },
  {
    accessorKey: "fsuId",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="FSU" />
    ),
    cell: ({ row }) => {
      const fsuName = IClassFSU[row.original.fsuId]
      return <div>{fsuName || "NaN"}</div>
    }
  },
  {
    id: "actions",
    cell: ({ row }) => {
      return <DataTableRowActions row={row} />
    }
  }
]
