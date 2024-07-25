import { render } from "@testing-library/react"

import CreateSyllabusHeader from "./CreateSyllabusHeader"

describe("CreateSyllabusHeader component", () => {
  it("renders correctly", () => {
    const mockControl = {
      control: {
        setValue: jest.fn(),
        register: jest.fn()
      }
    }

    const { getByText, getByPlaceholderText } = render(
      <CreateSyllabusHeader form={mockControl} />
    )

    expect(getByPlaceholderText("Syllabus Name*")).toBeInTheDocument()
    expect(getByPlaceholderText("Code*")).toBeInTheDocument()
    expect(getByText("Version 1.0")).toBeInTheDocument()
  })
})
