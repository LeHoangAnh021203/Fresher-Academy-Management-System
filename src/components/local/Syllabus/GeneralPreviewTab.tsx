import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { Focus, Settings } from "lucide-react"

import { cn } from "@/lib/utils"

import {
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import { ScrollArea } from "@/components/global/atoms/scroll-area"
import { Separator } from "@/components/global/atoms/separator"
import LiteQuill from "@/components/global/molecules/LiteQuillEditor"
import Quill from "@/components/global/molecules/QuillEditor"
import { QuillEditorPreview } from "@/components/global/molecules/QuillEditorPreview"

export const GeneralPreviewTab = ({ form }: { form: any }) => {
  const { isEdit } = useSyllabusDetailContext()
  return (
    <>
      <div className="flex flex-col w-full ">
        <div className={cn("flex items-center", isEdit && "mb-4")}>
          <Settings className="mr-2 h-5 w-5" />
          <span className="text-sm font-semibold">
            Technical Requirement(s)
          </span>
        </div>
        {isEdit ? (
          <FormField
            control={form.control}
            name="technicalRequirement"
            render={({ field }) => (
              <FormItem className="flex w-full flex-col">
                <FormControl>
                  <div className="w-full">
                    <LiteQuill field={field} />
                  </div>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        ) : (
          <ScrollArea className="h-[200px] mb-4">
            <QuillEditorPreview
              value={form.getValues("technicalRequirement" as string)}
            />
          </ScrollArea>
        )}
      </div>
      <Separator className={cn(isEdit && "mt-7")} />
      <div className="mt-7">
        <h1 className={cn("flex items-center text-xl", isEdit && "mb-4")}>
          <Focus className="mr-2 h-5 w-5" />
          <span className="text-sm font-semibold">Course objectives</span>
        </h1>
        {isEdit ? (
          <FormField
            control={form.control}
            name="courseObjective"
            render={({ field }) => (
              <FormItem className="flex w-full flex-col">
                <FormControl>
                  <div className="w-full">
                    <Quill field={field} />
                  </div>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        ) : (
          <ScrollArea className="h-[200px]">
            <QuillEditorPreview
              value={form.getValues("courseObjective" as string)}
            />
          </ScrollArea>
        )}
      </div>
    </>
  )
}
