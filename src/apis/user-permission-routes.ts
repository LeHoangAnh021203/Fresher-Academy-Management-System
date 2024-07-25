import { useQuery } from "@tanstack/react-query"

import { IUserRoleConfig } from "@/types/user-permission.interface"

import famsAPI from "@/lib/fams-api"

export const useGetRoleConfig = () => {
  return useQuery<IUserRoleConfig>({
    queryKey: ["user-permissions"],
    queryFn: async () => {
      const { data } = await famsAPI.get("/UserPermission/GetAllPermission")
      return data
    }
  })
}
