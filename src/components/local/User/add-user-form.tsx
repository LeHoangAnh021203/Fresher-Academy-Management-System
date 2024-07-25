import { useState } from "react"

import { CREATE_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { zodResolver } from "@hookform/resolvers/zod"
import { PlusCircle } from "lucide-react"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import * as z from "zod"

import { IUserGender, IUserRole, IUserStatus } from "@/types/user.interface"

import { useAddNewUser } from "@/apis/user-routes"

import { UserSchema } from "@/lib/schemas/user"

import { Button } from "@/components/global/atoms/button"
import {
  Dialog,
  DialogContent,
  DialogTrigger
} from "@/components/global/atoms/dialog"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import { Input } from "@/components/global/atoms/input"
import { Label } from "@/components/global/atoms/label"
import { Loader } from "@/components/global/atoms/loader"
import {
  RadioGroup,
  RadioGroupItem
} from "@/components/global/atoms/radio-group"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"
import { Switch } from "@/components/global/atoms/switch"
import { DatePicker } from "@/components/local/User/dob-picker"

export function AddUser() {
  const navigate = useNavigate()
  const { permissions } = useAuthContext()
  const [isOpen, setIsOpen] = useState(false)
  const { isPending, mutateAsync } = useAddNewUser()

  const form = useForm<z.infer<typeof UserSchema>>({
    resolver: zodResolver(UserSchema),
    defaultValues: {
      permissionId: IUserRole[3],
      name: "",
      email: "",
      phone: "",
      dob: new Date(),
      gender: IUserGender[1],
      status: IUserStatus[0],
      password: ""
    }
  })

  const onSubmit = async (data: z.infer<typeof UserSchema>) => {
    setIsOpen(false)
    form.reset()
    await mutateAsync(data)
  }

  const handleClose = () => {
    isOpen === true ? setIsOpen(false) : setIsOpen(true)
    form.reset()
  }

  if (isPending) {
    return <Loader />
  }

  return (
    <div>
      <div className="flex items-center justify-between px-5">
        <div>
          <Dialog open={isOpen} onOpenChange={handleClose}>
            <DialogTrigger
              disabled={
                !CREATE_PERMISSION.includes(
                  permissions?.userManagement ? permissions?.userManagement : 0
                )
              }
            >
              <Button
                variant="primary"
                type="button"
                disabled={
                  !CREATE_PERMISSION.includes(
                    permissions?.userManagement
                      ? permissions?.userManagement
                      : 0
                  )
                }
                onClick={() => navigate("/user/new")}
              >
                <PlusCircle size={16} />
                Add User
              </Button>
            </DialogTrigger>
            <DialogContent className="w-[600px] overflow-hidden bg-transparent p-0">
              <div className=" bg-primary py-2 text-center font-semibold text-white">
                Add a new user
              </div>
              <Form {...form}>
                <form
                  onSubmit={form.handleSubmit(onSubmit)}
                  className="flex w-full flex-col gap-5 bg-white px-8 py-6"
                >
                  <FormField
                    control={form.control}
                    name="permissionId"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">User type</span>
                        <div className="w-[70%]">
                          <FormControl>
                            <Select
                              value={field.value}
                              onValueChange={field.onChange}
                            >
                              <SelectTrigger className="w-full">
                                <SelectValue placeholder="Select one" />
                              </SelectTrigger>
                              <SelectContent>
                                <SelectItem value={IUserRole[1]}>
                                  Super Admin
                                </SelectItem>
                                <SelectItem value={IUserRole[2]}>
                                  Admin
                                </SelectItem>
                                <SelectItem value={IUserRole[3]}>
                                  Trainer
                                </SelectItem>
                              </SelectContent>
                            </Select>
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">Name *</span>
                        <div className="w-[70%]">
                          <FormControl>
                            <Input
                              placeholder="User name"
                              {...field}
                              className="w-full"
                            />
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="email"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">
                          Email address *
                        </span>
                        <div className="w-[70%]">
                          <FormControl>
                            <Input
                              placeholder="Email address"
                              {...field}
                              className="w-full"
                            />
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="phone"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">Phone *</span>
                        <div className="w-[70%]">
                          <FormControl>
                            <Input
                              placeholder="Phone number"
                              {...field}
                              className="w-full"
                            />
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="dob"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">
                          Date of birth
                        </span>
                        <div className="w-[70%]">
                          <FormControl>
                            <DatePicker
                              initialDate={
                                field.value instanceof Date
                                  ? field.value
                                  : new Date()
                              }
                              onChange={field.onChange}
                            />
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="gender"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">Gender</span>
                        <div className="w-[70%]">
                          <FormControl>
                            <RadioGroup
                              value={field.value}
                              onValueChange={field.onChange}
                            >
                              <div className="flex items-center gap-2">
                                <div className="flex items-center space-x-2">
                                  <RadioGroupItem
                                    value={IUserGender[1]}
                                    id="male"
                                  />
                                  <Label htmlFor="male">Male</Label>
                                </div>
                                <div className="flex items-center space-x-2">
                                  <RadioGroupItem
                                    value={IUserGender[2]}
                                    id="female"
                                  />
                                  <Label htmlFor="female">Female</Label>
                                </div>
                              </div>
                            </RadioGroup>
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="status"
                    render={({ field }) => (
                      <FormItem className="flex w-full items-center justify-between gap-10">
                        <span className="w-[30%] font-semibold">Status</span>
                        <div className="w-[70%]">
                          <FormControl>
                            <div className="flex items-center space-x-2">
                              <Switch
                                id="status"
                                onCheckedChange={(isChecked) =>
                                  field.onChange(
                                    isChecked ? IUserStatus[0] : IUserStatus[1]
                                  )
                                }
                                checked={field.value === IUserStatus[0]}
                                style={{
                                  backgroundColor:
                                    field.value === IUserStatus[0]
                                      ? "#D45B13"
                                      : "#E5E7EB"
                                }}
                              />
                              <Label htmlFor="status">
                                {field.value === IUserStatus[0]
                                  ? "Active"
                                  : "Inactive"}
                              </Label>
                            </div>
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                  <div className="flex justify-end space-x-4">
                    <Button type="submit">Save</Button>
                  </div>
                </form>
              </Form>
            </DialogContent>
          </Dialog>
        </div>
      </div>
    </div>
  )
}
