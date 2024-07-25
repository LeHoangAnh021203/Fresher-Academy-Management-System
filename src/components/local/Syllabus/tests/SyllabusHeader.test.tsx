import { render, screen } from "@testing-library/react"

import { SyllabusHeader } from "../SyllabusHeader"

describe("SyllabusHeader component", () => {
  test("render syllabus header", () => {
    render(
      <SyllabusHeader
        title="Syllabus"
        status="active"
        code="T1"
        version={2}
        onEdit={() => {}}
      />
    )
    // Use a custom text matcher function to find the element containing the text
    expect(
      screen.getByText((content, element) => {
        // Check if the text is found within an element with class "text-2xl"
        return element.classList.contains("text-2xl") && content === "Syllabus"
      })
    ).toBeInTheDocument()
  })
})
