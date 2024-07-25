// data-table-toolbar.test.tsx
import { Table } from "@tanstack/react-table"
import { TableState } from "@tanstack/react-table"
import { render } from "@testing-library/react"

import { DataTableToolbar } from "./data-table-toolbar"

describe("DataTableToolbar component", () => {
  it("renders correctly", () => {
    const mockTableState: TableState<any> = {
      columnFilters: []
      // Các thuộc tính khác của TableState nếu cần
    }
    const mockTable: Table<any> = {
      _features: [],
      _getAllFlatColumnsById: jest.fn(),
      _getColumnDefs: jest.fn(),
      _getDefaultColumnDef: jest.fn(),

      getState: jest.fn(() => mockTableState),
      getColumn: jest.fn(),
      resetColumnFilters: jest.fn()
    }

    const props = {
      table: mockTable
    }

    const { getByPlaceholderText, getByText } = render(
      <DataTableToolbar {...props} />
    )

    expect(
      getByPlaceholderText("Search by syllabus name...")
    ).toBeInTheDocument()
    expect(getByText("Reset")).toBeInTheDocument()
    expect(getByText("Import")).toBeInTheDocument()
    expect(getByText("Add new")).toBeInTheDocument()
  })

  // Các test case khác ở đây...
})
