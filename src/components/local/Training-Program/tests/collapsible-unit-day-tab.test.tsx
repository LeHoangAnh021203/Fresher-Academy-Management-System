import React from "react"

import { render } from "@testing-library/react"

import { CollapsibleTrainingContentTab } from "../collapsible-training-content-tab"

describe("CollapsibleTrainingContentTab", () => {
  const testData = {
    content: "Test Training",
    duration: 30,
    trainingFormat: "Online"
  }

  it("renders training content correctly", () => {
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={testData} index={1} />
    )

    expect(getByText(testData.content)).toBeInTheDocument()
    expect(getByText(`${testData.duration} mins`)).toBeInTheDocument()
    expect(getByText(testData.trainingFormat)).toBeInTheDocument()
  })

  it('displays "Offline" format correctly', () => {
    const offlineData = {
      ...testData,
      trainingFormat: "Offline"
    }
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={offlineData} index={2} />
    )

    expect(getByText("Offline")).toBeInTheDocument()
    expect(getByText("Offline")).toHaveClass("border-red-400")
  })

  it("displays action buttons", () => {
    const { getByLabelText } = render(
      <CollapsibleTrainingContentTab data={testData} index={3} />
    )

    expect(getByLabelText("User icon")).toBeInTheDocument()
    expect(getByLabelText("Folder icon")).toBeInTheDocument()
  })

  it("renders with correct class when training format is online", () => {
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={testData} index={4} />
    )

    expect(getByText("Online")).toBeInTheDocument()
    expect(getByText("Online")).toHaveClass("border-green-400")
  })

  it("renders with correct class when training format is not offline", () => {
    const customData = {
      ...testData,
      trainingFormat: "Custom Format"
    }
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={customData} index={5} />
    )

    expect(getByText("Custom Format")).toBeInTheDocument()
    expect(getByText("Custom Format")).toHaveClass("border-green-400")
  })

  it("renders with duration in minutes", () => {
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={testData} index={6} />
    )

    expect(getByText("30 mins")).toBeInTheDocument()
  })

  it("renders with correct text for duration", () => {
    const customDurationData = {
      ...testData,
      duration: 45
    }
    const { getByText } = render(
      <CollapsibleTrainingContentTab data={customDurationData} index={7} />
    )

    expect(getByText("45 mins")).toBeInTheDocument()
  })
})
