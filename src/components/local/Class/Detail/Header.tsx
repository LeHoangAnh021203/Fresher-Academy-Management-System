import {
  Album,
  Hand,
  MoreHorizontal,
  Pencil,
  RadioTower,
  SpellCheck
} from "lucide-react"
import { MdOutlineRecordVoiceOver } from "react-icons/md"

import { IClass, IClassStatus } from "@/types/class.interface"

import Chip from "@/components/global/atoms/chip"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from "@/components/global/atoms/dropdown-menu"

type HeaderProps = IClass & {
  onEdit: () => void
  isEditing: boolean
}

function Header({
  className,
  classCode,
  status,
  days,
  hours,
  onEdit,
  isEditing
}: HeaderProps) {
  return (
    <div className="w-full border-t-[1px] border-white">
      <div className="bg-primary p-5 text-2xl tracking-[4.8px] text-white">
        Class
      </div>
      <div className="bg-primary p-5 pt-2 text-white">
        <div className="flex justify-between">
          <div>
            <div className="w-full border-b-[1px] border-red-100 pb-2">
              <div className="flex items-center gap-4">
                <h2 className="text-4xl font-bold tracking-[6.4px]">
                  {className}
                </h2>

                <Chip content={IClassStatus[status]} color="#B9B9B9" />
              </div>
              <h3 className="pt-3 font-normal">{classCode}</h3>
            </div>
          </div>
          {!isEditing && (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <MoreHorizontal
                  size={40}
                  className="cursor-pointer"
                  role="button"
                />
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end" className="w-[180px]">
                <DropdownMenuLabel>Manage</DropdownMenuLabel>
                <DropdownMenuSeparator />
                <DropdownMenuItem onClick={onEdit}>
                  <Pencil size={16} className="mr-2" />
                  Edit class
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          )}
        </div>
        <div className="mt-4 flex items-center">
          <div className="mr-4 w-fit border-r-[1px] border-red-100 pr-8">
            <p className="text-2xl font-bold">
              <span className="text-2xl font-semibold leading-9 tracking-[4.8px]">
                {days} days
                <span className="text-base font-normal leading-6 -tracking-normal">
                  ({hours} hours)
                </span>
              </span>
            </p>
          </div>
          <div className="flex cursor-pointer gap-[15px] text-[#DFDEDE]">
            <Album size={20} />
            <MdOutlineRecordVoiceOver size={20} />
            <SpellCheck size={20} />
            <RadioTower size={20} />
            <Hand size={20} />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Header
