import { render } from "@testing-library/react"

import ProgressBar from "../ProgressBar"

describe("ProgressBar component", () => {
  it("should render correctly with default props", () => {
    const { getByText } = render(<ProgressBar currentStep={0} />)

    // Check overall structure
    expect(getByText("General")).toBeInTheDocument()
    expect(getByText("Outline")).toBeInTheDocument()
    expect(getByText("Other")).toBeInTheDocument()
    expect(getByText("Done")).toBeInTheDocument()
  })

  it("should render correctly with color in className", () => {
    const { getAllByRole } = render(<ProgressBar currentStep={1} />)
    const steps = getAllByRole("step")
    expect(steps[0]).toHaveClass("bg-primary")
  })
})
