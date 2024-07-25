import { useRef, useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import {
  ChevronDownCircle,
  MinusCircle,
  Pencil,
  PlusCircle
} from "lucide-react"
import { toast } from "sonner"

import { ITrainingContent, ITrainingUnit } from "@/types/syllabus.interface"

import { calculateTotalDurationTrainingUnit } from "@/lib/utils"

import CreateSyllabusForm from "@/components/local/Syllabus/CreateSyllabusForm"

import { Button } from "../atoms/button"
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger
} from "../atoms/collapsible"
import { Dialog, DialogContent, DialogTrigger } from "../atoms/dialog"
import { Input } from "../atoms/input"
import { TrainingContent } from "./TrainingContent"

interface TrainingUnitProps {
  unit: ITrainingUnit
  isUpdate?: boolean
  handleSetNameUnit: (unitCode: string, unitName: string) => void
  handleSetContentUnit: (unitCode: string, content: ITrainingContent) => void
  handleDeleteContentUnit: (contentId: string) => void
  handleDeleteUnit: (unitCode: string) => void
  mode: "view" | "edit" | "create"
}

export const TrainingUnit = ({
  unit,
  isUpdate,
  handleSetNameUnit,
  handleSetContentUnit,
  handleDeleteContentUnit,
  handleDeleteUnit,
  mode
}: TrainingUnitProps) => {
  const [updateName, setUpdateName] = useState(false)
  const [unitName, setUnitName] = useState(unit.unitName)
  const [openTab, setOpenTab] = useState(true)
  const [open, setOpen] = useState(false)
  const inputName = useRef<HTMLInputElement>(null)
  const { isEdit } = useSyllabusDetailContext()

  const handleSaveName = () => {
    if (inputName.current?.value) {
      const unitName = inputName.current?.value
      const unitCode = unit.unitCode
      if (handleSetNameUnit) {
        handleSetNameUnit(unitCode, unitName)
      }
      setUpdateName(false)
    }
  }

  const handleCreateContent = (content: ITrainingContent) => {
    if (handleSetContentUnit) {
      handleSetContentUnit(unit.unitCode, content)
    }
    setOpen(false)
  }
  return (
    <div className="flex w-full py-2">
      <div className="w-auto mr-4 flex h-2 items-center space-x-2">
        <p className="text-left font-bold text-nowrap">{unit.unitCode}</p>
        {(isEdit || mode === "edit" || mode === "create") && (
          <button
            className="text-red-800 hover:bg-red-100 p-1 rounded-full"
            onClick={() => {
              if (mode === "create" || mode === "edit") {
                handleDeleteUnit?.(unit.unitCode)
              }
            }}
          >
            <MinusCircle className="h-5 w-5" />
          </button>
        )}
      </div>
      <Collapsible className="w-full" open={openTab} onOpenChange={setOpenTab}>
        <div className="flex flex-1 justify-between w-auto">
          <div className="text-left">
            <div className="flex justify-start gap-4">
              {isUpdate ? (
                updateName ? (
                  <>
                    <Input
                      placeholder="Unit name"
                      ref={inputName}
                      value={unitName}
                      onChange={(e) => setUnitName(e.target.value)}
                    />

                    <Button type="button" onClick={() => handleSaveName()}>
                      Save
                    </Button>
                  </>
                ) : (
                  <>
                    <h1>{unit.unitName}</h1>
                    <Pencil
                      className="h-5 w-5"
                      onClick={() => setUpdateName(true)}
                    />
                  </>
                )
              ) : (
                <h1>{unit.unitName}</h1>
              )}
            </div>
            <span className="text-sm text-muted-foreground">
              {calculateTotalDurationTrainingUnit(unit).toFixed(2)}hrs
            </span>
          </div>
          <CollapsibleTrigger>
            <ChevronDownCircle className="h-5 w-5" />
          </CollapsibleTrigger>
        </div>
        <CollapsibleContent className=" space-y-2">
          {unit.trainingContents.map((content) => (
            <TrainingContent
              content={content}
              unitName={unit.unitName}
              unitCode={unit.unitCode}
              mode={mode}
              handleDeleteContentUnit={handleDeleteContentUnit}
            />
          ))}
          <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger>
              {(mode === "edit" || mode === "create" || isUpdate) && (
                <PlusCircle className="mt-2 h-5 w-5" />
              )}
            </DialogTrigger>
            <DialogContent className="w-[600px] overflow-hidden bg-transparent p-0">
              <CreateSyllabusForm handleCreateContent={handleCreateContent} />
            </DialogContent>
          </Dialog>
        </CollapsibleContent>
      </Collapsible>
    </div>
  )
}
