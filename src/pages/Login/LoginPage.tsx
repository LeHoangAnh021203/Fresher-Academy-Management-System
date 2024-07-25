import { useState } from "react"

import { useAuthContext } from "@/contexts/auth-provider"
import { zodResolver } from "@hookform/resolvers/zod"
import axios from "axios"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import { MoonLoader } from "react-spinners"
import { toast } from "sonner"
import * as z from "zod"

import { IAuthResponseData } from "@/types/auth.interface"

import { LoginSchema } from "@/lib/schemas/login"

import { Button } from "@/components/global/atoms/button"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import { Label } from "@/components/global/atoms/label"

const LoginPage = () => {
  const { setAccessToken, setRefreshToken, setExpiredAt } = useAuthContext()
  const [isLoading, setIsLoading] = useState(false)
  const navigate = useNavigate()
  const form = useForm<z.infer<typeof LoginSchema>>({
    resolver: zodResolver(LoginSchema),
    defaultValues: {
      email: "",
      password: ""
    }
  })

  const onSubmit = async (values: z.infer<typeof LoginSchema>) => {
    try {
      setIsLoading(true)
      const { data }: { data: IAuthResponseData } = await axios.post(
        `http://localhost:5141/Authorize/Login?email=${values.email}&password=${values.password}`
      )
      setRefreshToken(data.refreshToken, data.expiredAt)
      setAccessToken(data.accessTokenToken, data.expiredAt)
      setExpiredAt(data.expiredAt, data.expiredAt)
      toast.success("Login successfully")
      navigate("/")
      return data
    } catch (error) {
      toast.error("Email or password is incorrect")
      console.log(error)
    } finally {
      setIsLoading(false)
    }
  }
  return (
    <section className="flex h-full w-full flex-wrap">
      <div className="flex w-full items-center justify-center  p-8 lg:w-[50%] ">
        <div className="flex h-auto w-[50%] flex-col space-y-6">
          <div className="mb-4 flex w-full items-center justify-center">
            <img
              src="/Fpt_Logo.svg"
              alt="Logo"
              className="cursor-pointer w-[120px]"
            />
          </div>
          <div>
            <h1 className="font-['SF Pro Rounded'] tracking-wider select-none text-center text-3xl font-semibold">
              Fresher Academy Management System
            </h1>
          </div>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)}>
              <div className="space-y-4">
                <FormField
                  name="email"
                  control={form.control}
                  render={({ field }) => (
                    <FormItem>
                      <Label className="ml-2">Email</Label>
                      <FormControl>
                        <input
                          type="email"
                          className="h-12 w-full rounded-xl border border-gray-300 bg-slate-50 px-4 focus:border-slate-400 focus:outline-none"
                          placeholder="Example@email.com"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  name="password"
                  control={form.control}
                  render={({ field }) => (
                    <FormItem>
                      <Label className="ml-2">Password</Label>
                      <FormControl>
                        <input
                          type="password"
                          className=" h-12 w-full rounded-xl border border-gray-300 bg-slate-50 px-4 focus:border-slate-400 focus:outline-none"
                          placeholder="&#9679;&#9679;&#9679;&#9679;&#9679;&#9679;"
                          {...field}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <Button
                disabled={isLoading}
                size="lg"
                variant={"primary"}
                type="submit"
                className="mt-12 w-full text-lg rounded-xl h-12"
              >
                {isLoading ? (
                  <div className="mr-4 flex items-center ">
                    <MoonLoader color="#fff" size={15} className="mr-2" /> Sign
                    In
                  </div>
                ) : (
                  "Sign In"
                )}
              </Button>
            </form>
          </Form>
        </div>
      </div>
      <div className="w-[50%] p-8">
        <img
          className="hidden h-full w-full  rounded-3xl object-cover shadow-md lg:block"
          src="/bg-1.jpg"
        />
      </div>
    </section>
  )
}

export default LoginPage
