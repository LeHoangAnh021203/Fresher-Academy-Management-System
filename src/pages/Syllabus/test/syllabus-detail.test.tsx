import React, { useState } from "react"

import { SyllabusDetailContext } from "@/contexts/syllabus-detail-provider"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { render, screen, waitFor } from "@testing-library/react"
import axios from "axios"
import { BrowserRouter as Router } from "react-router-dom"

import { Syllabus, TrainingUnit } from "@/lib/types"

import { exampleSyllabus } from "@/components/local/Syllabus/tests/sampleSyllabus"

import SyllabusDetail from "../syllabus-detail"

jest.mock("axios")

describe("SyllabusDetail", () => {
  beforeEach(() => {
    axios.get = jest.fn().mockResolvedValue({ data: exampleSyllabus })
  })

  test("renders syllabus detail correctly", async () => {
    const MockComponent = () => {
      const queryClient = new QueryClient()
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
          <QueryClientProvider client={queryClient}>
            <Router>
              <SyllabusDetail />
            </Router>
          </QueryClientProvider>
        </SyllabusDetailContext.Provider>
      )
    }
    render(<MockComponent />)
    await waitFor(() => {
      expect(screen.getByText("Syllabus")).toBeInTheDocument()
    })
    expect(screen.getByText(exampleSyllabus.topicName)).toBeInTheDocument()
    expect(screen.getByText(exampleSyllabus.publishStatus)).toBeInTheDocument()
    expect(
      screen.getByText(
        `v${exampleSyllabus.version} ${exampleSyllabus.topicCode}`
      )
    ).toBeInTheDocument()
  })

  //   test("allows toggling between tabs", async () => {
  //     const queryClient = new QueryClient()

  //     render(
  //       <QueryClientProvider client={queryClient}>
  //         <Router>
  //           <SyllabusDetail />
  //         </Router>
  //       </QueryClientProvider>
  //     )

  //     await waitFor(() => {
  //       expect(screen.getByText(/Sample Topic/i)).toBeInTheDocument()
  //     })

  //     userEvent.click(screen.getByText(/Outline/i))
  //     expect(screen.getByText(/Outline Preview/i)).toBeInTheDocument()

  //     userEvent.click(screen.getByText(/Others/i))
  //     expect(screen.getByText(/Time allocation/i)).toBeInTheDocument()
  //   })
})
