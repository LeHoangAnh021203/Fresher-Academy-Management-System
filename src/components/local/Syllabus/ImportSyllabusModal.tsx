import React, { useState } from "react"

import { useForm } from "react-hook-form"
import { toast } from "sonner"

import { useImportSyllabus } from "@/apis/syllabus-routes"

import { Button } from "@/components/global/atoms/button"
import {
  Dialog,
  DialogContent,
  DialogTrigger
} from "@/components/global/atoms/dialog"
import { Separator } from "@/components/global/atoms/separator"

interface ImportSyllabusModalProps {
  children: React.ReactNode
}
const DOWNLOAD_TEMPLATE =
  "https://firebasestorage.googleapis.com/v0/b/fams-9a138.appspot.com/o/syllabus%2FImport_SyllabusV2.0.xlsx?alt=media&token=0066ed72-3847-439d-b142-c1ac14c06ffb"

const FileInputWithLabel = ({ register }) => (
  <div className="flex items-center justify-between">
    <p className="flex w-[50%]">
      File (xlsx) <span className="text-red-600">*</span>
    </p>
    <Button className="flex w-[40%] items-center" asChild>
      <label htmlFor="dropzone-file" className="cursor-pointer">
        <div className="flex flex-col items-center justify-center">
          <p className="text-sm text-white">
            <span>Select</span>
          </p>
        </div>
        <input
          id="dropzone-file"
          type="file"
          accept=".xlsx"
          className="hidden"
          {...register("file")}
        />
      </label>
    </Button>
  </div>
)

const ImportTemplate = () => (
  <div className="flex items-center justify-between">
    <p>Import template</p>
    <Button variant="link" asChild>
      <a href={DOWNLOAD_TEMPLATE}>Download</a>
    </Button>
  </div>
)

export const ImportSyllabusModal = ({ children }: ImportSyllabusModalProps) => {
  const { register, handleSubmit } = useForm()
  const importSyllabusMutation = useImportSyllabus()
  const [open, setOpen] = useState(false)

  const onSubmit = async (data) => {
    if (!data.file[0]) {
      toast.error("Please select a file !")
      return
    }
    try {
      const promise = importSyllabusMutation.mutateAsync(data.file[0])
      setOpen(false)

      toast.promise(promise, {
        loading: "Importing syllabus...",
        success: "Imported successfully!",
        error: (e) => `Failed to import: ${e.message}`
      })
    } catch (error) {
      console.error(error)
      toast.error("Failed to import syllabus file !")
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent className="max-w-[620px] overflow-hidden bg-transparent p-0">
        <div className="bg-primary py-2 text-center font-semibold text-white">
          Import Syllabus
        </div>
        <form onSubmit={handleSubmit(onSubmit)} className="bg-white p-4">
          <div className="grid grid-cols-3">
            <div className="col-span-1">
              <p className="text-md col-span-1 font-bold text-black">
                Import setting
              </p>
            </div>
            <div className="col-span-2 space-y-4">
              <FileInputWithLabel register={register} />
              <ImportTemplate />
            </div>
          </div>
          <Separator className="my-2" />
          <div className="flex justify-end">
            <Button type="submit">Import</Button>
          </div>
        </form>
      </DialogContent>
    </Dialog>
  )
}
