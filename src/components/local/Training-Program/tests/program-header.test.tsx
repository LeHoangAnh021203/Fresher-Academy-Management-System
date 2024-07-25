import { render, waitFor } from "@testing-library/react"

import { ProgramHeader } from "../program-header"

describe("ProgramHeader component", () => {
  const mockProps = {
    title: "Test Program",
    status: "Active",
    onEdit: jest.fn(),
    onDelete: jest.fn(),
    onCopy: jest.fn(),
    onActivate: jest.fn()
  }
  test("renders title and status correctly", () => {
    const title = "Sample Program"
    const status = "Active"

    const { getByText } = render(
      <ProgramHeader title={title} status={status} />
    )

    expect(getByText(title)).toBeInTheDocument()
    expect(getByText(status)).toBeInTheDocument()
  })

  test("renders skeleton for title when title is not provided", () => {
    const { getByTestId } = render(
      <ProgramHeader {...mockProps} title={undefined} />
    )
    expect(getByTestId("skeleton-title")).toBeInTheDocument()
  })

  test("does not render dropdown menu when program title is not provided", () => {
    const { queryByText } = render(
      <ProgramHeader {...mockProps} title={undefined} />
    )
    expect(queryByText("Manage")).toBeNull()
  })

  test("does not render dropdown menu when program status is not provided", () => {
    const { queryByText } = render(
      <ProgramHeader {...mockProps} status={undefined} />
    )
    expect(queryByText("Manage")).toBeNull()
  })

  test("renders within acceptable time frame", async () => {
    const { getByText } = render(<ProgramHeader {...mockProps} />)
    const startTime = performance.now()
    await waitFor(() => expect(getByText("Test Program")).toBeInTheDocument())
    const endTime = performance.now()
    expect(endTime - startTime).toBeLessThanOrEqual(1000) // Assuming 1 second is acceptable
  })
})
