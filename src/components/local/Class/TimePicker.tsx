import { ReactElement, useEffect, useRef, useState } from "react"

import { ChevronDown, ChevronUp } from "lucide-react"

type RenderOptionsProps = {
  start: number
  limit: number
  step: number
  setState: (value: string) => void
}

type TimePickerProps = {
  timePickerKey: string
  initialHour?: string
  initialMinute?: string
  onChange: (time: string) => void
}

function TimePicker({
  timePickerKey,
  onChange,
  initialHour = "--",
  initialMinute = "--"
}: TimePickerProps) {
  const [hour, setHour] = useState<string>(initialHour)
  const [minute, setMinute] = useState<string>(initialMinute)
  const [isOpen, setIsOpen] = useState<boolean>(false)
  const [startHour, setStartHour] = useState<number>(8)
  const [startMinute, setStartMinute] = useState<number>(0)
  const dropdownRef = useRef<HTMLDivElement | null>(null)

  const toggleDropdown = () => setIsOpen(!isOpen)

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false)
      }
    }

    document.addEventListener("mousedown", handleClickOutside)
    return () => {
      document.removeEventListener("mousedown", handleClickOutside)
    }
  }, [])

  useEffect(() => {
    if (
      timePickerKey === "startTimePicker" ||
      timePickerKey === "endTimePicker"
    ) {
      if (hour !== "--" && minute !== "--") {
        onChange(`${hour}:${minute}`)
      }
    }
  }, [timePickerKey, hour, minute, onChange])

  const renderOptions = ({
    start,
    limit,
    step,
    setState
  }: RenderOptionsProps): ReactElement[] => {
    return Array.from({ length: limit }, (_, i) => (start + i * step) % limit)
      .slice(0, 4)
      .map((value) => {
        const valueString = value.toString().padStart(2, "0")
        return (
          <div key={valueString} onClick={() => setState(valueString)}>
            {valueString}
          </div>
        )
      })
  }

  const selectNext = (
    limit: number,
    setter: React.Dispatch<React.SetStateAction<number>>
  ) => {
    setter((prev) => (prev + 1) % limit)
  }

  const selectPrevious = (
    limit: number,
    setter: React.Dispatch<React.SetStateAction<number>>
  ) => {
    setter((prev) => (prev - 1 + limit) % limit)
  }

  return (
    <div className="relative select-none rounded-[5px] border-[1px] px-[10px] py-[5px] text-primary">
      <div
        className="time-display cursor-pointer text-center"
        data-testid="time-display"
        onClick={toggleDropdown}
      >
        {hour} <span>:</span> {minute}
      </div>

      {isOpen && (
        <div
          ref={dropdownRef}
          className="time-picker absolute left-[50%] right-0 top-8 z-10 mt-1 w-[90px] translate-x-[-50%] rounded-[10px] border border-gray-300 bg-white shadow-md"
        >
          <div className="flex items-center justify-between px-[10px] py-[5px]">
            <div
              className="flex flex-col items-center justify-center gap-[5px]"
              data-testid="hour-options"
            >
              <button
                type="button"
                data-testid="hour-decrement"
                onClick={() => selectPrevious(24, setStartHour)}
              >
                <ChevronUp size={20} />
              </button>
              {renderOptions({
                start: startHour,
                limit: 22,
                step: 1,
                setState: setHour
              })}
              <button
                type="button"
                data-testid="hour-increment"
                onClick={() => selectNext(24, setStartHour)}
              >
                <ChevronDown size={20} />
              </button>
            </div>
            <span>:</span>
            <div
              className="flex flex-col items-center justify-center gap-[5px]"
              data-testid="minute-options"
            >
              <button
                type="button"
                data-testid="minute-decrement"
                onClick={() => selectNext(12, setStartMinute)}
              >
                <ChevronUp size={20} />
              </button>
              {renderOptions({
                start: startMinute * 15,
                limit: 60,
                step: 15,
                setState: setMinute
              })}
              <button
                data-testid="minute-increment"
                onClick={() => selectNext(60, setStartMinute)}
                type="button"
              >
                <ChevronDown size={20} />
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}

export default TimePicker
