import { fireEvent, render, screen } from "@testing-library/react"
import jest from "jest-mock"

import General from "../General"

describe("General Component", () => {
  const mockHandleDataChange = jest.fn()

  test("renders General component", () => {
    render(<General handleDataChange={mockHandleDataChange} />)
    expect(screen.getByText("General")).toBeInTheDocument()
  })

  test("toggles visibility of details", () => {
    render(<General handleDataChange={mockHandleDataChange} />)
    const toggleButton = screen.getByTestId("toggle-visibility")
    // Initially, details are visible
    expect(screen.getByText("Class time")).toBeInTheDocument()

    // Click the toggle button
    fireEvent.click(toggleButton)
    // After clicking, the details should not be visible
    expect(screen.queryByText("Class time")).not.toBeInTheDocument()

    // Click again to toggle visibility back
    fireEvent.click(toggleButton)
    // The details should be visible again
    expect(screen.getByText("Class time")).toBeInTheDocument()
  })
})
