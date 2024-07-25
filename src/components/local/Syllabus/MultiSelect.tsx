"use client"

import * as React from "react"

import { learningObjectiveData } from "@/constants/learning-objective"
import { Command as CommandPrimitive } from "cmdk"
import { XCircle } from "lucide-react"

import { LearningObjective } from "@/lib/types"

import { Badge } from "@/components/global/atoms/badge"
import {
  Command,
  CommandGroup,
  CommandItem
} from "@/components/global/atoms/command"

interface MultiSelectProps {
  selectedLearningObjective: LearningObjective[]
  setSelectedLearningObjective: React.Dispatch<
    React.SetStateAction<LearningObjective[]>
  >
}

export function MultiSelect({
  selectedLearningObjective,
  setSelectedLearningObjective
}: MultiSelectProps) {
  const inputRef = React.useRef<HTMLInputElement>(null)
  const [open, setOpen] = React.useState(false)
  const [inputValue, setInputValue] = React.useState("")

  const handleUnselect = React.useCallback((learningObj: LearningObjective) => {
    console.log("Unselecting:", learningObj)
    setSelectedLearningObjective((prev) =>
      prev.filter((s) => s.code !== learningObj.code)
    )
  }, [])

  const handleKeyDown = React.useCallback(
    (e: React.KeyboardEvent<HTMLDivElement>) => {
      const input = inputRef.current
      if (input) {
        if (e.key === "Delete" || e.key === "Backspace") {
          if (input.value === "") {
            setSelectedLearningObjective((prev) => {
              const newSelected = [...prev]
              newSelected.pop()
              return newSelected
            })
          }
        }
        if (e.key === "Escape") {
          input.blur()
        }
      }
    },
    []
  )

  const selectables = learningObjectiveData.filter(
    (learningObj) => !selectedLearningObjective.includes(learningObj)
  )

  return (
    <Command
      onKeyDown={handleKeyDown}
      className="overflow-visible bg-transparent"
      data-testid="command-component"
    >
      <div className="group rounded-md border border-primary px-1 py-2 text-sm ring-offset-background  focus-within:ring-ring focus-within:ring-offset-2">
        <div className="flex flex-wrap gap-1" data-testid="selected-area">
          {selectedLearningObjective.map((learningObj) => {
            return (
              <Badge key={learningObj.code} variant="default">
                {learningObj.code}
                <button
                  className="ml-1 rounded-full outline-none ring-offset-background focus:ring-2 focus:ring-ring focus:ring-offset-2"
                  onKeyDown={(e) => {
                    if (e.key === "Enter") {
                      handleUnselect(learningObj)
                    }
                  }}
                  onMouseDown={(e) => {
                    e.preventDefault()
                    e.stopPropagation()
                  }}
                  onClick={() => handleUnselect(learningObj)}
                  data-testid={`remove-button-${learningObj.code}`}
                >
                  <XCircle className="h-5 w-5 text-white hover:text-white/90" />
                </button>
              </Badge>
            )
          })}
          {/* Avoid having the "Search" Icon */}
          <CommandPrimitive.Input
            ref={inputRef}
            value={inputValue}
            onValueChange={setInputValue}
            onBlur={() => setOpen(false)}
            onFocus={() => setOpen(true)}
            placeholder="Select output..."
            className="ml-2 flex-1 bg-transparent outline-none placeholder:text-muted-foreground"
          />
        </div>
      </div>
      <div className="relative mt-2">
        {open && selectables.length > 0 ? (
          <div className="absolute top-0 z-10 w-full rounded-md border bg-popover text-popover-foreground shadow-md outline-none animate-in">
            <CommandGroup className="h-full overflow-auto">
              {selectables.map((framework) => {
                return (
                  <CommandItem
                    key={framework.code}
                    onMouseDown={(e) => {
                      e.preventDefault()
                      e.stopPropagation()
                    }}
                    onSelect={() => {
                      setInputValue("")
                      setSelectedLearningObjective((prev) => [
                        ...prev,
                        framework
                      ])
                    }}
                    className={"cursor-pointer"}
                  >
                    {framework.code}
                  </CommandItem>
                )
              })}
            </CommandGroup>
          </div>
        ) : null}
      </div>
    </Command>
  )
}
