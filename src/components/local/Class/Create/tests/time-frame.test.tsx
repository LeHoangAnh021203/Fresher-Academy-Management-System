import { fireEvent, render, screen } from "@testing-library/react"
import { eachDayOfInterval, getDay, startOfDay } from "date-fns"
import jest from "jest-mock"

import TimeFrame from "../TimeFrame"

describe("TimeFrame Component", () => {
  const mockOnStartChange = jest.fn()
  const mockOnEndChange = jest.fn()

  test("renders TimeFrame component", () => {
    render(
      <TimeFrame
        onStartChange={mockOnStartChange}
        onEndChange={mockOnEndChange}
      />
    )
    expect(screen.getByText("Time frame")).toBeInTheDocument()
  })

  test("toggles visibility of time frame details", () => {
    render(
      <TimeFrame
        onStartChange={mockOnStartChange}
        onEndChange={mockOnEndChange}
      />
    )
    const toggleButton = screen.getByTestId("toggle-visibility")
    fireEvent.click(toggleButton)
  })

  test("selects start and end dates", () => {
    render(
      <TimeFrame
        onStartChange={mockOnStartChange}
        onEndChange={mockOnEndChange}
      />
    )

    // Find all instances of "15" and "20" and filter out disabled or outside days
    const allStartDays = screen
      .getAllByText("15")
      .filter(
        (day) =>
          !day.classList.contains("day-disabled") &&
          !day.classList.contains("day-outside")
      )
    const allEndDays = screen
      .getAllByText("20")
      .filter(
        (day) =>
          !day.classList.contains("day-disabled") &&
          !day.classList.contains("day-outside")
      )

    // Check if we have at least one valid day to select
    if (allStartDays.length > 0 && allEndDays.length > 0) {
      fireEvent.click(allStartDays[0])
      fireEvent.click(allEndDays[0])
    } else {
      throw new Error("Valid calendar days not found")
    }
  })

  test("clears selected dates", () => {
    render(
      <TimeFrame
        onStartChange={mockOnStartChange}
        onEndChange={mockOnEndChange}
      />
    )
    const clearButton = screen.getByRole("button", { name: /clear/i })
    fireEvent.click(clearButton)
  })

  const isWeekdayEven = (date) => {
    return getDay(date) % 2 === 0
  }

  test("returns true for even weekdays", () => {
    // Assuming Sunday as 0, Tuesday as 2, etc.
    expect(isWeekdayEven(new Date("2024-03-26"))).toBe(true) // Tuesday
    expect(isWeekdayEven(new Date("2024-03-28"))).toBe(true) // Thursday
    // Add more test cases as needed
  })

  test("returns false for odd weekdays", () => {
    expect(isWeekdayEven(new Date("2024-03-25"))).toBe(false) // Monday
    expect(isWeekdayEven(new Date("2024-03-27"))).toBe(false) // Wednesday
    // Add more test cases as needed
  })

  const findMatchingWeekdays = (startDate, endDate, isEven) => {
    const dates = eachDayOfInterval({ start: startDate, end: endDate })
    return dates.filter((date) => isWeekdayEven(startOfDay(date)) === isEven)
  }

  test("returns correct matching weekdays", () => {
    const startDate = startOfDay(new Date("2024-03-25")) // Monday
    const endDate = startOfDay(new Date("2024-03-30")) // Sunday

    const evenDays = findMatchingWeekdays(startDate, endDate, true)
    expect(evenDays).toEqual([
      startOfDay(new Date("2024-03-26")), // Tuesday
      startOfDay(new Date("2024-03-28")), // Thursday
      startOfDay(new Date("2024-03-30")) // Saturday
    ])

    const oddDays = findMatchingWeekdays(startDate, endDate, false)
    expect(oddDays).toEqual([
      startOfDay(new Date("2024-03-25")), // Monday
      startOfDay(new Date("2024-03-27")), // Wednesday
      startOfDay(new Date("2024-03-29")) // Friday
    ])
  })
})
