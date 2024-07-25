import { MODIFY_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { DotsHorizontalIcon } from "@radix-ui/react-icons"
import { Row } from "@tanstack/react-table"
import { Copy, Pencil, Trash } from "lucide-react"
import { Link } from "react-router-dom"

import { Button } from "@/components/global/atoms/button"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"

interface DataTableRowActionsProps<TData> {
  row: Row<TData>
}

export function DataTableRowActions<TData>({
  row
}: DataTableRowActionsProps<TData>) {
  const { permissions } = useAuthContext()
  return (
    <>
      <DropdownMenu>
        <DropdownMenuTrigger
          asChild
          disabled={
            !MODIFY_PERMISSION.includes(
              permissions?.class ? permissions?.class : 0
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

          <Link to={`/class/${row.original.classID}`}>
            <DropdownMenuItem>
              <Pencil className="mr-2 h-4 w-4" /> Edit class
            </DropdownMenuItem>
          </Link>
          <DropdownMenuItem>
            <Copy className="mr-2 h-4 w-4" />
            Duplicate class
          </DropdownMenuItem>

          <DropdownMenuSeparator />
          <DropdownMenuItem disabled>
            <Trash className="mr-2 h-4 w-4" /> Delete class
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </>
  )
}
