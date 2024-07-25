import { fireEvent, render, screen } from "@testing-library/react"

import TimeFrame from "../TimeFrame"

describe("TimeFrame Component", () => {
  const mockStartDate = "2023-01-01" // Example start date
  const mockEndDate = "2023-01-07" // Example end date

  test("renders TimeFrame component with correct dates", () => {
    render(<TimeFrame startDate={mockStartDate} endDate={mockEndDate} />)

    expect(screen.getByText("Time frame")).toBeInTheDocument()
    expect(screen.getByDisplayValue("01/01/2023")).toBeInTheDocument()
    expect(screen.getByDisplayValue("07/01/2023")).toBeInTheDocument()
  })

  test("toggles visibility of the time frame details", () => {
    render(<TimeFrame startDate={mockStartDate} endDate={mockEndDate} />)

    // Find toggle button and click to hide details
    const toggleButton = screen.getByTestId("toggle-visibility")
    fireEvent.click(toggleButton)
  })

  test("displays placeholder when dates are not provided", () => {
    render(<TimeFrame />)

    const dateInputs = screen.getAllByDisplayValue("--/--/----")
    expect(dateInputs.length).toBe(2)
  })
})
