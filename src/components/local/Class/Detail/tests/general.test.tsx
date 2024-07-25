import { fireEvent, render, screen } from "@testing-library/react"

import General from "../General"

describe("General Component", () => {
  const mockData = {
    startClassTime: "09:00",
    endClassTime: "17:00",
    location: ["New York"],
    trainers: [{ name: "John Doe" }],
    admins: [{ name: "Jane Doe" }],
    fsu: "FSU Name",
    email: "email@example.com",
    createdBy: { name: "Creator" },
    createdOn: "01/01/2021"
  }

  test("renders with correct content", () => {
    render(<General {...mockData} />)

    expect(screen.getByText("General")).toBeInTheDocument()
    expect(screen.getByText("09:00")).toBeInTheDocument()
    expect(screen.getByText("17:00")).toBeInTheDocument()
    expect(screen.getByText("New York")).toBeInTheDocument()
    // Add more assertions for other fields
  })

  test("toggles visibility of details", () => {
    render(<General {...mockData} />)
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
