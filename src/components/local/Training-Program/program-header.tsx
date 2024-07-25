import { Eye, EyeOff, MoreHorizontal, Pencil, Trash } from "lucide-react"
import { useParams } from "react-router-dom"

import { ITrainingProgramStatus } from "@/types/training-program.interface"

import {
  useDeleteTrainingProgram,
  useUpdateTrainingProgramStatus
} from "@/apis/training-program-routes"

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"
import { Skeleton } from "@/components/global/atoms/skeleton"
import { ConfirmModal } from "@/components/global/molecules/Confirm-Modal"

type ProgramHeaderProps = {
  title: string | undefined
  status: ITrainingProgramStatus | undefined
  onEdit?: () => void
  mode: "create" | "edit"
}

export const ProgramHeader = ({
  title,
  onEdit,
  status,
  mode = "create"
}: ProgramHeaderProps) => {
  const { id } = useParams()
  const { mutateAsync: mutateDeleteTrainingProgram } =
    useDeleteTrainingProgram()
  const { mutateAsync: mutateUpdateTrainingProgramStatus } =
    useUpdateTrainingProgramStatus(id!)
  return (
    <div className="flex w-full items-end justify-between space-y-4 bg-primary p-5">
      <div>
        <p className="mb-[10px] text-2xl tracking-[4.8px] text-white">
          Training Program
        </p>
        <div className="flex items-center space-x-4">
          {title ? (
            <h1 className="text-4xl font-bold tracking-[7.2px] text-white">
              {title}
            </h1>
          ) : (
            <Skeleton className="h-8 w-96" />
          )}

          <span className="inline-flex cursor-pointer select-none items-center rounded-[50px] border-2 px-4 py-[6px] text-xs text-white">
            {status === ITrainingProgramStatus.Active ? "Active" : "Inactive"}
          </span>
        </div>
      </div>
      {mode === "edit" && (
        <DropdownMenu>
          <DropdownMenuTrigger className=" rounded-full p-1 text-white hover:bg-gray-100 hover:text-gray-800">
            <MoreHorizontal className="h-7 w-7 " />
          </DropdownMenuTrigger>
          <DropdownMenuContent className="mr-5">
            <DropdownMenuLabel>Manage</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem className="flex items-center" onClick={onEdit}>
              <Pencil className="mr-2 h-4 w-4" /> Edit program
            </DropdownMenuItem>
            <DropdownMenuItem
              className="flex items-center"
              onClick={() => mutateUpdateTrainingProgramStatus(id as string)}
            >
              {status === ITrainingProgramStatus.Active ? (
                <EyeOff className="mr-2 h-4 w-4" />
              ) : (
                <Eye className="mr-2 h-4 w-4" />
              )}
              {status === ITrainingProgramStatus.Active
                ? "Inactivate"
                : "Activate"}
            </DropdownMenuItem>
            <DropdownMenuSeparator />
            <ConfirmModal
              onConfirm={() => mutateDeleteTrainingProgram(id as string)}
            >
              <div className="relative flex cursor-pointer select-none items-center rounded-sm px-2 py-1.5 text-sm outline-none transition-colors hover:bg-zinc-100 focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50">
                <Trash className="mr-2 h-4 w-4" /> Delete program
              </div>
            </ConfirmModal>
          </DropdownMenuContent>
        </DropdownMenu>
      )}
    </div>
  )
}
