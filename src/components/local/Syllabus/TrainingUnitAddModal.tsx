import { useState } from "react"

import { Button } from "@/components/global/atoms/button"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle
} from "@/components/global/atoms/dialog"
import { Input } from "@/components/global/atoms/input"
import { Label } from "@/components/global/atoms/label"

interface TrainingUnitAddModalProps {
  isOpen: boolean
  onClose: () => void
  handleAddUnit: (dayNumber: number, unitName: string) => void
  dayNumber: number
}

export const TrainingUnitAddModal = ({
  isOpen,
  onClose,
  dayNumber,
  handleAddUnit
}: TrainingUnitAddModalProps) => {
  const [unitName, setUnitName] = useState("")

  const handleAddButtonClick = () => {
    handleAddUnit(dayNumber, unitName)
    setUnitName("")
    onClose()
  }

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Add new unit</DialogTitle>
          <DialogDescription>
            Enter the name of the new unit. Click save when you're done.
          </DialogDescription>
        </DialogHeader>
        <div className="py-4">
          <div className="space-y-2 items-center gap-4">
            <Label htmlFor="name" className="text-right">
              Name
            </Label>
            <Input
              id="name"
              value={unitName}
              onChange={(e) => setUnitName(e.target.value)}
            />
          </div>
        </div>
        <DialogFooter>
          <Button type="button" onClick={handleAddButtonClick}>
            Add Unit
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
