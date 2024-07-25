import { useMutation, useQueryClient } from "@tanstack/react-query"

import { IAssessment } from "@/types/syllabus.interface"

import famsAPI from "@/lib/fams-api"

export const useEditAssignment = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (data: IAssessment) => {
      await famsAPI.put("/Syllabus/EditAssessment", { ...data, topicCode })
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}
