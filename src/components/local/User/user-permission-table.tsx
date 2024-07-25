import { useState } from "react"

import { useAuthContext } from "@/contexts/auth-provider"
import { Eye, EyeOff, Pencil, PlusCircle, Star } from "lucide-react"
import { useForm } from "react-hook-form"
import { toast } from "sonner"
import * as z from "zod"

import { useGetRoleConfig } from "@/apis/user-permission-routes"

import famsAPI from "@/lib/fams-api"
import { UserRoleConfigSchema } from "@/lib/schemas/user-permission"
import { cn } from "@/lib/utils"

import { Button } from "@/components/global/atoms/button"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"
import { Skeleton } from "@/components/global/atoms/skeleton"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from "@/components/global/atoms/table"

import { PermissionIcon } from "./permission-icon"

export const UserPermissionTable = () => {
  const [update, setUpdate] = useState(false)
  const { data: roleConfigs, isLoading } = useGetRoleConfig()
  const { setPermissions, role } = useAuthContext()

  const form = useForm<z.infer<typeof UserRoleConfigSchema>>({
    values: roleConfigs
  })

  const onSubmit = async (value: z.infer<typeof UserRoleConfigSchema>) => {
    const body = value.map((item) => {
      const {
        permissionId,
        permissionName,
        syllabus,
        learningMaterial,
        trainingProgram,
        userManagement
      } = item

      return {
        permissionId,
        permissionName,
        syllabus,
        learningMaterial,
        trainingProgram,
        userManagement,
        class: item.class
      }
    })

    famsAPI
      .put(`/UserPermission/UpdatePermission`, body)
      .then(() => {
        setUpdate(false)
        const foundPermission = value.find((item) => item.permissionId === role)
        if (foundPermission) setPermissions(foundPermission)
        toast.success("save success!!!")
      })
      .catch(() => toast.error("save failed"))
  }

  if (isLoading) {
    return (
      <div className="mx-4 overflow-auto rounded-t-[10px] border">
        <Table>
          <TableHeader className="bg-primary">
            <TableRow className="hover:bg-primary">
              <TableHead className="pl-4 text-white ">Role name</TableHead>
              <TableHead className="pl-4 text-white">Syllabus</TableHead>
              <TableHead className="pl-4 text-white">
                Training program
              </TableHead>
              <TableHead className="pl-4 text-white">Class</TableHead>
              <TableHead className="pl-4 text-white">
                Learning material
              </TableHead>
              <TableHead className="pl-4 text-white">User</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {Array.from({ length: 2 }).map((_, i) => (
              <TableRow key={i}>
                {Array.from({ length: 6 }).map((_, i) => (
                  <TableCell className="px-2" key={i}>
                    <Skeleton className="h-8 w-full" />
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    )
  }
  return (
    <div className="space-y-4">
      {!update && (
        <div className="mx-4 flex justify-end">
          <Button onClick={() => setUpdate(true)}>Update permission</Button>
        </div>
      )}
      {update && (
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <div className="mx-4 overflow-auto rounded-t-[10px] border">
              <Table>
                <TableHeader className="bg-primary">
                  <TableRow className="hover:bg-primary">
                    <TableHead className="pl-4 text-white ">
                      Role name
                    </TableHead>
                    <TableHead className="pl-4 text-white">Syllabus</TableHead>
                    <TableHead className="pl-4 text-white">
                      Training program
                    </TableHead>
                    <TableHead className="pl-4 text-white">Class</TableHead>
                    <TableHead className="pl-4 text-white">
                      Learning material
                    </TableHead>
                    <TableHead className="pl-4 text-white">User</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {roleConfigs?.map((roleConfig, index) => (
                    <TableRow
                      className={cn(
                        "",
                        roleConfig.permissionName === "Super Admin" && "hidden"
                      )}
                    >
                      <TableCell>{roleConfig.permissionName}</TableCell>
                      <TableCell>
                        <FormField
                          control={form.control}
                          name={`${index}.syllabus`}
                          render={({ field }) => (
                            <FormItem>
                              <FormControl>
                                <Select
                                  onValueChange={(value) =>
                                    field.onChange(parseInt(value))
                                  }
                                  defaultValue={field?.value?.toString()}
                                >
                                  <SelectTrigger className="min-w-[200px] max-w-[200px] ">
                                    <SelectValue placeholder="Permission" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="0">
                                      <div className="flex items-center">
                                        <EyeOff className="mr-2 h-4 w-4" />
                                        Access denied
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="1">
                                      <div className="flex items-center">
                                        <Eye className="mr-2 h-4 w-4" />
                                        View
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="2">
                                      <div className="flex items-center">
                                        <Pencil className="mr-2 h-4 w-4" />
                                        Modify
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="3">
                                      <div className="flex items-center">
                                        <PlusCircle className="mr-2 h-4 w-4" />
                                        Create
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="4">
                                      <div className="flex items-center">
                                        <Star className="mr-2 h-4 w-4" /> Full
                                        access
                                      </div>
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </TableCell>
                      <TableCell>
                        <FormField
                          control={form.control}
                          name={`${index}.trainingProgram`}
                          render={({ field }) => (
                            <FormItem>
                              <FormControl>
                                <Select
                                  onValueChange={(value) =>
                                    field.onChange(parseInt(value))
                                  }
                                  defaultValue={field?.value?.toString()}
                                >
                                  <SelectTrigger className="min-w-[200px] max-w-[200px] ">
                                    <SelectValue placeholder="Permission" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="0">
                                      <div className="flex items-center">
                                        <EyeOff className="mr-2 h-4 w-4" />
                                        Access denied
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="1">
                                      <div className="flex items-center">
                                        <Eye className="mr-2 h-4 w-4" />
                                        View
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="2">
                                      <div className="flex items-center">
                                        <Pencil className="mr-2 h-4 w-4" />
                                        Modify
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="3">
                                      <div className="flex items-center">
                                        <PlusCircle className="mr-2 h-4 w-4" />
                                        Create
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="4">
                                      <div className="flex items-center">
                                        <Star className="mr-2 h-4 w-4" /> Full
                                        access
                                      </div>
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                              </FormControl>
                            </FormItem>
                          )}
                        />
                      </TableCell>
                      <TableCell>
                        <FormField
                          control={form.control}
                          name={`${index}.class`}
                          render={({ field }) => (
                            <FormItem>
                              <FormControl>
                                <Select
                                  onValueChange={(value) =>
                                    field.onChange(parseInt(value))
                                  }
                                  defaultValue={field?.value?.toString()}
                                >
                                  <SelectTrigger className="min-w-[200px] max-w-[200px] ">
                                    <SelectValue placeholder="Permission" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="0">
                                      <div className="flex items-center">
                                        <EyeOff className="mr-2 h-4 w-4" />
                                        Access denied
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="1">
                                      <div className="flex items-center">
                                        <Eye className="mr-2 h-4 w-4" />
                                        View
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="2">
                                      <div className="flex items-center">
                                        <Pencil className="mr-2 h-4 w-4" />
                                        Modify
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="3">
                                      <div className="flex items-center">
                                        <PlusCircle className="mr-2 h-4 w-4" />
                                        Create
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="4">
                                      <div className="flex items-center">
                                        <Star className="mr-2 h-4 w-4" /> Full
                                        access
                                      </div>
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                              </FormControl>
                            </FormItem>
                          )}
                        />
                      </TableCell>
                      <TableCell>
                        <FormField
                          control={form.control}
                          name={`${index}.learningMaterial`}
                          render={({ field }) => (
                            <FormItem>
                              <FormControl>
                                <Select
                                  onValueChange={(value) =>
                                    field.onChange(parseInt(value))
                                  }
                                  defaultValue={field?.value?.toString()}
                                >
                                  <SelectTrigger className="min-w-[200px] max-w-[200px] ">
                                    <SelectValue placeholder="Permission" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="0">
                                      <div className="flex items-center">
                                        <EyeOff className="mr-2 h-4 w-4" />
                                        Access denied
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="1">
                                      <div className="flex items-center">
                                        <Eye className="mr-2 h-4 w-4" />
                                        View
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="2">
                                      <div className="flex items-center">
                                        <Pencil className="mr-2 h-4 w-4" />
                                        Modify
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="3">
                                      <div className="flex items-center">
                                        <PlusCircle className="mr-2 h-4 w-4" />
                                        Create
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="4">
                                      <div className="flex items-center">
                                        <Star className="mr-2 h-4 w-4" /> Full
                                        access
                                      </div>
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                              </FormControl>
                            </FormItem>
                          )}
                        />
                      </TableCell>
                      <TableCell>
                        <FormField
                          control={form.control}
                          name={`${index}.userManagement`}
                          render={({ field }) => (
                            <FormItem>
                              <FormControl>
                                <Select
                                  onValueChange={(value) =>
                                    field.onChange(parseInt(value))
                                  }
                                  defaultValue={field?.value?.toString()}
                                >
                                  <SelectTrigger className="min-w-[200px] max-w-[200px] ">
                                    <SelectValue placeholder="Permission" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="0">
                                      <div className="flex items-center">
                                        <EyeOff className="mr-2 h-4 w-4" />
                                        Access denied
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="1">
                                      <div className="flex items-center">
                                        <Eye className="mr-2 h-4 w-4" />
                                        View
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="2">
                                      <div className="flex items-center">
                                        <Pencil className="mr-2 h-4 w-4" />
                                        Modify
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="3">
                                      <div className="flex items-center">
                                        <PlusCircle className="mr-2 h-4 w-4" />
                                        Create
                                      </div>
                                    </SelectItem>
                                    <SelectItem value="4">
                                      <div className="flex items-center">
                                        <Star className="mr-2 h-4 w-4" /> Full
                                        access
                                      </div>
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                              </FormControl>
                            </FormItem>
                          )}
                        />
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </div>
            {update && (
              <div className="mx-4 flex items-center justify-end">
                <Button
                  type="button"
                  variant="link"
                  onClick={() => setUpdate(false)}
                >
                  Cancel
                </Button>
                <Button type="submit">Save</Button>
              </div>
            )}
          </form>
        </Form>
      )}

      {!update && (
        <div className="mx-4 overflow-auto rounded-t-[10px] border">
          <Table>
            <TableHeader className="bg-primary">
              <TableRow className="hover:bg-primary">
                <TableHead className="pl-4 text-white ">Role name</TableHead>
                <TableHead className="pl-4 text-white">Syllabus</TableHead>
                <TableHead className="pl-4 text-white">
                  Training program
                </TableHead>
                <TableHead className="pl-4 text-white">Class</TableHead>
                <TableHead className="pl-4 text-white">
                  Learning material
                </TableHead>
                <TableHead className="pl-4 text-white">User</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {roleConfigs?.map((roleConfig) => (
                <TableRow
                  className={cn(
                    "",
                    roleConfig.permissionName === "Super Admin" && "hidden"
                  )}
                >
                  <TableCell>{roleConfig.permissionName}</TableCell>
                  <TableCell>
                    <PermissionIcon permission={roleConfig.syllabus} />
                  </TableCell>
                  <TableCell>
                    <PermissionIcon permission={roleConfig.trainingProgram} />
                  </TableCell>
                  <TableCell>
                    <PermissionIcon permission={roleConfig.class} />
                  </TableCell>
                  <TableCell>
                    <PermissionIcon permission={roleConfig.learningMaterial} />
                  </TableCell>
                  <TableCell>
                    <PermissionIcon permission={roleConfig.userManagement} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      )}
    </div>
  )
}
