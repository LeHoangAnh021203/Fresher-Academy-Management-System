import { render, screen } from "@testing-library/react"

import { CollapsibleTrainingContentTab } from "../collapsible-training-content-tab"

describe("CollapsibleTrainingContentTab", () => {
  const testData = {
    content: "Test Training",
    duration: 30,
    trainingFormat: "Online"
  }

  test("renders training content correctly", () => {
    render(<CollapsibleTrainingContentTab data={testData} index={0} />)
    expect(screen.getByText("Test Training")).toBeInTheDocument()
    expect(screen.getByText("30 mins")).toBeInTheDocument()
    expect(screen.getByText("Online")).toBeInTheDocument()
  })

  test("displays correct format styling based on training format", () => {
    const offlineData = { ...testData, trainingFormat: "Offline" }
    render(<CollapsibleTrainingContentTab data={offlineData} index={0} />)
    expect(screen.getByText("Offline")).toHaveClass("border-red-400")
    expect(screen.getByText("Offline")).toHaveClass("bg-red-100")
    expect(screen.getByText("Offline")).toHaveClass("text-red-800")
  })

  // test("renders user and folder icons", () => {
  //   render(<CollapsibleTrainingContentTab data={testData} index={0} />)
  //   expect(screen.getByRole("button", { name: /user/i })).toBeInTheDocument()
  //   expect(screen.getByRole("button", { name: /folder/i })).toBeInTheDocument()
  // })

  test("renders K6SD badge with correct text", () => {
    render(<CollapsibleTrainingContentTab data={testData} index={0} />)
    expect(screen.getByText("K6SD")).toBeInTheDocument()
  })

  test("renders different index correctly", () => {
    const testData2 = { ...testData, content: "Second Training", duration: 45 }
    render(<CollapsibleTrainingContentTab data={testData2} index={1} />)
    expect(screen.getByText("Second Training")).toBeInTheDocument()
    expect(screen.getByText("45 mins")).toBeInTheDocument()
  })

  test("handles long training content names gracefully", () => {
    const longContent = {
      content:
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut eget elit tincidunt, convallis nunc nec, lacinia ligula.",
      duration: 60,
      trainingFormat: "Offline"
    }
    render(<CollapsibleTrainingContentTab data={longContent} index={0} />)
    expect(
      screen.getByText(
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut eget elit tincidunt, convallis nunc nec, lacinia ligula."
      )
    ).toBeInTheDocument()
  })

  test("handles missing content gracefully", () => {
    const missingContentData = { ...testData, content: undefined }
    render(
      <CollapsibleTrainingContentTab data={missingContentData} index={0} />
    )
    expect(screen.queryByText("Test Training")).not.toBeInTheDocument()
  })
})
