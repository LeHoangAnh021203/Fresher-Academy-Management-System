import { MODIFY_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { DotsHorizontalIcon } from "@radix-ui/react-icons"
import { Row } from "@tanstack/react-table"
import { Eye, EyeOff, Pencil, Trash } from "lucide-react"
import { Link } from "react-router-dom"

import { ITrainingProgramStatus } from "@/types/training-program.interface"

import {
  useDeleteTrainingProgram,
  useUpdateTrainingProgramStatus
} from "@/apis/training-program-routes"

import { Button } from "@/components/global/atoms/button"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"
import { ConfirmModal } from "@/components/global/molecules/Confirm-Modal"

interface DataTableRowActionsProps<TData> {
  row: Row<TData>
}

export function DataTableRowActions<TData>({
  row
}: DataTableRowActionsProps<TData>) {
  const { permissions } = useAuthContext()
  const status = row.getValue("status") as ITrainingProgramStatus
  const trainingCode = row.getValue("trainingProgramCode") as string
  const { mutateAsync: mutateDeleteTrainingProgram } =
    useDeleteTrainingProgram()
  const { mutateAsync: mutateUpdateTrainingProgramStatus } =
    useUpdateTrainingProgramStatus()

  return (
    <>
      <DropdownMenu>
        <DropdownMenuTrigger
          asChild
          disabled={
            !MODIFY_PERMISSION.includes(
              permissions?.trainingProgram ? permissions?.trainingProgram : 0
            )
          }
        >
          <Button
            variant="ghost"
            className="flex h-8 w-8 p-0 data-[state=open]:bg-muted"
          >
            <DotsHorizontalIcon className="h-4 w-4" />
            <span className="sr-only">Open menu</span>
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end" className="w-[180px]">
          <DropdownMenuLabel>Manage</DropdownMenuLabel>

          <Link
            to={`/training-programs/${row.getValue("trainingProgramCode")}`}
          >
            <DropdownMenuItem
              disabled={
                !MODIFY_PERMISSION.includes(
                  permissions?.trainingProgram
                    ? permissions?.trainingProgram
                    : 0
                )
              }
            >
              <Pencil className="mr-2 h-4 w-4" /> Edit program
            </DropdownMenuItem>
          </Link>
          <DropdownMenuItem
            onClick={() => mutateUpdateTrainingProgramStatus(trainingCode)}
            disabled={
              !MODIFY_PERMISSION.includes(
                permissions?.trainingProgram ? permissions?.trainingProgram : 0
              )
            }
          >
            {status == ITrainingProgramStatus.Active ? (
              <EyeOff className="mr-2 h-4 w-4" />
            ) : (
              <Eye className="mr-2 h-4 w-4" />
            )}
            {status == ITrainingProgramStatus.Active ? "Inactive" : "Activate"}
          </DropdownMenuItem>
          <DropdownMenuSeparator />
          <ConfirmModal
            onConfirm={() => mutateDeleteTrainingProgram(trainingCode)}
          >
            <div className="relative flex cursor-default select-none items-center rounded-sm px-2 py-1.5 text-sm outline-none transition-colors hover:bg-slate-100 focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50">
              <Trash className="mr-2 h-4 w-4" /> Delete
            </div>
          </ConfirmModal>
        </DropdownMenuContent>
      </DropdownMenu>
    </>
  )
}
