import { Eye, EyeOff, Pencil, PlusCircle, Star } from "lucide-react"

import { IUserPermission } from "@/types/user-permission.interface"

interface PermissionIconProps {
  permission: IUserPermission
}

export const PermissionIcon = ({ permission }: PermissionIconProps) => {
  switch (permission) {
    case IUserPermission.AccessDenied:
      return (
        <div className="flex items-center">
          <EyeOff className="mr-2 h-4 w-4" />
          Access denied
        </div>
      )
    case IUserPermission.View:
      return (
        <div className="flex items-center">
          <Eye className="mr-2 h-4 w-4" />
          View
        </div>
      )
    case IUserPermission.Modify:
      return (
        <div className="flex items-center">
          <Pencil className="mr-2 h-4 w-4" />
          Modify
        </div>
      )
    case IUserPermission.Create:
      return (
        <div className="flex items-center">
          <PlusCircle className="mr-2 h-4 w-4" />
          Create
        </div>
      )
    case IUserPermission.FullAccess:
      return (
        <div className="flex items-center">
          <Star className="mr-2 h-4 w-4" />
          Full access
        </div>
      )
    default:
      return <div className="flex items-center">No permission</div>
  }
}
