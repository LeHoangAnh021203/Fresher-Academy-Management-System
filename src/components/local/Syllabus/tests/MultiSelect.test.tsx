import { useState } from "react"

import { createEvent, fireEvent, render, screen } from "@testing-library/react"

import { LearningObjective } from "@/lib/types"

import { MultiSelect } from "../MultiSelect"

const mockSelectedLearningObjective: LearningObjective[] = [
  {
    code: "LO1",
    name: "Learning Objective 1",
    description: "Description for Learning Objective 1"
  },
  {
    code: "LO2",
    name: "Learning Objective 2",
    description: "Description for Learning Objective 2"
  }
]

describe("MultiSelect", () => {
  test("renders selected learning objectives correctly", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>(mockSelectedLearningObjective)
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    // Check if selected learning objectives are rendered
    mockSelectedLearningObjective.forEach((learningObj) => {
      expect(screen.getByText(learningObj.code)).toBeInTheDocument()
    })
  })

  test("allows unselecting learning objectives", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>(mockSelectedLearningObjective)
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    // Click on the "X" button to unselect the first learning objective
    const closeButton = screen.getByTestId("remove-button-LO1")
    fireEvent.click(closeButton)

    // Check if setSelectedLearningObjective is called with the correct parameter
    expect(screen.getByTestId("selected-area")).not.toHaveTextContent("LO1")
  })
  test("user press delete", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>(mockSelectedLearningObjective)
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    const commandComponent = screen.getByTestId("command-component")
    fireEvent.keyDown(commandComponent, { key: "Delete" })

    // Check if setSelectedLearningObjective is called with the correct parameter
    expect(screen.getByTestId("selected-area")).not.toHaveTextContent("LO2")
  })
  test("allows selecting learning objectives", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>([])
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    // Simulate clicking on the input to open the dropdown
    const input = screen.getByPlaceholderText("Select output...")
    fireEvent.focus(input)

    // Simulate clicking on a CommandItem to select a learning objective
    const commandItem = screen.getByText("LO1") // replace "LO1" with the code of the learning objective you want to select
    fireEvent.click(commandItem)

    // Check if the selected learning objective is now in the selected area
    const selectedArea = screen.getByTestId("selected-area")
    expect(selectedArea).toHaveTextContent("LO1") // replace "LO1" with the code of the learning objective you selected
  })
  test("allows unselecting learning objectives with Enter key", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>(mockSelectedLearningObjective)
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    // Simulate pressing Enter on the remove button of the first learning objective
    const removeButton = screen.getByTestId("remove-button-LO1") // replace "LO1" with the code of the learning objective you want to unselect
    fireEvent.keyDown(removeButton, { key: "Enter" })

    // Check if the learning objective is no longer in the selected area
    const selectedArea = screen.getByTestId("selected-area")
    expect(selectedArea).not.toHaveTextContent("LO1") // replace "LO1" with the code of the learning objective you unselected
  })

  test("prevents default mouse down behavior on remove button", () => {
    const MockComponent = () => {
      const [selectedLearningObjective, setSelectedLearningObjective] =
        useState<LearningObjective[]>(mockSelectedLearningObjective)
      return (
        <MultiSelect
          selectedLearningObjective={selectedLearningObjective}
          setSelectedLearningObjective={setSelectedLearningObjective}
        />
      )
    }
    render(<MockComponent />)

    // Simulate mouse down on the remove button of the first learning objective
    const removeButton = screen.getByTestId("remove-button-LO1") // replace "LO1" with the code of the learning objective's remove button
    const mouseDownEvent = createEvent.mouseDown(removeButton)
    fireEvent(removeButton, mouseDownEvent)

    // Check if the default behavior was prevented
    expect(mouseDownEvent.defaultPrevented).toBe(true)
  })
  // You can add more tests for other functionalities like selecting new learning objectives, etc.
})
