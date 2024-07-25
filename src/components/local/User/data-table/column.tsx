import { ColumnDef } from "@tanstack/react-table"
import { format } from "date-fns"
import { UserRound } from "lucide-react"
import { Link } from "react-router-dom"

import { IUser, IUserGender, IUserRole } from "@/types/user.interface"

import Chip from "@/components/global/atoms/chip"
import { DataTableColumnHeader } from "@/components/global/molecules/data-table/data-table-column-header"

import { DataTableRowActions } from "./data-table-row-actions"

export const columns: ColumnDef<IUser>[] = [
  {
    accessorKey: "userId",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="ID" />
    ),
    cell: ({ row }) => {
      const userId = row.getValue("userId") as string
      const shortenedUserId = userId.substring(0, 8)
      return <div className="w-[50px]">{shortenedUserId}</div>
    },
    enableHiding: false
  },
  {
    accessorKey: "name",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Full Name" />
    ),
    cell: ({ row }) => {
      return (
        <Link to={`/users/${row.getValue("userId")}`}>
          <div className="max-w-[500px] truncate font-semibold">
            {row.getValue("name")}
          </div>
        </Link>
      )
    }
  },
  {
    accessorKey: "email",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Email" />
    )
  },
  {
    accessorKey: "dob",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Date of Birth" />
    ),
    cell: ({ row }) => {
      const createdOnDate = new Date(row.getValue("dob"))
      return <div>{format(createdOnDate, "dd/MM/yyyy")}</div>
    }
  },
  {
    accessorKey: "gender",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Gender" />
    ),
    cell: ({ row }) => {
      const gender = row.getValue("gender") as IUserGender
      const color = gender === IUserGender.Female ? "#D45B13" : "#000000"
      return (
        <div className="mx-5">
          <UserRound color={color} />
        </div>
      )
    },
    filterFn: (row, id, value) => {
      return value.includes(row.getValue(id))
    }
  },
  {
    accessorKey: "permissionId",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Type" />
    ),
    cell: ({ row }) => {
      const role = row.getValue("permissionId") as IUserRole
      const roleName =
        role == IUserRole.SuperAdmin
          ? "Super Admin"
          : role == IUserRole.ClassAdmin
            ? "Admin"
            : "Trainer"
      const color =
        role === IUserRole.SuperAdmin
          ? "#D45B13"
          : role === IUserRole.ClassAdmin
            ? "#4DB848"
            : "#2D3748"

      return <Chip content={roleName} color={color} />
    },
    filterFn: (row, id, value) => {
      return value.includes(row.getValue(id))
    }
  },
  {
    accessorKey: "status",
    header: () => <div className="hidden"></div>,
    cell: ({ row }) => {
      return <div className="hidden">{row.getValue("status")}</div>
    }
  },
  {
    id: "actions",
    cell: ({ row }) => {
      return <DataTableRowActions row={row} />
    }
  }
]
