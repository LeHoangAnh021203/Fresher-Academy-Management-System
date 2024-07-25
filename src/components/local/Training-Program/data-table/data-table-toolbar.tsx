import { CREATE_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import { Table } from "@tanstack/react-table"
import { Import, PlusCircle, Search } from "lucide-react"
import { Link, useNavigate, useRoutes } from "react-router-dom"

import { Button } from "@/components/global/atoms/button"
import { DataTableFacetedFilter } from "@/components/global/molecules/data-table/data-table-faceted-filter"

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
              placeholder="Search program name..."
              value={
                (table.getColumn("name")?.getFilterValue() as string) ?? ""
              }
              onChange={(event) =>
                table.getColumn("name")?.setFilterValue(event.target.value)
              }
            />
          </div>
          {table.getColumn("status") && (
            <DataTableFacetedFilter
              column={table.getColumn("status")}
              title="Status"
              options={status}
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
      <div className="flex items-center space-x-2">
        <Button
          variant="orange"
          disabled={
            !CREATE_PERMISSION.includes(
              permissions?.trainingProgram ? permissions?.trainingProgram : 0
            )
          }
        >
          <Import className="h-4 w-4" />
          Import
        </Button>
        <Button
          variant="primary"
          type="button"
          disabled={
            !CREATE_PERMISSION.includes(
              permissions?.trainingProgram ? permissions?.trainingProgram : 0
            )
          }
          onClick={() => navigate("/training-programs/new")}
        >
          <PlusCircle className="h-4 w-4" />
          Add new
        </Button>
      </div>
    </div>
  )
}
