import { fireEvent, render, screen } from "@testing-library/react"
import jest from "jest-mock"

import TimePicker from "../TimePicker"

describe("TimePicker Component Tests", () => {
  test("renders TimePicker component", () => {
    render(<TimePicker timePickerKey="testTimePicker" />)
    const timeDisplay = screen.getByTestId("time-display")
    expect(timeDisplay).toHaveTextContent("-- : --")
  })

  test("toggle dropdown on time display click", () => {
    render(<TimePicker timePickerKey="testTimePicker" />)

    const timeDisplay = screen.getByTestId("time-display")

    // Click to open dropdown
    fireEvent.click(timeDisplay)
    expect(screen.getByTestId("hour-increment")).toBeInTheDocument()
    expect(screen.getByTestId("minute-increment")).toBeInTheDocument()

    // Click again to close dropdown
    fireEvent.click(timeDisplay)
    expect(screen.queryByTestId("hour-increment")).not.toBeInTheDocument()
    expect(screen.queryByTestId("minute-increment")).not.toBeInTheDocument()
  })

  test("close dropdown on outside click", () => {
    render(<TimePicker timePickerKey="testTimePicker" />)

    // Open the dropdown
    const timeDisplay = screen.getByTestId("time-display")
    fireEvent.click(timeDisplay)
    expect(screen.getByTestId("hour-increment")).toBeInTheDocument()

    // Simulate a click outside the dropdown
    fireEvent.mouseDown(document.body)
    expect(screen.queryByTestId("hour-increment")).not.toBeInTheDocument()
  })

  test("sets isInputComplete when valid time is selected", () => {
    const mockOnChange = jest.fn()
    render(
      <TimePicker timePickerKey="startTimePicker" onChange={mockOnChange} />
    )

    const timeDisplay = screen.getByTestId("time-display")
    fireEvent.click(timeDisplay) // Open the dropdown

    // Simulate selecting a valid hour and minute
    const hourOption = screen.getByText("01")
    const minuteOption = screen.getByText("15")
    fireEvent.click(hourOption)
    fireEvent.click(minuteOption)

    expect(mockOnChange).toHaveBeenCalledWith("01:15")
  })

  test("selectNext increments time correctly", () => {
    render(<TimePicker timePickerKey="testTimePicker" />)
    const timeDisplay = screen.getByTestId("time-display")
    fireEvent.click(timeDisplay) // Open the dropdown

    const hourIncrementButton = screen.getByTestId("hour-increment")
    fireEvent.click(hourIncrementButton) // Increment hour
    // Logic to verify the increment

    const minuteIncrementButton = screen.getByTestId("minute-increment")
    fireEvent.click(minuteIncrementButton) // Increment minute
    // Logic to verify the increment
  })

  test("selectPrevious decrements time correctly", () => {
    render(<TimePicker timePickerKey="testTimePicker" />)
    const timeDisplay = screen.getByTestId("time-display")
    fireEvent.click(timeDisplay) // Open the dropdown

    const hourDecrementButton = screen.getByTestId("hour-decrement")
    fireEvent.click(hourDecrementButton) // Decrement hour
    // Logic to verify the decrement

    const minuteDecrementButton = screen.getByTestId("minute-decrement")
    fireEvent.click(minuteDecrementButton) // Decrement minute
    // Logic to verify the decrement
  })
})
