import { fireEvent, render, screen } from "@testing-library/react"

import TrainingProgram from "../TrainingProgram"

describe("TrainingProgram Component", () => {
  test("renders TrainingProgram component", () => {
    render(
      <TrainingProgram
        onProgramSelect={() => {}}
        register={() => {}}
        errors={{}}
      />
    )
    expect(screen.getByText("Training program")).toBeInTheDocument()
  })

  test("switches to different tabs", () => {
    render(
      <TrainingProgram
        onProgramSelect={() => {}}
        register={() => {}}
        errors={{}}
      />
    )

    const attendeeListTab = screen.getByText("Attendee list")
    fireEvent.click(attendeeListTab)
    expect(screen.getByText("Attendee list")).toBeInTheDocument()
  })

  test("searches for a program", () => {
    render(
      <TrainingProgram
        onProgramSelect={() => {}}
        register={() => {}}
        errors={{}}
      />
    )

    const searchInput = screen.getByPlaceholderText("Select program")
    fireEvent.change(searchInput, { target: { value: "some search query" } })
    expect(searchInput.value).toBe("some search query")
  })

  test("clears the search query", () => {
    render(
      <TrainingProgram
        onProgramSelect={() => {}}
        register={() => {}}
        errors={{}}
      />
    )

    const searchInput = screen.getByPlaceholderText("Select program")
    fireEvent.change(searchInput, { target: { value: "some text" } })

    const clearButton = screen.getByTestId("clear")
    fireEvent.click(clearButton)

    expect(searchInput.value).toBe("")
  })
})
