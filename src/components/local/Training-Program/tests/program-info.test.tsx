import { render, screen } from "@testing-library/react"

import { ProgramInfo } from "../program-info"

describe("ProgramInfo Component", () => {
  test("renders general information correctly", () => {
    render(<ProgramInfo id="123456" name="Test Program" />)

    const programIdElement = screen.getByText("Program ID: 123456")
    const programNameElement = screen.getByText("Program Name: Test Program")

    expect(programIdElement).toBeInTheDocument()
    expect(programNameElement).toBeInTheDocument()
  })

  test("renders no Skeleton when id and name are provided", () => {
    render(<ProgramInfo id="123456" name="Test Program" />)

    const skeletonElements = screen.queryAllByTestId("skeleton")
    expect(skeletonElements).toHaveLength(0)
  })

  test("renders no Skeleton when id and name are provided", () => {
    render(<ProgramInfo id="123456" name="Test Program" />)

    const skeletonElements = screen.queryAllByTestId("skeleton")
    expect(skeletonElements).toHaveLength(0)
  })

  // test("renders correct number of Skeletons when only name is not provided", () => {
  //   render(<ProgramInfo id="123456" />)

  //   const skeletonElements = screen.getAllByTestId("skeleton")
  //   expect(skeletonElements).toHaveLength(1)
  // })

  test("renders no Skeleton when both id and name are provided", () => {
    render(<ProgramInfo id="123456" name="Test Program" />)

    const skeletonElements = screen.queryAllByTestId("skeleton")
    expect(skeletonElements).toHaveLength(0)
  })

  test("renders no Skeleton when id and name are empty strings", () => {
    render(<ProgramInfo id="" name="" />)

    const skeletonElements = screen.queryAllByTestId("skeleton")
    expect(skeletonElements).toHaveLength(0)
  })
})
