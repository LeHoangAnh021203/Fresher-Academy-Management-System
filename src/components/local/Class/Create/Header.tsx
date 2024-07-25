import { Album, Hand, RadioTower, SpellCheck } from "lucide-react"
import { MdOutlineRecordVoiceOver } from "react-icons/md"

import { IClass } from "@/types/class.interface"

import Chip from "@/components/global/atoms/chip"

function Header({ className, totalDays, totalLearningTime }: IClass) {
  return (
    <div className="w-full border-t-[1px] border-white">
      <div className="bg-primary p-5 text-2xl tracking-[4.8px] text-white">
        Class
      </div>
      <div className="bg-primary p-5 pt-2 text-white">
        <div className="w-full border-b-[1px] border-red-100 pb-2">
          <div className="flex items-center gap-4">
            <h2 className="text-3xl font-bold tracking-[6.4px] md:text-4xl">
              {className}
            </h2>
            <Chip content="Planning" color="#B9B9B9" />
          </div>
        </div>
        <div className="mt-4 flex items-center">
          <div className="mr-4 w-fit border-r-[1px] border-red-100 pr-8">
            <p className="text-2xl font-bold">
              <span className="text-xl font-semibold leading-9 tracking-[4.8px] md:text-2xl">
                {totalDays ? totalDays : 0} days
                <span className="text-sm font-normal leading-6 -tracking-normal md:text-base">
                  ({totalLearningTime ? totalLearningTime : 0} hours)
                </span>
              </span>
            </p>
          </div>
          <div className="flex cursor-pointer gap-[15px] text-[#DFDEDE]/50">
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
