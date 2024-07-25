import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"

import {
  IDeliveryType,
  ITrainingContent,
  ITrainingFormat
} from "@/types/syllabus.interface"

import { TrainingContentCreateSchema } from "@/lib/schemas/training-content"

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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"
import { Switch } from "@/components/global/atoms/switch"

interface CreateSyllabusFormProps {
  handleCreateContent: (content: ITrainingContent) => void
}

const CreateSyllabusForm = ({
  handleCreateContent
}: CreateSyllabusFormProps) => {
  const form = useForm<z.infer<typeof TrainingContentCreateSchema>>({
    resolver: zodResolver(TrainingContentCreateSchema),
    defaultValues: {
      content: "",
      duration: "",
      deliveryType: IDeliveryType.AssignmentLab,
      trainingFormat: ITrainingFormat.Online
    }
  })

  const deliveryType = [
    "Assignment/Lab",
    "Concept/Lecture",
    "Guide/Review",
    "Test/Quiz",
    "Exam",
    "Seminar/Workshop"
  ]

  const onSubmit = async (
    data: z.infer<typeof TrainingContentCreateSchema>
  ) => {
    //@ts-expect-error type error
    handleCreateContent({
      content: data.content,
      deliveryType: data.deliveryType,
      trainingFormat: data.trainingFormat,
      duration: parseInt(data.duration),
      code: data.code,
      note: "This is note",
      unitCode: "Unit 1"
    })
  }
  return (
    <>
      <div className=" bg-primary py-2 text-center font-semibold text-white">
        Create new content
      </div>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="flex w-full flex-col gap-5 bg-white px-8 py-6"
        >
          {/* Content */}
          <FormField
            control={form.control}
            name="content"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Name</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Input
                      placeholder="Name of content"
                      {...field}
                      className="w-full"
                    />
                  </FormControl>
                  <FormMessage />
                </div>
              </FormItem>
            )}
          />

          {/* Output standart */}
          <FormField
            control={form.control}
            name="code"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Delivery type</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Select value={field.value} onValueChange={field.onChange}>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Select one" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="H5SD">H5SD | SQL skills</SelectItem>
                        <SelectItem value="K4SD">K4SD | RDBMS</SelectItem>
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
            name="duration"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Training time</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Input
                      placeholder="Minutes"
                      type="number"
                      min="1"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </div>
              </FormItem>
            )}
          />

          {/* Delivery Type */}
          <FormField
            control={form.control}
            name="deliveryType"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Delivery type</span>
                <div className="w-[70%]">
                  <FormControl>
                    <Select value={field.value} onValueChange={field.onChange}>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Select one" />
                      </SelectTrigger>
                      <SelectContent>
                        {deliveryType.map((item, key) => (
                          <SelectItem value={item} key={key}>
                            {item}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </FormControl>
                  <FormMessage />
                </div>
              </FormItem>
            )}
          />

          {/* Method */}
          <FormField
            control={form.control}
            name="trainingFormat"
            render={({ field }) => (
              <FormItem className="flex w-full items-center justify-between gap-10">
                <span className="w-[30%] font-semibold">Method</span>
                <div className="w-[70%]">
                  <FormControl>
                    <div className="flex items-center space-x-2">
                      <Switch
                        id="status"
                        onCheckedChange={(isChecked) =>
                          field.onChange(isChecked ? "Online" : "Offline")
                        }
                        checked={field.value === "Online"}
                        style={{
                          backgroundColor:
                            field.value === "Online" ? "#D45B13" : "#E5E7EB"
                        }}
                      />
                      <Label htmlFor="status">
                        {field.value === "Online" ? "Online" : "Offline"}
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
    </>
  )
}

export default CreateSyllabusForm
