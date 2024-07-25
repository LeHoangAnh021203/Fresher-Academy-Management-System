import { render, screen } from "@testing-library/react"
import jest from "jest-mock"

import { NavigationBar } from "../navigation-bar"

describe("NavigationBar Component", () => {
  test('disables "Back" button at first step', () => {
    const mockPrev = jest.fn()
    render(
      <NavigationBar
        currentStep={0}
        steps={[{ name: "Step 1", id: "step1", fields: [] }]}
        next={() => {}}
        prev={mockPrev}
      />
    )

    const backButton = screen.getByText("Back")
    expect(backButton).toBeDisabled()
  })

  test('shows "Save" button at last step', () => {
    const mockNext = jest.fn()
    render(
      <NavigationBar
        currentStep={1}
        steps={[
          { name: "Step 1", id: "step1", fields: [] },
          { name: "Step 2", id: "step2", fields: [] }
        ]}
        next={mockNext}
        prev={() => {}}
      />
    )

    const saveButton = screen.getByRole("button", { name: "Save" })
    expect(saveButton).toBeInTheDocument()
  })

  // test("invokes next function on clicking Next button", () => {
  //   const mockNext = jest.fn()
  //   render(
  //     <NavigationBar
  //       currentStep={0}
  //       steps={[{ name: "Step 1", id: "step1", fields: [] }]}
  //       next={mockNext}
  //       prev={() => {}}
  //     />
  //   )

  //   const nextButton = screen.getByText("Next")
  //   fireEvent.click(nextButton)
  //   expect(mockNext).toHaveBeenCalledTimes(1)
  // })

  // test("invokes prev function on clicking Back button", () => {
  //   const mockPrev = jest.fn()
  //   render(
  //     <NavigationBar
  //       currentStep={1}
  //       steps={[
  //         { name: "Step 1", id: "step1", fields: [] },
  //         { name: "Step 2", id: "step2", fields: [] }
  //       ]}
  //       next={() => {}}
  //       prev={mockPrev}
  //     />
  //   )

  //   const backButton = screen.getByText("Back")
  //   fireEvent.click(backButton)
  //   expect(mockPrev).toHaveBeenCalledTimes(1)
  // })

  test("renders Cancel button", () => {
    const mockPrev = jest.fn()
    render(
      <NavigationBar
        currentStep={0}
        steps={[{ name: "Step 1", id: "step1", fields: [] }]}
        next={() => {}}
        prev={mockPrev}
      />
    )

    const cancelButton = screen.getByText("Cancel")
    expect(cancelButton).toBeInTheDocument()
  })

  test("does not render step name if steps array is empty", () => {
    render(
      <NavigationBar
        currentStep={0}
        steps={[]}
        next={() => {}}
        prev={() => {}}
      />
    )

    const stepName = screen.queryByText("Step 1")
    expect(stepName).toBeNull()
  })
})
