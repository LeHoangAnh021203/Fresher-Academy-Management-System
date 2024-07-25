import { useQuery } from "@tanstack/react-query"

import { IClass } from "@/types/class.interface"

import famsAPI from "@/lib/fams-api"

export const useGetAllClasses = () => {
  return useQuery<IClass>({
    queryKey: ["classes"],
    queryFn: async () => {
      const { data } = await famsAPI.get("/Class/GetAllClasses")
      return data
    }
  })
}

export const useGetClassById = (classID) => {
  return useQuery<IClass>({
    queryKey: ["class", classID],
    queryFn: async () => {
      const { data } = await famsAPI.get(`/Class/GetClassById/${classID}`)
      return data
    },
    enabled: !!classID
  })
}
