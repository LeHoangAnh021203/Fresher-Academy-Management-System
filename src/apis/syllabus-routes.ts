import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import { ISyllabus, ISyllabusPreview } from "@/types/syllabus.interface"

import famsAPI from "@/lib/fams-api"

export const useGetAllSyllabus = () => {
  return useQuery<ISyllabusPreview[]>({
    queryKey: ["syllabuses"],
    queryFn: async () => {
      const { data } = await famsAPI.get("/Syllabus/GetAllSyllabuses")
      return data
    }
  })
}

export const useDuplicateSyllabus = (topicCode: string) => {
  return useMutation({
    mutationFn: async () => {
      const { data } = await famsAPI.post(
        `/Syllabus/DuplicateSyllabus?keyword=${topicCode}`
      )
      return data
    }
  })
}

export const useGetSyllabusById = (id: string) => {
  return useQuery<ISyllabus>({
    queryKey: ["syllabus", id],
    queryFn: async () => {
      const { data } = await famsAPI.get(
        `/Syllabus/ViewSyllabusDetail?key=${id}`
      )
      return data.result
    },
    refetchOnWindowFocus: false
  })
}

export const useCreateSyllabus = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (syllabus: ISyllabus) => {
      const { data } = await famsAPI.post(
        `/Syllabus/AddNewSyllabus?topicName=${syllabus.topicName}&technicalGroup=abc&trainingAudience=abc&topicOutline=abc&trainingMaterials=abc&trainingPrinciple=${syllabus.trainingPrinciple}&courseObjective=${syllabus.courseObjective}&technicalRequirement=${syllabus.technicalRequirement}&quizCount=${syllabus.assessment.quizCount}&quizPercent=${syllabus.assessment.quizPercent}&assignmentCount=${syllabus.assessment.assignmentCount}&assignmentPercent=${syllabus.assessment.assignmentPercent}&fe=${syllabus.assessment.finalTheoryPercent}&pe=${syllabus.assessment.finalPracticePercent}`,
        syllabus.trainingUnits
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabuses"] })
    }
  })
}

export const useImportSyllabus = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (file: File) => {
      const formData = new FormData()
      formData.append("file", file)
      const { data } = await famsAPI.post(
        "/Syllabus/Syllabus/Import",
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabuses"] })
    }
  })
}

export const useUpdateSyllabus = (topicCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (syllabus: {
      topicCode: string
      topicName: string
      technicalGroup: string
      technicalRequirement: string
      courseObjective: string
      trainingAudience: string
      topicOutline: string
      trainingMaterials: string
      trainingPrinciple: string
    }) => {
      const { data } = await famsAPI.put("/Syllabus/UpdateSyllabus", syllabus)
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["syllabus", topicCode] })
    }
  })
}
