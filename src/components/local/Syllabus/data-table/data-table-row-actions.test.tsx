import { Row } from "@tanstack/react-table"
import { render, screen } from "@testing-library/react"

import { DataTableRowActions } from "./data-table-row-actions"

describe("DataTableRowActions Component", () => {
  test("Renders dropdown menu trigger button", () => {
    const mockRowData: Row<any> = {
      getValue: jest.fn(() => "mockTopicCode")
    }

    render(<DataTableRowActions row={mockRowData} />)

    const menuButton = screen.getByRole("button", { name: /open menu/i })
    expect(menuButton).toBeInTheDocument()
  })
})
