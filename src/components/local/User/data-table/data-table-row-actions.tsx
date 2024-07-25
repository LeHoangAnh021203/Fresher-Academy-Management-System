import { useState } from "react"

import { MODIFY_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { DotsHorizontalIcon } from "@radix-ui/react-icons"
import { Row } from "@tanstack/react-table"
import { Eye, EyeOff, Pencil, Trash, User } from "lucide-react"
import { Link } from "react-router-dom"
import { toast } from "sonner"

import { IUserRole, IUserStatus } from "@/types/user.interface"

import {
  useDeleteUser,
  useUpdateRole,
  useUpdateStatus
} from "@/apis/user-routes"

import { Button } from "@/components/global/atoms/button"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuPortal,
  DropdownMenuSeparator,
  DropdownMenuSub,
  DropdownMenuSubContent,
  DropdownMenuSubTrigger,
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
  const [isDropdownOpen, setIsDropdownOpen] = useState(false)
  const status = row.getValue("status") as number
  const userId = row.getValue("userId") as string
  const { mutateAsync } = useUpdateRole()
  const mutationUpdateStatus = useUpdateStatus(userId)
  const mutationDeleteUser = useDeleteUser(userId)

  const handleUpdateRole = async (userRole: IUserRole) => {
    try {
      await mutateAsync({
        userRole: userRole,
        userId: userId
      })
      toast.success("Update role successfully!")
    } catch {
      toast.error("Failed to update role")
    }
  }

  return (
    <>
      <DropdownMenu open={isDropdownOpen} onOpenChange={setIsDropdownOpen}>
        <DropdownMenuTrigger
          asChild
          disabled={
            !MODIFY_PERMISSION.includes(
              permissions?.userManagement ? permissions?.userManagement : 0
            )
          }
        >
          <Button
            variant="ghost"
            className="flex h-8 w-8 p-0 data-[state=open]:bg-muted"
            onClick={() => setIsDropdownOpen(!isDropdownOpen)}
          >
            <DotsHorizontalIcon className="h-4 w-4" />
            <span className="sr-only">Open menu</span>
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end" className="w-[180px]">
          <DropdownMenuLabel>Manage</DropdownMenuLabel>
          <Link to={`/users/${row.getValue("userId")}`}>
            <DropdownMenuItem>
              <Pencil className="mr-2 h-4 w-4" /> Edit user
            </DropdownMenuItem>
          </Link>
          <DropdownMenuGroup>
            <DropdownMenuSub>
              <DropdownMenuSubTrigger>
                <User className="mr-2 h-4 w-4" /> Change role
              </DropdownMenuSubTrigger>
              <DropdownMenuPortal>
                <DropdownMenuSubContent>
                  <DropdownMenuItem
                    onClick={() => handleUpdateRole(IUserRole.SuperAdmin)}
                  >
                    Super Admin
                  </DropdownMenuItem>
                  <DropdownMenuItem
                    onClick={() => handleUpdateRole(IUserRole.ClassAdmin)}
                  >
                    Class Admin
                  </DropdownMenuItem>
                  <DropdownMenuItem
                    onClick={() => handleUpdateRole(IUserRole.Trainer)}
                  >
                    Trainer
                  </DropdownMenuItem>
                </DropdownMenuSubContent>
              </DropdownMenuPortal>
            </DropdownMenuSub>
          </DropdownMenuGroup>
          <DropdownMenuItem onClick={() => mutationUpdateStatus.mutate()}>
            {status === IUserStatus.Active ? (
              <EyeOff className="mr-2 h-4 w-4" />
            ) : (
              <Eye className="mr-2 h-4 w-4" />
            )}
            {status === IUserStatus.Active ? "De-activate" : "Activate"}
          </DropdownMenuItem>
          <DropdownMenuSeparator />
          <ConfirmModal
            onConfirm={() => {
              mutationDeleteUser.mutate()
              setIsDropdownOpen(false)
            }}
          >
            <button className="w-full flex items-center px-2 text-sm py-1">
              <Trash className="mr-2 h-4 w-4" />
              Delete
            </button>
          </ConfirmModal>
        </DropdownMenuContent>
      </DropdownMenu>
    </>
  )
}
