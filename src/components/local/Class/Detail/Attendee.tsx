import { useEffect, useState } from "react"

import { ChevronDownCircle, ChevronLeftCircle } from "lucide-react"
import { MdOutlineStarBorder } from "react-icons/md"

import { IClass, IClassAttendee } from "@/types/class.interface"

import { Input } from "@/components/global/atoms/input"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"

type FieldState = {
  value: string
  isEditing: boolean
}

type FieldsState = {
  planned: FieldState
  accepted: FieldState
  actual: FieldState
}

const fieldStyles = {
  planned: "bg-[#2D3748] text-[#a1a1a1]",
  accepted: "bg-[#EDF2F7] text-[#474747]",
  actual: "bg-[#DFDEDE] text-black"
}

type FieldName = keyof FieldsState

const Field = ({
  fieldName,
  fieldState,
  handleFieldChange,
  isEditing
}: {
  fieldName: FieldName
  fieldState: FieldState
  handleFieldChange: (
    fieldName: FieldName,
    value: string,
    isEditing: boolean
  ) => void
  isEditing: boolean
}) => {
  const [isFieldEditing, setIsFieldEditing] = useState(fieldState.isEditing)

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    handleFieldChange(fieldName, e.target.value, true)
  }

  const handleInputEvent = (
    e:
      | React.KeyboardEvent<HTMLInputElement>
      | React.FocusEvent<HTMLInputElement>
  ) => {
    if (
      e.type === "blur" ||
      (e as React.KeyboardEvent<HTMLInputElement>).key === "Enter"
    ) {
      const value = fieldState.value.trim() === "" ? "0" : fieldState.value
      handleFieldChange(fieldName, value, false)
      setIsFieldEditing(false)
    }
  }

  const handleSpanClick = () => {
    setIsFieldEditing(true)
  }

  useEffect(() => {
    setIsFieldEditing(isEditing && fieldState.isEditing)
  }, [isEditing, fieldState.isEditing])

  return (
    <div
      className={`flex w-1/3 flex-col items-center gap-[15px] py-[10px] font-semibold ${fieldStyles[fieldName]}`}
    >
      <span className="text-sm leading-[22px]">
        {fieldName.charAt(0).toUpperCase() + fieldName.slice(1)}
      </span>
      <span className="text-2xl leading-9">
        {isFieldEditing && isEditing ? (
          <Input
            type="number"
            className="hide-spinner w-[50px] text-xl"
            value={fieldState.value}
            onChange={handleInputChange}
            onKeyDown={handleInputEvent}
            onBlur={handleInputEvent}
            data-testid={`${fieldName}-input`}
          />
        ) : (
          <span onClick={handleSpanClick}>{fieldState.value || "0"}</span>
        )}
      </span>
    </div>
  )
}

const getAttendeeLabel = (attendee: IClassAttendee): string => {
  switch (attendee) {
    case IClassAttendee.Fresher:
      return "Fresher"
    case IClassAttendee.OnlineFeeFresher:
      return "Online fee-fresher"
    case IClassAttendee.OfflineFeeFresher:
      return "Offline fee-fresher"
    case IClassAttendee.Intern:
      return "Intern"
    default:
      return "Unknown"
  }
}

function Attendee({
  attendee,
  isEditing,
  onAttendeeChange
}: IClass & {
  isEditing: boolean
  onAttendeeChange: (attendee: IClassAttendee) => void
}) {
  const [isAttendeeVisible, setIsAttendeeVisible] = useState(true)
  const attendeeText = getAttendeeLabel(attendee)
  const [fields, setFields] = useState<FieldsState>({
    planned: { value: "", isEditing: true },
    accepted: { value: "", isEditing: true },
    actual: { value: "", isEditing: true }
  })

  const toggleAttendeeVisibility = () =>
    setIsAttendeeVisible(!isAttendeeVisible)

  const handleFieldChange = (
    fieldName: FieldName,
    value: string,
    isEditing: boolean
  ) => {
    setFields({
      ...fields,
      [fieldName]: { value, isEditing }
    })
  }

  const handleAttendeeSelection = (value: string) => {
    const attendeeTypeMapping: { [key: string]: number } = {
      Fresher: 0,
      "Online fee-fresher": 1,
      "Offline fee-fresher": 2,
      Intern: 3
    }

    const attendeeType = attendeeTypeMapping[value]
    onAttendeeChange(attendeeType)
  }

  return (
    <div className="h-fit w-full overflow-auto rounded-[10px] shadow-xl">
      <div
        className={`flex min-h-[52px] cursor-pointer select-none items-center justify-between px-4 py-2 text-white transition-all duration-200 ${
          isAttendeeVisible ? "bg-primary" : "bg-[#8B8B8B]"
        }`}
      >
        <div className="flex items-center justify-between gap-[10px]">
          <MdOutlineStarBorder size={24} />
          <div className="flex items-center gap-5">
            <span>Attendee</span>
            {isEditing ? (
              <Select
                defaultValue={attendeeText}
                onValueChange={handleAttendeeSelection}
              >
                <SelectTrigger className="h-8 w-[175px] mr-2 border-none bg-white text-muted-foreground">
                  <SelectValue placeholder="Select" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="Fresher" className="text-sm">
                    Fresher
                  </SelectItem>
                  <SelectItem value="Online fee-fresher" className="text-sm">
                    Online fee-fresher
                  </SelectItem>
                  <SelectItem value="Offline fee-fresher" className="text-sm">
                    Offline fee-fresher
                  </SelectItem>
                  <SelectItem value="Intern" className="text-sm">
                    Intern
                  </SelectItem>
                </SelectContent>
              </Select>
            ) : (
              <Input
                value={attendeeText}
                className="flex h-8 cursor-pointer items-center justify-between rounded-md border border-none bg-background px-3 py-2 w-[175px] mr-2 text-sm text-primary shadow-custom ring-offset-background"
                readOnly
              />
            )}
          </div>
        </div>
        <div
          className="p-1"
          onClick={toggleAttendeeVisibility}
          data-testid="toggle-visibility"
        >
          {isAttendeeVisible ? <ChevronDownCircle /> : <ChevronLeftCircle />}
        </div>
      </div>

      {isAttendeeVisible && (
        <div className="w-full">
          <div className="flex">
            {Object.entries(fields).map(([fieldName, fieldState]) => (
              <Field
                key={fieldName}
                fieldName={fieldName as FieldName}
                fieldState={fieldState}
                handleFieldChange={handleFieldChange}
                isEditing={isEditing}
              />
            ))}
          </div>
        </div>
      )}
    </div>
  )
}

export default Attendee
