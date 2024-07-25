import { render, screen } from "@testing-library/react"
import { format } from "date-fns"

import { CollapsibleSyllabusCard } from "../collapsible-syllabus-card"

describe("CollapsibleSyllabusCard", () => {
  const mockSyllabus = {
    topicName: "Sample Topic",
    publishStatus: "Published",
    version: "1.0",
    modifiedOn: new Date(),
    modifiedBy: "John Doe",
    trainingUnits: [
      { unitName: "Unit 1", duration: 60 },
      { unitName: "Unit 2", duration: 45 }
    ]
  }

  test("renders syllabus card with correct topic name and publish status", () => {
    render(<CollapsibleSyllabusCard data={mockSyllabus} cardIndex={1} />)
    expect(screen.getByText("Sample Topic")).toBeInTheDocument()
    expect(screen.getByText("Published")).toBeInTheDocument()
  })

  test("displays correct version and duration", () => {
    render(<CollapsibleSyllabusCard data={mockSyllabus} cardIndex={1} />)
    expect(screen.getByText("1.0")).toBeInTheDocument()
    expect(screen.getByText("12 days (23 hours)")).toBeInTheDocument()
  })

  // test("renders correct number of training units", () => {
  //   render(<CollapsibleSyllabusCard data={mockSyllabus} cardIndex={1} />)
  //   const trainingUnits = screen.getAllByTestId("training-unit")
  //   expect(trainingUnits.length).toBe(mockSyllabus.trainingUnits.length)
  // })

  test("collapsible content is hidden by default", () => {
    render(<CollapsibleSyllabusCard data={mockSyllabus} cardIndex={1} />)
    expect(screen.queryByText("Unit 1")).not.toBeInTheDocument()
  })

  test("renders with correct modified information", () => {
    render(<CollapsibleSyllabusCard data={mockSyllabus} cardIndex={1} />)
    const modifiedElement = screen.getByText(
      `Modified on ${format(new Date(mockSyllabus.modifiedOn), "MM/dd/yyyy")} by John Doe`
    )
    expect(modifiedElement).toBeInTheDocument()
  })
})
