import { fireEvent, render, screen } from "@testing-library/react"

import Footer from "../Footer"

describe("Footer Component", () => {
  test("renders buttons", () => {
    const mockOnSave = jest.fn()

    render(<Footer onSave={mockOnSave} />)

    expect(screen.getByText("Cancel")).toBeInTheDocument()
    expect(screen.getByText("Save")).toBeInTheDocument()
  })

  test("calls onSave when Save button is clicked", () => {
    const mockOnSave = jest.fn()

    render(<Footer onSave={mockOnSave} />)

    fireEvent.click(screen.getByText("Save"))

    expect(mockOnSave).toHaveBeenCalled()
  })
})
