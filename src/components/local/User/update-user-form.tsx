import { useState } from "react"

import { zodResolver } from "@hookform/resolvers/zod"
import { Eye, EyeOff } from "lucide-react"
import { useForm } from "react-hook-form"
import { Link } from "react-router-dom"
import * as z from "zod"

import {
  IUser,
  IUserGender,
  IUserRole,
  IUserStatus
} from "@/types/user.interface"

import { useUpdateUser } from "@/apis/user-routes"

import { UserSchema } from "@/lib/schemas/user"

import { Button } from "@/components/global/atoms/button"
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

interface Props {
  initData: IUser
}

export const UserForm = ({ initData }: Props) => {
  const [showPassword, setShowPassword] = useState(false)
  const { isPending, mutateAsync } = useUpdateUser()

  const form = useForm<z.infer<typeof UserSchema>>({
    resolver: zodResolver(UserSchema),
    defaultValues: {
      permissionId: IUserRole[initData.permissionId],
      name: initData.name,
      email: initData.email,
      dob: new Date(initData.dob),
      gender: IUserGender[initData.gender],
      status: IUserStatus[initData.status],
      phone: initData.phoneNum,
      password: "",
    }
  })

  const onSubmit = async (data: z.infer<typeof UserSchema>) => {
    await mutateAsync({ data: data, userId: initData.userId })
    console.log(data)
  }

  if (isPending) {
    return <Loader />
  }

  return (
    <div>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="flex w-[50%] flex-col gap-5 bg-white px-8 py-6"
        >
          <FormField
            control={form.control}
            name="permissionId"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">User type</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Select value={field.value} onValueChange={field.onChange}>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Select one" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value={IUserRole[1]}>
                          Super Admin
                        </SelectItem>
                        <SelectItem value={IUserRole[2]}>
                          Class Admin
                        </SelectItem>
                        <SelectItem value={IUserRole[3]}>Trainer</SelectItem>
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
                <span className="w-[30%] font-semibold">Email address *</span>
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
            name="password"
            render={({ field }) => (
              <FormItem className="flex relative w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Password</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Input
                      type={showPassword ? "text" : "password"}
                      placeholder="Change password"
                      {...field}
                      className="w-full"
                    />
                  </FormControl>
                  <FormMessage />
                </div>
                <button
                  type="button"
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute right-2 top-2"
                >
                  {showPassword ? <EyeOff /> : <Eye />}
                </button>
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="dob"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Date of birth</span>
                <div className="w-[70%]">
                  <FormControl>
                    <DatePicker
                      initialDate={field.value as Date}
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
                          <RadioGroupItem value={IUserGender[1]} id="male" />
                          <Label htmlFor="male">Male</Label>
                        </div>
                        <div className="flex items-center space-x-2">
                          <RadioGroupItem value={IUserGender[2]} id="female" />
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
                        {field.value === IUserStatus[0] ? "Active" : "Inactive"}
                      </Label>
                    </div>
                  </FormControl>
                  <FormMessage />
                </div>
              </FormItem>
            )}
          />
          <div className="flex justify-end space-x-4">
            <Link to="/users">
              <Button type="button" variant={"ghost"}>
                Cancel
              </Button>
            </Link>
            <Button type="submit">Save</Button>
          </div>
        </form>
      </Form>
    </div>
  )
}
