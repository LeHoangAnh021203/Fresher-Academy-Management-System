import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import {
  ITrainingProgram,
  ITrainingProgramPreview
} from "@/types/training-program.interface"

import famsAPI from "@/lib/fams-api"

export const useGetAllTrainingPrograms = () => {
  return useQuery<ITrainingProgramPreview[]>({
    queryKey: ["training-programs"],
    queryFn: async () => {
      const { data } = await famsAPI.get(
        "/TrainingProgram/GetAllTrainingProgram"
      )
      return data
    }
  })
}

export const useGetTrainingProgramById = (trainingProgramCode: string) => {
  return useQuery<ITrainingProgram>({
    queryKey: ["training-program", trainingProgramCode],
    queryFn: async () => {
      const { data } = await famsAPI.get(
        `/TrainingProgram/Admin?programCode=${trainingProgramCode}`
      )
      return data.result
    }
  })
}

export const useAddNewTrainingProgram = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (values: {
      name: string
      duration: number
      syllabuses: string[]
    }) => {
      const { data } = await famsAPI.post(
        `/TrainingProgram/AddNewTrainingProgram?name=${values.name}&duration=${values.duration}`,
        values.syllabuses
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["training-programs"] })
    }
  })
}

export const useDeleteTrainingProgram = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (trainingProgramCode: string) => {
      const { data } = await famsAPI.delete(
        `/TrainingProgram/Delete-training-program?trainingCode=${trainingProgramCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["training-programs"] })
    }
  })
}

export const useUpdateTrainingProgramStatus = (trainingProgramCode: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (trainingProgramCode: string) => {
      const { data } = await famsAPI.put(
        `/TrainingProgram/UpdateStatusTrainingProgram?trainingProgramCode=${trainingProgramCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({
        queryKey: ["training-program", trainingProgramCode]
      })
      queryClient.refetchQueries({ queryKey: ["training-programs"] })
    }
  })
}

export const useDeleteSyllabusFromTrainingProgram = (
  trainingProgramCode: string
) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (topicCode: string) => {
      const { data } = await famsAPI.delete(
        `/TrainingProgram/RemoveSyllabusFromTrainingProgram?trainingProgramCode=${trainingProgramCode}&topicCode=${topicCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({
        queryKey: ["training-program", trainingProgramCode]
      })
      queryClient.refetchQueries({ queryKey: ["training-programs"] })
    }
  })
}

export const useAddSyllabusToTrainingProgram = (
  trainingProgramCode: string
) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (topicCode: string) => {
      const { data } = await famsAPI.post(
        `/TrainingProgram/AddSyllabusToTrainingProgram?trainingProgramCode=${trainingProgramCode}&topicCode=${topicCode}`
      )
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({
        queryKey: ["training-program", trainingProgramCode]
      })
      queryClient.refetchQueries({ queryKey: ["training-programs"] })
    }
  })
}
