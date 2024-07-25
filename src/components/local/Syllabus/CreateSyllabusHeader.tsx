import {
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import { Input } from "@/components/global/atoms/input"

interface CreateSyllabusHeaderProps {
  form: any
}

const CreateSyllabusHeader = ({ form }: CreateSyllabusHeaderProps) => {
  const items = [
    {
      title: "",
      type: "input",
      id: "topicName",
      placeholder: "Syllabus Name*",
      name: "topicName"
    },
    {
      title: "Code",
      type: "input",
      value: "NPL",
      id: "topicCode",
      name: "topicCode",
      placeholder: "Syllabus Code*"
    },
    { title: "Version", type: "text", value: "1.0" }
  ]

  return (
    <div className="flex gap-20 py-4">
      <div className="flex items-center gap-4">
        <span className="whitespace-nowrap font-semibold">Syllabus Name*</span>
        <FormField
          control={form.control}
          name="topicName"
          render={({ field }) => (
            <FormItem className="flex items-center gap-10 space-y-0">
              <FormControl>
                <Input
                  placeholder="Syllabus Name*"
                  {...field}
                  className="m-0 w-[300px]"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </div>

      <div className="flex items-center gap-4">
        <span className="whitespace-nowrap font-semibold">Code</span>
        <FormField
          control={form.control}
          name="topicCode"
          render={({ field }) => (
            <FormItem className="flex items-center gap-10 space-y-0">
              <FormControl>
                <Input
                  placeholder="Code*"
                  {...field}
                  className="m-0 w-[100px]"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </div>

      <div className="flex items-center gap-4">
        <span className="whitespace-nowrap font-semibold">Version</span>
        <p>1.0</p>
      </div>
    </div>
  )
}

export default CreateSyllabusHeader
