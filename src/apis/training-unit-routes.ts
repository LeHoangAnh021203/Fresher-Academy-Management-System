import { useMutation, useQueryClient } from "@tanstack/react-query"

import famsAPI from "@/lib/fams-api"

export const useAddNewTrainingUnit = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (unit: {
      unitName: string
      dayNumber: number
      topicCode: string
    }) => {
      const { data } = await famsAPI.post(
        `/Training/AddNewUnit?unitName=${unit.unitName}&day=${unit.dayNumber}&topicCode=${unit.topicCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}

export const useEditTrainingUnit = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (unit: { unitCode: string; unitName: string }) => {
      const { data } = await famsAPI.put(
        `/Training/EditUnit?unitCode=${unit.unitCode}&unitName=${unit.unitName}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}

export const useDeleteAllUnitByDay = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (dayNumber: number) => {
      const { data } = await famsAPI.delete(
        `/Training/RemoveByDay/${dayNumber}?topicCode=${topicCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}

export const useDeleteTrainingUnit = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (unitCode: string) => {
      const { data } = await famsAPI.delete(`/Training/RemoveUnit/${unitCode}`)
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}
