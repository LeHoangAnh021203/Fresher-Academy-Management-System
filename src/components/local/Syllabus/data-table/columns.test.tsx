import { render, screen } from "@testing-library/react"

import { columns } from "./columns"

describe("DataTable Columns", () => {
  test("Renders Code column correctly", () => {
    // Mock data for the cell
    const mockRowData = { getValue: jest.fn(() => "ABC123") }

    // Render the Code cell
    render(columns[1].cell({ row: mockRowData }))

    // Assert that the cell content is rendered correctly
    const cellElement = screen.getByText("ABC123")
    expect(cellElement).toBeInTheDocument()
  })

  // Add similar tests for other columns (Created on, Created by, Duration, Output standard, Status)
})
