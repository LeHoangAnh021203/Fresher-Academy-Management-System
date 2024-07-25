import { CREATE_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { Table } from "@tanstack/react-table"
import { Import, Search } from "lucide-react"
import { useNavigate } from "react-router-dom"

import { Button } from "@/components/global/atoms/button"
import { DataTableFacetedFilter } from "@/components/global/molecules/data-table/data-table-faceted-filter"

import { AddUser } from "../add-user-form"

interface DataTableToolbarProps<TData> {
  table: Table<TData>
}
const types = [
  {
    label: "Admin",
    value: "Admin"
  },
  { label: "Trainer", value: "Trainer" }
]

const genders = [
  {
    label: "Male",
    value: "Male"
  },
  { label: "Female", value: "Female" }
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
              placeholder="Search by name..."
              value={
                (table.getColumn("name")?.getFilterValue() as string) ?? ""
              }
              onChange={(event) =>
                table.getColumn("name")?.setFilterValue(event.target.value)
              }
            />
          </div>
          {table.getColumn("type") && (
            <DataTableFacetedFilter
              column={table.getColumn("type")}
              title="Type"
              options={types}
            />
          )}
          {table.getColumn("gender") && (
            <DataTableFacetedFilter
              column={table.getColumn("gender")}
              title="Gender"
              options={genders}
            />
          )}
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
      <div className="flex items-center">
        <Button
          variant="orange"
          disabled={
            !CREATE_PERMISSION.includes(
              permissions?.userManagement ? permissions?.userManagement : 0
            )
          }
          onClick={() => navigate("/user/new")}
        >
          <Import className="h-4 w-4" />
          Import
        </Button>
        <AddUser />
      </div>
    </div>
  )
}
