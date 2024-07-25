import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from "@/components/global/atoms/form"
import LiteQuill from "@/components/global/molecules/LiteQuillEditor"
import Quill from "@/components/global/molecules/QuillEditor"

interface GeneralTabProps {
  form: any
}

const GeneralTab = ({ form }: GeneralTabProps) => {
  return (
    <div className="flex w-full flex-col space-y-6">
      <FormField
        control={form.control}
        name="technicalRequirement"
        render={({ field }) => (
          <FormItem className="flex w-full flex-col">
            <FormLabel className="whitespace-nowrap font-semibold">
              Technical Requirement(s)
            </FormLabel>
            <FormControl>
              <div className="w-full">
                <LiteQuill field={field} />
              </div>
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />

      <FormField
        control={form.control}
        name="courseObjective"
        render={({ field }) => (
          <FormItem className="flex w-full flex-col">
            <FormLabel className="whitespace-nowrap font-semibold">
              Course Objective
            </FormLabel>
            <FormControl>
              <div className="w-full">
                <Quill field={field} />
              </div>
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}

export default GeneralTab
