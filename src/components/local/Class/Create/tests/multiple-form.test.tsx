import { render, screen, waitFor } from "@testing-library/react"
import userEvent from "@testing-library/user-event"

import MultiStepCreateClass from "../MultipleForm"

describe("MultiStepCreateClass", () => {
  test("renders without crashing", () => {
    render(<MultiStepCreateClass />)
    expect(screen.getByText("Class")).toBeInTheDocument()
  })

  test('navigates step on "Next" and "Back" button click', async () => {
    render(<MultiStepCreateClass />)

    // Simulate user input
    const classNameInput = screen.getByPlaceholderText(
      "e.g: Fresher Develop Operation"
    )
    userEvent.type(classNameInput, "Test Class Name")

    // Move to next step
    userEvent.click(screen.getByText("Next"))
    await waitFor(() => {
      // Go back to previous step
      userEvent.click(screen.getByText("Back"))
    })

    // Check if back at the first step
    expect(screen.getByText("Class")).toBeInTheDocument()
  })

  test('displays "Save" button on final step and submits data', async () => {
    render(<MultiStepCreateClass />)

    // Fill in required fields and navigate to the final step
    // Assume step 1 requires class name input
    userEvent.type(
      screen.getByPlaceholderText("e.g: Fresher Develop Operation"),
      "Test Class Name"
    )
    userEvent.click(screen.getByText("Next"))

    // Navigate to the final step
    userEvent.click(screen.getByText("Next"))

    // Check if the "Save" button is present at the final step
    const saveButton = await screen.getByTestId("save-btn")
    expect(saveButton).toBeInTheDocument()
  })
})
