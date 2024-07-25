import { useState } from "react"

import { SyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { render, screen } from "@testing-library/react"

import { Syllabus, TrainingUnit } from "@/lib/types"

import { OthersPreviewTab } from "../OthersPreviewTab"
import { exampleSyllabus } from "./sampleSyllabus"

// Make sure to provide the correct path to your component

// Mock the context and any other dependencies if needed
jest.mock("@/contexts/syllabus-detail-provider", () => ({
  useSyllabusDetailContext: jest.fn(() => ({
    syllabus: {
      trainingPrinciple: "Mocked training principle text" // Provide any mocked data needed for the test
    }
  }))
}))

describe("OthersPreviewTab", () => {
  it("renders the component correctly", () => {
    // Provide any necessary props for the component
    const MockComponent = () => {
      const timeAllocation = {
        Quiz: "15%",
        Assignment: "15%",
        Final: "70%"
      }
      const [isEdit, setIsEdit] = useState<boolean>(false)
      const [syllabus, setSyllabus] = useState<Syllabus | null>(exampleSyllabus)
      const [trainingUnits, setTrainingUnits] = useState<TrainingUnit[]>([])
      return (
        <SyllabusDetailContext.Provider
          value={{
            isEdit,
            setIsEdit,
            syllabus,
            setSyllabus,
            trainingUnits,
            setTrainingUnits
          }}
        >
          <OthersPreviewTab timeAllocation={timeAllocation} />
        </SyllabusDetailContext.Provider>
      )
    }

    render(<MockComponent />)

    // Add your assertions here to ensure that the component renders as expected
    // For example:
    expect(screen.getByText("Time allocation")).toBeInTheDocument()
    expect(screen.getByText("Assessment Scheme")).toBeInTheDocument()
    expect(screen.getByText("Training delivery principle")).toBeInTheDocument()
  })

  // Add more test cases as needed
})
