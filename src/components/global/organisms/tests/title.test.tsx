import { render, screen } from "@testing-library/react"

import Title from "../Title"

describe("Title Component", () => {
  test("renders title correctly", () => {
    const testTitle = "Test Title"
    render(<Title title={testTitle} />)

    const titleElement = screen.getByText(testTitle)

    expect(titleElement).toBeInTheDocument()
  })
})
