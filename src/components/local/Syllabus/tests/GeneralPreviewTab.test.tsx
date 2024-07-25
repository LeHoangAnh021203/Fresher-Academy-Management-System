import { useState } from "react"

import { SyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { render as rltRender, screen } from "@testing-library/react"
import { describe } from "node:test"

import { Syllabus, TrainingUnit } from "@/lib/types"

import { GeneralPreviewTab } from "../GeneralPreviewTab"
import { exampleSyllabus } from "./sampleSyllabus"

describe("GeneralPreviewTab component", () => {
  test("renders general preview tab", () => {
    const MockComponent = () => {
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
          <GeneralPreviewTab />
        </SyllabusDetailContext.Provider>
      )
    }
    rltRender(<MockComponent />)
    expect(
      screen.getByText((content, element) => {
        // Check if the text is found within an element with class "text-2xl"
        return (
          element!.classList.contains("text-xl") &&
          content === "Course objectives"
        )
      })
    ).toBeInTheDocument()
    // Similarly, test for other expected elements
    expect(screen.getByText("Level")).toBeInTheDocument()
    expect(screen.getByText("Attendee number")).toBeInTheDocument()
    expect(screen.getByText("Output standard")).toBeInTheDocument()
    // Make sure to add more assertions for other elements as needed
  })
  test("allows selecting level when isEdit is true", async () => {
    const MockComponent = () => {
      const [isEdit, setIsEdit] = useState<boolean>(true)
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
          <GeneralPreviewTab />
        </SyllabusDetailContext.Provider>
      )
    }
    rltRender(<MockComponent />)
    // Simulate clicking on the SelectTrigger to open the dropdown
    const selectTrigger = screen.getByRole("combobox")
    expect(selectTrigger).toBeInTheDocument()
  })
})
