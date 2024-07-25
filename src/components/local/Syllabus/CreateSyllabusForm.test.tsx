import { fireEvent, render, waitFor } from "@testing-library/react"

import CreateSyllabusForm from "./CreateSyllabusForm"

describe("CreateSyllabusForm component", () => {
  it("renders correctly", () => {
    const { getByText, getByPlaceholderText } = render(
      <CreateSyllabusForm handleCreateContent={() => {}} />
    )

    // Assert that necessary elements are rendered
    expect(getByText("Add a new user")).toBeInTheDocument()
    expect(getByPlaceholderText("Name of content")).toBeInTheDocument()
    expect(getByPlaceholderText("Minutes")).toBeInTheDocument()
    expect(getByText("Select one")).toBeInTheDocument()
    expect(getByText("Online")).toBeInTheDocument()
    expect(getByText("Offline")).toBeInTheDocument()
    expect(getByText("Save")).toBeInTheDocument()
  })

  it("submits form with correct data", async () => {
    const handleCreateContentMock = jest.fn()

    const { getByPlaceholderText, getByText } = render(
      <CreateSyllabusForm handleCreateContent={handleCreateContentMock} />
    )

    // Fill in form fields
    fireEvent.change(getByPlaceholderText("Name of content"), {
      target: { value: "Test Content" }
    })
    fireEvent.change(getByPlaceholderText("Minutes"), {
      target: { value: "60" }
    })
    fireEvent.click(getByText("Select one"))
    fireEvent.click(getByText("Assignment/Lab"))
    fireEvent.click(getByText("Online"))
    fireEvent.click(getByText("Save"))

    // Wait for form submission
    await waitFor(() => {
      expect(handleCreateContentMock).toHaveBeenCalledWith({
        content: "Test Content",
        deliveryType: "Assignment/Lab",
        trainingFormat: "Online",
        duration: 60,
        learningObjectives: [] // Assuming learning objectives are empty initially
      })
    })
  })
})
