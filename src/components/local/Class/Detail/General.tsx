import { useState } from "react"

import { userData } from "@/constants/user"
import {
  Calendar,
  ChevronDownCircle,
  ChevronLeftCircle,
  Clock4
} from "lucide-react"
import {
  MdOutlineHomeWork,
  MdOutlineRecordVoiceOver,
  MdOutlineStarBorder,
  MdOutlineStars
} from "react-icons/md"

import {
  IClass,
  IClassEmail,
  IClassFSU,
  IClassLocation
} from "@/types/class.interface"

import { formatDateVN } from "@/lib/utils"

import TimePicker from "../TimePicker"
import SelectInput from "./SelectInput"

type GeneralProps = IClass & {
  location: string
  fsu: string
  createdOn: string
  handleDataChange: <K extends keyof IClass>(key: K, value: IClass[K]) => void
  onEdit: () => void
  isEditing: boolean
}

function General({
  startClassTime,
  endClassTime,
  location,
  trainers,
  admins,
  fsu,
  email,
  createdBy,
  createdOn,
  handleDataChange,
  isEditing
}: GeneralProps) {
  const [isGeneralVisible, setIsGeneralVisible] = useState(true)

  const toggleGeneralVisibility = () => {
    setIsGeneralVisible(!isGeneralVisible)
  }

  const locationOptions = Object.values(IClassLocation)
  const trainerOptions = userData
    .filter((user) => user.role === "Trainer")
    .map((user) => user.name)
  const adminOptions = userData
    .filter((user) => user.role === "Admin")
    .map((user) => user.name)
  const fsuOptions = Object.values(IClassFSU)
  const emailOptions = IClassEmail

  const locationValueToKey = Object.entries(IClassLocation).reduce(
    (acc: { [key: string]: string }, [key, value]) => {
      acc[value] = key
      return acc
    },
    {}
  )

  const fsuValueToKey = Object.entries(IClassFSU).reduce(
    (acc: { [key: string]: string }, [key, value]) => {
      acc[value] = key
      return acc
    },
    {}
  )

  const currentLocationName = location ? IClassLocation[location] : ""

  const currentFSUName = fsu ? IClassFSU[fsu] : ""

  const handleLocationChange = (selectedLocation) => {
    const locationId = locationValueToKey[selectedLocation]
    handleDataChange("locationId", locationId)
    // console.log(locationId)
  }

  const handleTrainerChange = (trainers) => {
    handleDataChange("trainers", trainers)
  }

  const handleAdminChange = (admins) => {
    handleDataChange("admins", admins)
  }

  const handleFSUChange = (selectedFSU) => {
    const fsuId = fsuValueToKey[selectedFSU]
    handleDataChange("fsuId", fsuId)
    // console.log(fsuId)
  }

  const handleEmailChange = (email) => {
    handleDataChange("email", email)
  }

  return (
    <div className="h-fit overflow-auto rounded-[10px] shadow-xl">
      <div
        className={`flex min-h-[52px] cursor-pointer select-none items-center justify-between px-4 py-2 text-white transition-all duration-200 ${
          isGeneralVisible ? "bg-primary" : "bg-[#8B8B8B]"
        }`}
      >
        <div className="flex items-center justify-between gap-[10px]">
          <Calendar />
          <span>General</span>
        </div>
        <div
          className="p-1"
          onClick={toggleGeneralVisibility}
          data-testid="toggle-visibility"
        >
          {isGeneralVisible ? <ChevronDownCircle /> : <ChevronLeftCircle />}
        </div>
      </div>

      {isGeneralVisible && (
        <div className="w-full p-5">
          <table className="flex flex-col gap-[15px] border-b-[1px] border-black pb-[15px]">
            {/* CLASS TIME */}
            <tr className="flex justify-between">
              <td className="flex items-center gap-2">
                <Clock4 size={20} />
                <span className="font-semibold">Class time</span>
              </td>
              {isEditing ? (
                <td className="flex items-center text-sm">
                  <span className="mr-1">from</span>
                  <TimePicker
                    timePickerKey="startTimePicker"
                    onChange={(time) =>
                      handleDataChange("startClassTime", time)
                    }
                  />

                  <span className="ml-2 mr-1">to</span>
                  <TimePicker
                    timePickerKey="endTimePicker"
                    onChange={(time) => handleDataChange("endClassTime", time)}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm flex gap-1 items-center">
                  <span className="relative py-[5px] text-primary">
                    {startClassTime}
                  </span>
                  <span>-</span>
                  <span className="relative py-[5px] text-primary">
                    {endClassTime}
                  </span>
                </td>
              )}
            </tr>

            {/* LOCATION */}
            <tr className="flex justify-between">
              <td className="flex gap-2">
                <MdOutlineHomeWork className="text-xl" />
                <span className="font-semibold">Location</span>
              </td>
              {isEditing ? (
                <td className="w-[180px] text-sm">
                  <SelectInput
                    placeHolder="Select"
                    selectItems={locationOptions}
                    initialSelectedItems={currentLocationName}
                    type="normal"
                    onChange={handleLocationChange}
                    multiSelect={false}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm">
                  {IClassLocation[location]}
                </td>
              )}
            </tr>

            {/* TRAINER */}
            <tr className="flex justify-between">
              <td className="flex gap-2">
                <MdOutlineRecordVoiceOver className="text-xl" />
                <span className="font-semibold">Trainer</span>
              </td>
              {isEditing ? (
                <td className="w-[180px] text-sm">
                  <SelectInput
                    placeHolder="Select"
                    selectItems={trainerOptions}
                    initialSelectedItems={trainers}
                    type="fullName"
                    onChange={handleTrainerChange}
                    multiSelect={true}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm">
                  {trainers &&
                    trainers.map((trainer, index) => (
                      <div
                        key={index}
                        className="-ml-2 w-fit cursor-pointer select-none items-center gap-2 rounded bg-white px-2 py-1 text-[#285D9A] underline hover:bg-[#DFDEDE]"
                      >
                        {trainer}
                      </div>
                    ))}
                </td>
              )}
            </tr>

            {/* ADMIN */}
            <tr className="flex justify-between">
              <td className="flex gap-2">
                <MdOutlineStarBorder className="text-xl" />
                <span className="font-semibold">Admin</span>
              </td>
              {isEditing ? (
                <td className="w-[180px] text-sm">
                  <SelectInput
                    placeHolder="Select"
                    selectItems={adminOptions}
                    initialSelectedItems={admins}
                    type="fullName"
                    onChange={handleAdminChange}
                    multiSelect={true}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm">
                  {admins &&
                    admins.map((admin, index) => (
                      <div
                        key={index}
                        className="-ml-2 flex w-fit cursor-pointer select-none items-center gap-2 rounded bg-white px-2 py-1 text-[#285D9A] underline hover:bg-[#DFDEDE]"
                      >
                        {admin}
                      </div>
                    ))}
                </td>
              )}
            </tr>

            {/* FSU */}
            <tr className="flex justify-between">
              <td className="flex gap-2">
                <MdOutlineStars className="text-xl" />
                <span className="font-semibold">FSU</span>
              </td>
              {isEditing ? (
                <td className="w-[180px] text-sm">
                  <SelectInput
                    placeHolder="Select"
                    selectItems={fsuOptions}
                    initialSelectedItems={currentFSUName}
                    type="normal"
                    onChange={handleFSUChange}
                    multiSelect={false}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm">{IClassFSU[fsu]}</td>
              )}
            </tr>

            {/* ADMIN */}
            <tr className="flex justify-between">
              <td></td>
              {isEditing ? (
                <td className="w-[180px] text-sm">
                  <SelectInput
                    placeHolder="Contacts"
                    selectItems={emailOptions}
                    initialSelectedItems={email}
                    type="email"
                    onChange={handleEmailChange}
                    multiSelect={false}
                  />
                </td>
              ) : (
                <td className="w-[180px] text-sm">{email}</td>
              )}
            </tr>
          </table>
          <div className="mt-[15px] flex flex-row justify-between gap-[15px] text-sm font-semibold text-[#8B8B8B]">
            <div className="flex flex-col gap-[15px]">
              <span>Created</span>
              <span>Review</span>
              <span>Approve</span>
            </div>
            <div className="flex flex-col gap-[15px] px-4">
              <span>
                {formatDateVN(createdOn)} by {createdBy}
              </span>
              <span>30/03/2022 by TrungDVQ</span>
              <span>02/04/2022 by VongNT</span>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}

export default General
