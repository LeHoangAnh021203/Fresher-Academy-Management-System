import { render, screen } from "@testing-library/react"

import Title2 from "../Title2"

describe("Title Component", () => {
  test("renders title correctly", () => {
    const testTitle = "Test Title"
    render(<Title2 title={testTitle} />)

    const titleElement = screen.getByText(testTitle)

    expect(titleElement).toBeInTheDocument()
  })
})
