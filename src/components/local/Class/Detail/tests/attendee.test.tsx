import { fireEvent, render, screen } from "@testing-library/react"

import Attendee from "../Attendee"

describe("Attendee Component", () => {
  const mockAttendee = "Intern"

  test("renders attendee fields and toggle button", () => {
    render(<Attendee attendee={mockAttendee} />)
    expect(screen.getByText("Attendee")).toBeInTheDocument()
    expect(screen.getByText("Planned")).toBeInTheDocument()
    expect(screen.getByText("Accepted")).toBeInTheDocument()
    expect(screen.getByText("Actual")).toBeInTheDocument()
  })

  test("handle visibility toggle", () => {
    render(<Attendee attendee={mockAttendee} />)
    const toggleButton = screen.getByTestId("toggle-visibility")
    fireEvent.click(toggleButton)
  })

  test("toggles between editable and non-editable states", () => {
    render(<Attendee attendee={mockAttendee} />)
  })
})
