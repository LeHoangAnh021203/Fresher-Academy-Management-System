import { useState } from "react"

import { SyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { fireEvent, render as rltRender } from "@testing-library/react"
import { describe } from "node:test"

import { Syllabus, TrainingUnit } from "@/lib/types"

import { OutlinePreviewTab } from "../OutlinePreviewTab"
import { exampleSyllabus } from "./sampleSyllabus"

describe("OutlinePreviewTab component", () => {
  test("renders outline preview tab", () => {
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
          <OutlinePreviewTab />
        </SyllabusDetailContext.Provider>
      )
    }
    const { getByText } = rltRender(<MockComponent />)
    // Similarly, test for other expected elements
    expect(getByText("Add day")).toBeInTheDocument()
    // Make sure to add more assertions for other elements as needed
  })
  test('adds a day when "Add day" button is clicked', () => {
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
          <OutlinePreviewTab />
        </SyllabusDetailContext.Provider>
      )
    }
    const { getByText } = rltRender(<MockComponent />)
    fireEvent.click(getByText("Add day"))
  })
})
