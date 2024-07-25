import { render, screen } from "@testing-library/react"

import Header from "../Header"

describe("Header Component", () => {
  const mockOnEdit = jest.fn()

  test("renders with correct content", () => {
    const props = {
      className: "Test Class",
      classCode: "TC101",
      status: "Active",
      days: 0,
      hours: 0,
      onEdit: mockOnEdit,
      isEditing: false
    }

    render(<Header {...props} />)

    expect(screen.getByText("Test Class")).toBeInTheDocument()
    expect(screen.getByText("TC101")).toBeInTheDocument()
    expect(screen.getByText("Active")).toBeInTheDocument()
  })

  test("does not show edit option when editing", () => {
    const props = {
      isEditing: true
    }

    render(<Header {...props} />)
    const moreOptionsButton = screen.queryByRole("button")
    expect(moreOptionsButton).toBeNull()
  })
})
