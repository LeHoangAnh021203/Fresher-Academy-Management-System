import { render, screen } from "@testing-library/react"
import { BrowserRouter } from "react-router-dom"

import { DataTable } from "./data-table"

describe("DataTable Component", () => {
  test("Renders correctly with data", () => {
    // Mock data and props
    const columns = [
      { header: "Name", accessor: "name" },
      { header: "Age", accessor: "age" }
    ]
    const data = [
      { name: "John", age: 30 },
      { name: "Jane", age: 25 }
    ]

    // Render the DataTable component with mocked data and props
    render(
      <BrowserRouter>
        <DataTable columns={columns} data={data} />
      </BrowserRouter>
    )

    // Assert that table headers and data are rendered
    expect(screen.getByText("Name")).toBeInTheDocument()
    expect(screen.getByText("Age")).toBeInTheDocument()
    expect(screen.getByText("John")).toBeInTheDocument()
    expect(screen.getByText("Jane")).toBeInTheDocument()
    expect(screen.getByText("30")).toBeInTheDocument()
    expect(screen.getByText("25")).toBeInTheDocument()
  })

  test("Renders 'No results' message when no data is provided", () => {
    // Mock data and props with no data
    const columns = [
      { header: "Name", accessor: "name" },
      { header: "Age", accessor: "age" }
    ]
    const data: [] = [] // Correct empty data array

    // Render the DataTable component with no data
    render(
      <BrowserRouter>
        <DataTable columns={columns} data={data} />
      </BrowserRouter>
    )

    // Assert that the "No results" message is rendered
    expect(screen.getByText("No results.")).toBeInTheDocument()
  })

  test("Renders loading skeleton when data is being fetched", () => {
    // Mock data and props with no data initially
    const columns = [
      { header: "Name", accessor: "name" },
      { header: "Age", accessor: "age" }
    ]
    const data: [] = []

    // Render the DataTable component with no data initially
    render(
      <BrowserRouter>
        <DataTable columns={columns} data={data} />
      </BrowserRouter>
    )

    // Assert that loading skeletons are rendered
    columns.forEach((column) => {
      // Check if the column object and accessor property are defined before attempting to access them
      if (column && column.accessor) {
        expect(
          screen.getByTestId(`loading-skeleton-${column.accessor}`)
        ).toBeInTheDocument()
      }
    })
  })

  // You can add more test cases as needed
})
