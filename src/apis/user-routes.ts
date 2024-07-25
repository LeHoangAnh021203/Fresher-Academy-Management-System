import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import { format } from "date-fns"
import { useNavigate } from "react-router-dom"
import { toast } from "sonner"
import { z } from "zod"

import {
  IUser,
  IUserGender,
  IUserRole,
  IUserStatus
} from "@/types/user.interface"

import famsAPI from "@/lib/fams-api"
import { UserSchema } from "@/lib/schemas/user"

interface propRole {
  userId: string
  userRole: IUserRole
}

interface propUpdateUser {
  data: z.infer<typeof UserSchema>
  userId: string
}

export const useGetAllUser = () => {
  return useQuery<IUser[]>({
    queryKey: ["users"],
    queryFn: async () => {
      const { data } = await famsAPI.get<IUser[]>("/User/GetAllUser")
      return data
    }
  })
}

export const useGetUserById = (userId: string | undefined) => {
  return useQuery<IUser | undefined>({
    queryKey: ["user"],
    queryFn: async () => {
      const { data } = await famsAPI.get<IUser[]>("/User/GetAllUser")
      return data!.find((user: IUser) => user.userId === userId)
    }
  })
}

export const useUpdateRole = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (props: propRole) => {
      const res = await famsAPI.put("/User/GrantPermission", {
        userId: props.userId,
        permissionId: props.userRole
      })
      return res.data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["users"] })
    }
  })
}

export const useUpdateStatus = (userId: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async () => {
      const { data } = await famsAPI.put(`/User/UpdateUserStatus/${userId}`)
      return data
    },
    onSuccess: () => {
      toast.success("Update status successfully!")
      queryClient.refetchQueries({ queryKey: ["users"] })
      queryClient.refetchQueries({ queryKey: ["user"] })
    },
    onError: () => {
      toast.error("Error updating status")
    }
  })
}

export const useDeleteUser = (userId: string) => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async () => {
      const { data } = await famsAPI.put(`/User/DeleteUser/${userId}`)
      return data
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["users"] })
      queryClient.refetchQueries({ queryKey: ["user"] })
      toast.success("Delete user successfully!")
    },
    onError: () => {
      toast.error("Error delete user")
    }
  })
}

export const useAddNewUser = () => {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async (data: z.infer<typeof UserSchema>) => {
      const permissionIdUser =
        data.permissionId === IUserRole[1]
          ? 1
          : data.permissionId === IUserRole[2]
            ? 2
            : 3
      const genderUser =
        data.gender === IUserGender[1]
          ? 1
          : data.gender === IUserGender[2]
            ? 2
            : 0
      const statusUser =
        data.status === IUserStatus[0]
          ? 0
          : data.status === IUserStatus[1]
            ? 1
            : 2
      const dobUser = format(new Date(data.dob!), "yyyy-MM-dd")
      try {
        const res = await famsAPI.post(
          `/User/AddNewUser?name=${data.name}&email=${data.email}&genders=${genderUser}&phone=${data.phone}&dob=${dobUser}&status=${statusUser}&permissionId=${permissionIdUser}`
        )
        return res.data
      } catch (error) {
        console.error(error)
      }
    },
    onSuccess: () => {
      queryClient.refetchQueries({ queryKey: ["users"] })
      toast.success("Add new user successfully")
    },
    onError: () => {
      toast.error("Failed to add new user")
    }
  })
}

export const useUpdateUser = () => {
  const queryClient = useQueryClient()
  const navigate = useNavigate()
  return useMutation({
    mutationFn: async (props: propUpdateUser) => {
      const formData = {
        ...props.data,
        dob: format(new Date(props.data.dob!), "yyyy-MM-dd"),
        permissionId:
          props.data.permissionId === IUserRole[1]
            ? 1
            : props.data.permissionId === IUserRole[2]
              ? 2
              : 3,
        gender:
          props.data.gender === IUserGender[1]
            ? 1
            : props.data.gender === IUserGender[2]
              ? 2
              : 0,
        status:
          props.data.status === IUserStatus[0]
            ? 0
            : props.data.status === IUserStatus[1]
              ? 1
              : 2,
        password: props.data.password,
        userId: props.userId
      }
      try {
        console.log(formData)
        const res = await famsAPI.put("/User/UpdateUser", formData)
        return res.data
      } catch (error) {
        console.error(error)
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["user"] })
      queryClient.resetQueries({ queryKey: ["users"] })
      toast.success("User updated successfully")
      navigate("/users")
    },
    onError: () => {
      toast.error("Failed to update user")
    }
  })
}
