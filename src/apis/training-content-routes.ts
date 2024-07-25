import { useMutation, useQueryClient } from "@tanstack/react-query"

import famsAPI from "@/lib/fams-api"

export const useDeleteTrainingContent = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (contentId: string) => {
      const { data } = await famsAPI.delete(
        `/Training/DeleteContentByContentId/${contentId}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}

export const useAddNewTrainingContent = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (values: {
      content: string
      code: string
      deliveryType: string
      duration: number
      trainingFormat: string
      note: string
      unitCode: string
    }) => {
      const { data } = await famsAPI.post(
        `/Training/AddNewContent?unitCode=${values.unitCode}&content=${values.content}&code=${values.code}&duration=${values.duration}&deliveryType=${values.deliveryType}&trainingFormat=${values.trainingFormat}&note=abc`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}
