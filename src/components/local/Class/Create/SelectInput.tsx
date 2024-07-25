import { useState } from "react"

import { SelectViewport } from "@radix-ui/react-select"
import { X } from "lucide-react"

import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"

type SelectInputProps = {
  placeHolder: string
  selectItems: string[]
  type: string
  onChange: (value: string | string[]) => void
  multiSelect?: boolean
}

function SelectInput({
  placeHolder,
  selectItems,
  type,
  onChange,
  multiSelect = true
}: SelectInputProps) {
  const [selectedItems, setSelectedItems] = useState<string[]>([])
  const [selectedValue] = useState("")

  const handleSelectChange = (item: string) => {
    let newSelectedItems
    if (multiSelect) {
      if (selectedItems.includes(item)) {
        newSelectedItems = selectedItems.filter((selected) => selected !== item)
      } else {
        newSelectedItems = [...selectedItems, item]
      }
    } else {
      newSelectedItems = [item]
    }

    setSelectedItems(newSelectedItems)
    onChange(multiSelect ? newSelectedItems : item)
  }

  const handleRemoveItem = (item: string) => {
    const newSelectedItems = selectedItems.filter(
      (selected) => selected !== item
    )
    setSelectedItems(newSelectedItems)
    onChange(newSelectedItems)
  }
  const allItemsSelected = selectedItems.length === selectItems.length

  // console.log(selectedItems);

  return (
    <div className="flex flex-col gap-1">
      {selectedItems.map((item, index) => (
        <div key={index} className="flex items-center">
          <div
            className={`ml-0 flex w-fit cursor-pointer select-none items-center gap-2 rounded bg-white px-2 py-1 hover:bg-[#DFDEDE] ${
              type === "fullName" ? "text-[#285D9A] underline" : "text-primary"
            }`}
          >
            {item}
          </div>
          <X
            size={16}
            className="ml-1 cursor-pointer text-[#E74A3B]"
            onClick={() => handleRemoveItem(item)}
          />
        </div>
      ))}
      {!allItemsSelected && (
        <div className="relative">
          <Select value={selectedValue} onValueChange={handleSelectChange}>
            <SelectTrigger className="h-8 cursor-default focus-visible:outline-none focus-visible:ring-0">
              <SelectValue placeholder={placeHolder || "Select"} />
            </SelectTrigger>
            <SelectContent>
              <SelectViewport>
                {selectItems
                  .filter((item) => !selectedItems.includes(item))
                  .map((item, index) => (
                    <SelectGroup key={index}>
                      <SelectItem key={item} value={item}>
                        {item}
                      </SelectItem>
                    </SelectGroup>
                  ))}
              </SelectViewport>
            </SelectContent>
          </Select>
        </div>
      )}
    </div>
  )
}

export default SelectInput
