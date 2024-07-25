import { useEffect, useState } from "react"

import { useSyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import {
  deleteObject,
  getDownloadURL,
  getMetadata,
  listAll,
  ref,
  uploadBytes
} from "firebase/storage"
import { FileUp, FolderOpen, Trash } from "lucide-react"
import { toast } from "sonner"
import { v4 as uuid } from "uuid"

import {
  IDeliveryType,
  ITrainingContent,
  ITrainingFormat
} from "@/types/syllabus.interface"

import { fileDB } from "@/lib/firebase"
import { cn } from "@/lib/utils"

import { Button } from "../atoms/button"
import { Dialog, DialogContent, DialogTrigger } from "../atoms/dialog"
import { Separator } from "../atoms/separator"
import assignmentIcon from "/delevery-icons/AssignmentIcon.svg"
import conceptIcon from "/delevery-icons/ConceptIcon.svg"
import examIcon from "/delevery-icons/ExamIcon.svg"
import guideIcon from "/delevery-icons/GuideIcon.svg"
import seminarIcon from "/delevery-icons/SeminarIcon.svg"
import testIcon from "/delevery-icons/TestIcon.svg"

interface TrainingContentProps {
  content: ITrainingContent
  unitName: string
  unitCode: string
  mode: "view" | "edit" | "create"
  handleDeleteContentUnit: (contentId: string) => void
}

interface FirebaseFile {
  name: string
  url: string
  uploadDate?: string
}

export const TrainingContent = ({
  content,
  unitName,
  unitCode,
  mode,
  handleDeleteContentUnit
}: TrainingContentProps) => {
  const [firebaseFiles, setFirebaseFiles] = useState<FirebaseFile[]>([])
  const { isEdit } = useSyllabusDetailContext()

  const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      const selectedFiles = Array.from(e.target.files)
      await uploadFilesToFirebase(selectedFiles)
    }
  }

  const uploadFilesToFirebase = async (files: File[]) => {
    if (files.length === 0) {
      toast.error("Please choose a file")
      return
    }

    const promises = files.map(async (file) => {
      const storageRef = ref(
        fileDB,
        `training-content-materials/${content.contentId}/${file.name + " | " + uuid()}`
      )
      await uploadBytes(storageRef, file).then(async () => {
        const url = await getDownloadURL(storageRef)
        const uploadDate = new Date().toLocaleDateString()
        setFirebaseFiles((prevFiles) => [
          ...prevFiles,
          { name: storageRef.name, url, uploadDate }
        ])
      })
    })

    const allPromises = Promise.all(promises)

    try {
      await toast.promise(allPromises, {
        loading: "Uploading files...",
        success: "Files uploaded successfully",
        error: "Error uploading files"
      })
    } catch (error) {
      console.error("Error uploading files:", error)
      toast.error("Error uploading files")
    }
  }

  const handleDeleteInFirebase = async (fileName: string) => {
    const storageRef = ref(
      fileDB,
      `training-content-materials/${content.contentId}/${fileName}`
    )

    try {
      await deleteObject(storageRef)
      setFirebaseFiles((prevFiles) =>
        prevFiles.filter((file) => file.name !== fileName)
      )
      toast.success(`Deleted ${fileName} successfully`)
    } catch (error) {
      console.error(`Error deleting ${fileName}:`, error)
      toast.error(`Error deleting ${fileName}`)
    }
  }

  useEffect(() => {
    const storageRef = ref(
      fileDB,
      `training-content-materials/${content.contentId}`
    )

    listAll(storageRef).then((res) => {
      res.items.forEach(async (item) => {
        const url = await getDownloadURL(item)
        const metadata = await getMetadata(item)
        const uploadDate = metadata.timeCreated
          ? new Date(metadata.timeCreated).toLocaleDateString()
          : "Unknown"
        setFirebaseFiles((prevFiles) => [
          ...prevFiles,
          { name: item.name, url, uploadDate }
        ])
      })
    })
  }, [unitCode, content.contentId])

  return (
    <div className="flex w-full justify-between rounded-sm bg-zinc-100 px-3 py-1">
      <p className="font-semibold">{content.content}</p>
      <div className="flex items-center justify-center space-x-4 text-sm">
        <span className="items-center rounded-sm bg-primary px-2.5 py-1 text-xs font-medium text-white">
          {content.code}
        </span>

        <span>{content.duration} min</span>
        <span className="items-center rounded-sm border border-green-400 bg-green-100 px-2.5 py-1 text-xs font-medium text-green-800">
          {content.trainingFormat === ITrainingFormat.Online
            ? "Online"
            : "Offline"}
        </span>

        {checkDeliveryType(content.deliveryType)}
        <button>
          <Dialog>
            <DialogTrigger asChild>
              <button className="flex items-center rounded-sm font-semibold">
                <FolderOpen className="h-5 w-5" />
              </button>
            </DialogTrigger>
            <DialogContent className="w-auto min-w-[700px]">
              <div className="flex w-full flex-col justify-between space-y-4">
                <div className="flex space-x-7 font-semibold">
                  <p className="text-lg">{unitName}</p>
                </div>
                <div className="flex flex-col rounded-md bg-zinc-100 px-2.5 py-1.5">
                  <div className="mb-2 flex items-center justify-between">
                    <p className="text-sm font-semibold">{content.content}</p>
                    {(isEdit || mode === "edit" || mode === "create") && (
                      <Button
                        size="sm"
                        className="flex w-[23%] items-center"
                        asChild
                      >
                        <label
                          htmlFor="dropzone-file"
                          className="cursor-pointer"
                        >
                          <div className="flex flex-col items-center justify-center">
                            <p className=" text-sm text-white">
                              <span className="flex items-center">
                                <FileUp className="mr-2 h-4 w-4" /> Upload files
                              </span>
                            </p>
                          </div>
                          <input
                            id="dropzone-file"
                            type="file"
                            multiple
                            className="hidden"
                            onChange={handleFileChange}
                          />
                        </label>
                      </Button>
                    )}
                  </div>
                  <Separator className="mb-2" />
                  <div className="mt-2 max-h-[300px] space-y-4 overflow-y-auto">
                    {firebaseFiles.map((file, index) => (
                      <div
                        key={index}
                        className="flex items-center justify-between space-x-4 text-xs"
                      >
                        <a
                          href={file.url}
                          className="max-w-[400px] truncate text-[#0C4DA2] underline"
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          {file.name}
                        </a>
                        <div className="flex items-center justify-between space-x-4 text-xs">
                          <p
                            className={cn(
                              "text-muted-foreground",
                              !isEdit && "mr-1"
                            )}
                          >
                            Uploaded on {file.uploadDate}
                          </p>
                          {(isEdit || mode === "edit" || mode === "create") && (
                            <button
                              onClick={() => handleDeleteInFirebase(file.name)}
                              className="rounded-full p-1 hover:bg-red-100 hover:text-red-800"
                            >
                              <Trash className="h-4 w-4" />
                            </button>
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              </div>
            </DialogContent>
          </Dialog>
        </button>
        {(isEdit || mode === "edit" || mode === "create") && (
          <button
            onClick={() => {
              if (mode === "create" || mode === "edit") {
                handleDeleteContentUnit(content.contentId)
              }
            }}
            className="h-full hover:bg-red-100 text-red-800 p-1 rounded-full"
          >
            <Trash className="h-5 w-5" />
          </button>
        )}
      </div>
    </div>
  )
}

const checkDeliveryType = (deliveryType: IDeliveryType) => {
  let iconStr = ""
  switch (deliveryType) {
    case IDeliveryType.AssignmentLab:
      iconStr = assignmentIcon
      break
    case IDeliveryType.ConceptLecture:
      iconStr = conceptIcon
      break
    case IDeliveryType.Exam:
      iconStr = examIcon
      break
    case IDeliveryType.GuideReview:
      iconStr = guideIcon
      break
    case IDeliveryType.TestQuiz:
      iconStr = testIcon
      break
    case IDeliveryType.SeminarWorkshop:
      iconStr = seminarIcon
      break
    default:
      iconStr = examIcon
      break
  }

  return <img className="h-5 w-5" src={iconStr} alt={`${deliveryType} icon`} />
}
