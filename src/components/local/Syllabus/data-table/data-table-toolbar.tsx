import { CREATE_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { Table } from "@tanstack/react-table"
import { Import, PlusCircle, Search } from "lucide-react"
import { useNavigate } from "react-router-dom"

import { Button } from "@/components/global/atoms/button"

import { ImportSyllabusModal } from "../ImportSyllabusModal"

interface DataTableToolbarProps<TData> {
  table: Table<TData>
}
const status = [
  {
    label: "Active",
    value: "Active"
  },
  { label: "Inactive", value: "Inactive" },
  { label: "Draft", value: "Draft" }
]

export function DataTableToolbar<TData>({
  table
}: DataTableToolbarProps<TData>) {
  const { permissions } = useAuthContext()
  const isFiltered = table.getState().columnFilters.length > 0
  const navigate = useNavigate()
  return (
    <div className="flex items-center justify-between">
      <div className="flex flex-1 items-center space-x-2">
        <div className="flex items-center space-x-2">
          <div className="relative w-full">
            <div className="pointer-events-none absolute inset-y-0 start-0 flex items-center ps-3">
              <Search className="h-4 w-4 text-gray-400" />
            </div>
            <input
              type="text"
              id="simple-search"
              className="block w-[300px] rounded-lg border border-gray-300 bg-gray-50 p-2.5 ps-10 text-sm text-gray-900 "
              placeholder="Search by syllabus name..."
              value={
                (table.getColumn("topicName")?.getFilterValue() as string) ?? ""
              }
              onChange={(event) =>
                table.getColumn("topicName")?.setFilterValue(event.target.value)
              }
            />
          </div>

          {isFiltered && (
            <Button
              variant="ghost"
              onClick={() => table.resetColumnFilters()}
              className="h-8 px-2 lg:px-3"
            >
              Reset
            </Button>
          )}
        </div>
      </div>
      <div className="flex items-center space-x-2">
        <ImportSyllabusModal>
          <Button
            variant="orange"
            disabled={
              !CREATE_PERMISSION.includes(
                permissions?.syllabus ? permissions?.syllabus : 0
              )
            }
            onClick={() => navigate("/syllabus/new")}
          >
            <Import className="h-4 w-4" />
            Import
          </Button>
        </ImportSyllabusModal>
        <Button
          variant="primary"
          type="button"
          disabled={
            !CREATE_PERMISSION.includes(
              permissions?.syllabus ? permissions?.syllabus : 0
            )
          }
          onClick={() => navigate("/syllabus/new")}
        >
          <PlusCircle className="h-4 w-4" />
          Add new
        </Button>
      </div>
    </div>
  )
}
