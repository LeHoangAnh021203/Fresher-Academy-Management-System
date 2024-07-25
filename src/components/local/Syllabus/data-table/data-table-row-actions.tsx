import { MODIFY_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { DotsHorizontalIcon } from "@radix-ui/react-icons"
import { useQueryClient } from "@tanstack/react-query"
import { Row } from "@tanstack/react-table"
import { Copy, Pencil } from "lucide-react"
import { useNavigate } from "react-router-dom"
import { toast } from "sonner"

import { useDuplicateSyllabus } from "@/apis/syllabus-routes"

import { Button } from "@/components/global/atoms/button"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"

interface DataTableRowActionsProps<TData> {
  row: Row<TData>
}

export function DataTableRowActions<TData>({
  row
}: DataTableRowActionsProps<TData>) {
  const { permissions } = useAuthContext()
  const id = row.getValue("topicCode") as string
  const { setIsEdit } = useSyllabusDetailContext()
  const queryClient = useQueryClient()
  const navigate = useNavigate()

  const { isSuccess, mutate } = useDuplicateSyllabus(id)

  if (isSuccess) {
    toast.success("Syllabus duplicated successfully")
    queryClient.refetchQueries({ queryKey: ["syllabuses"] })
    queryClient.refetchQueries({ queryKey: ["syllabus", id] })
  }

  const handleEditSyllabus = () => {
    setIsEdit(true)
    navigate(`/syllabus/${id}`)
  }

  return (
    <>
      <DropdownMenu>
        <DropdownMenuTrigger
          asChild
          disabled={
            !MODIFY_PERMISSION.includes(
              permissions?.syllabus ? permissions?.syllabus : 0
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
        <DropdownMenuContent align="end" className="w-[200px]">
          <DropdownMenuLabel>Manage</DropdownMenuLabel>
          <DropdownMenuItem
            onClick={handleEditSyllabus}
            disabled={
              !MODIFY_PERMISSION.includes(
                permissions?.syllabus ? permissions?.syllabus : 0
              )
            }
          >
            <Pencil className="mr-2 h-4 w-4" /> Edit syllabus
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => mutate()}>
            <Copy className="mr-2 h-4 w-4" />
            Duplicate syllabus
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </>
  )
}
