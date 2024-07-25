import { IUserStatus } from "@/types/user.interface"

import { useGetAllUser } from "@/apis/user-routes"

import { cn } from "@/lib/utils"

import {
  Avatar,
  AvatarFallback,
  AvatarImage
} from "@/components/global/atoms/avatar"
import { Skeleton } from "@/components/global/atoms/skeleton"

export function RecentUsers() {
  const { isLoading, data } = useGetAllUser()

  if (isLoading) {
    return (
      <div className="space-y-8">
        {Array.from({ length: 7 }).map((_, i) => (
          <div className="flex items-center" key={i}>
            <Skeleton className="h-9 w-9 rounded-full" />
            <div className="ml-4 space-y-1">
              <Skeleton className="h-3 w-20" />
              <Skeleton className="h-3 w-32" />
            </div>
            <div className="ml-auto mr-4">
              <Skeleton className="h-6 w-20" />
            </div>
          </div>
        ))}
      </div>
    )
  }

  return (
    <div className={cn("space-y-8 ", data?.length! > 4 && "pr-4")}>
      {data?.map((user) => (
        <div className="flex items-center" key={user.userId}>
          <Avatar className="h-9 w-9">
            {user.gender === 2 ? (
              <AvatarImage src="/avatars/user1.png" alt="Avatar" />
            ) : (
              <AvatarImage src="/avatars/user2.png" alt="Avatar" />
            )}
            <AvatarFallback>AVA</AvatarFallback>
          </Avatar>
          <div className="ml-4 space-y-1">
            <p className="text-sm font-medium leading-none">{user.name}</p>
            <p className="text-sm text-muted-foreground">{user.email}</p>
          </div>
          <div className="ml-auto">
            {/* Display status text based on user.status */}
            <span
              className={cn(
                "inline-flex select-none cursor-pointer items-center rounded-[50px] px-4 py-[6px] text-xs text-white mr-2",
                user.status === IUserStatus.Active
                  ? "bg-[#4DB848] text-white"
                  : user.status === IUserStatus.Inactive
                    ? "bg-[#D45B13] text-white"
                    : user.status === IUserStatus.Suspended
                      ? "bg-[#2D3748] text-white"
                      : ""
              )}
            >
              {user.status === IUserStatus.Active
                ? "Active"
                : user.status === IUserStatus.Inactive
                  ? "Deactive"
                  : user.status === IUserStatus.Suspended
                    ? "Suspended"
                    : ""}
            </span>
          </div>
        </div>
      ))}
    </div>
  )
}
