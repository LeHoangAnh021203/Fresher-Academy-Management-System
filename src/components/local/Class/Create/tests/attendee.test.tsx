import { fireEvent, render, screen } from "@testing-library/react"
import jest from "jest-mock"

import Attendee from "../Attendee"

describe("Attendee Component", () => {
  const mockOnAttendeeChange = jest.fn()

  beforeEach(() => {
    render(<Attendee onAttendeeChange={mockOnAttendeeChange} />)
  })

  test("render Attendee component", () => {
    expect(screen.getByText("Attendee")).toBeInTheDocument()
  })

  test("handle visibility toggle", () => {
    const toggleButton = screen.getByTestId("toggle-visibility") // Sử dụng getByTestId
    fireEvent.click(toggleButton)
  })

  test("handle input changes correctly", () => {
    // Adjust this query based on your actual DOM structure
    const plannedInput = screen.getByTestId("planned-input")
    fireEvent.change(plannedInput, { target: { value: "10" } })
    expect(plannedInput.value).toBe("10")
  })

  test("handle Enter key press and blur event", () => {
    const plannedInput = screen.getByTestId("planned-input")
    // Simulate Enter key press
    fireEvent.keyDown(plannedInput, { key: "Enter", code: "Enter" })
    // Simulate blur event
    fireEvent.blur(plannedInput)
    // Additional assertions as needed
  })

  test("handle input changes correctly", () => {
    // Adjust this query based on your actual DOM structure
    const acceptedInput = screen.getByTestId("accepted-input")
    fireEvent.change(acceptedInput, { target: { value: "10" } })
    expect(acceptedInput.value).toBe("10")
  })

  test("handle Enter key press and blur event", () => {
    const acceptedInput = screen.getByTestId("accepted-input")
    // Simulate Enter key press
    fireEvent.keyDown(acceptedInput, { key: "Enter", code: "Enter" })
    // Simulate blur event
    fireEvent.blur(acceptedInput)
    // Additional assertions as needed
  })

  test("handle input changes correctly", () => {
    // Adjust this query based on your actual DOM structure
    const actualInput = screen.getByTestId("actual-input")
    fireEvent.change(actualInput, { target: { value: "10" } })
    expect(actualInput.value).toBe("10")
  })

  test("handle Enter key press and blur event", () => {
    const actualInput = screen.getByTestId("actual-input")
    // Simulate Enter key press
    fireEvent.keyDown(actualInput, { key: "Enter", code: "Enter" })
    // Simulate blur event
    fireEvent.blur(actualInput)
    // Additional assertions as needed
  })
})
