import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { Copy, MoreHorizontal, Pencil } from "lucide-react"

import { IPublishStatus } from "@/types/syllabus.interface"

import Chip from "@/components/global/atoms/chip"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"
import {
  FormControl,
  FormField,
  FormItem
} from "@/components/global/atoms/form"

interface SyllabusHeaderProps {
  form: any
  onEdit: () => void
}

export const SyllabusHeader = ({
  form,

  onEdit
}: SyllabusHeaderProps) => {
  const { isEdit } = useSyllabusDetailContext()
  const title = form.watch("topicName")
  const status = form.watch("pulishStatus") as IPublishStatus
  const code = form.watch("topicCode")
  const version = form.watch("version")
  let chipContent = ""
  switch (status) {
    case IPublishStatus.Denied:
      chipContent = "Denied"
      break
    case IPublishStatus.Editing:
      chipContent = "Editing"
      break
    case IPublishStatus.Pending:
      chipContent = "Pending"
      break
    case IPublishStatus.Published:
      chipContent = "Published"
      break
    default:
      chipContent = ""
  }

  return (
    <div className="flex gap-[15px] border-b-[2px] border-gray-500 p-5 items-center">
      <div>
        <div className="text-2xl font-semibold leading-9 tracking-[4.80px] text-gray-700">
          Syllabus
        </div>
        <div className="inline-flex items-center space-x-4">
          {!isEdit ? (
            <h1 className="text-[32px] font-semibold leading-[48px] tracking-[6.40px] text-gray-700">
              {title}
            </h1>
          ) : (
            <FormField
              control={form.control}
              name="topicName"
              render={({ field }) => (
                <FormItem>
                  <FormControl>
                    <input
                      type="text"
                      className="w-[300px] rounded-md border-2 border-gray-300 p-2"
                      {...field}
                    />
                  </FormControl>
                </FormItem>
              )}
            />
          )}
          {<Chip content={chipContent} color="#2D3748" />}
        </div>
        <div className="mt-1 text-base font-semibold leading-normal text-gray-700">
          Code: {code} - v{version}
        </div>
      </div>
      <div className="ml-auto">
        <DropdownMenu>
          <DropdownMenuTrigger className=" rounded-full p-1  hover:bg-gray-100 hover:text-gray-800">
            <MoreHorizontal className="h-7 w-7 " />
          </DropdownMenuTrigger>
          <DropdownMenuContent className="mr-5">
            <DropdownMenuLabel>Manage</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem className="flex items-center" onClick={onEdit}>
              <Pencil className="mr-2 h-4 w-4" /> Edit syllabus
            </DropdownMenuItem>
            <DropdownMenuItem className="flex items-center">
              <Copy className="mr-2 h-4 w-4" /> Duplicate syllabus
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </div>
  )
}
