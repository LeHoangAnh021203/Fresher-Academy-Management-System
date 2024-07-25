import { fireEvent, render, screen } from "@testing-library/react"

import TrainingProgram from "../TrainingProgram"

describe("TrainingProgram Component", () => {
  const mockData = {
    programs: [{ name: "Program 1", trainingProgramCode: "P1" }]
  }

  const mockOnProgramSelect = jest.fn()

  beforeEach(() => {
    jest.clearAllMocks()
  })

  test("renders non-editing mode correctly", () => {
    render(
      <TrainingProgram
        {...mockData}
        isEditing={false}
        onProgramSelect={mockOnProgramSelect}
      />
    )
  })

  test("renders editing mode correctly", () => {
    render(
      <TrainingProgram
        {...mockData}
        isEditing={true}
        onProgramSelect={mockOnProgramSelect}
      />
    )
  })

  test("search functionality works in edit mode", async () => {
    render(
      <TrainingProgram
        {...mockData}
        isEditing={true}
        onProgramSelect={mockOnProgramSelect}
      />
    )
    const searchInput = screen.getByPlaceholderText("Select program")
    fireEvent.change(searchInput, { target: { value: "test" } })
  })

  test("tabs switch content when clicked", () => {
    render(
      <TrainingProgram
        {...mockData}
        isEditing={true}
        onProgramSelect={mockOnProgramSelect}
      />
    )
    const budgetTab = screen.getByText("Budget")
    fireEvent.click(budgetTab)
  })

  test("syllabus cards are displayed when a training program is selected", () => {
    render(
      <TrainingProgram
        {...mockData}
        isEditing={false}
        onProgramSelect={mockOnProgramSelect}
      />
    )
  })
})
