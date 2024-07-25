import { fireEvent, render, screen } from "@testing-library/react"
import { BrowserRouter } from "react-router-dom"

import SideBar from "../SideBar"

describe("SideBar Component", () => {
  test("renders menu items", () => {
    render(
      <BrowserRouter>
        <SideBar />
      </BrowserRouter>
    )

    const homeMenuItem = screen.getByText("Home")
    expect(homeMenuItem).toBeInTheDocument()

    const syllabusMenuItem = screen.getByText("Syllabus")
    expect(syllabusMenuItem).toBeInTheDocument()

    // Add more assertions for other menu items as needed...
  })

  test("toggles submenu", () => {
    render(
      <BrowserRouter>
        <SideBar />
      </BrowserRouter>
    )

    const syllabusMenuItem = screen.getByText("Syllabus")
    fireEvent.click(syllabusMenuItem)
    const viewSyllabusSubmenuItem = screen.getByText("View syllabus")
    expect(viewSyllabusSubmenuItem).toBeInTheDocument()

    fireEvent.click(syllabusMenuItem)
    expect(viewSyllabusSubmenuItem).not.toBeInTheDocument()
  })

  test("renders menu items and navigates on click", () => {
    const { getByText } = render(
      <BrowserRouter>
        <SideBar />
      </BrowserRouter>
    )

    // Get the menu item and simulate a click
    const syllabusMenuItem = getByText("Syllabus")
    fireEvent.click(syllabusMenuItem)

    // Get the submenu item and click it
    const viewSyllabusSubmenuItem = getByText("View syllabus")
    fireEvent.click(viewSyllabusSubmenuItem)

    // Assert that the correct page is navigated to
    expect(window.location.pathname).toBe("/syllabus")

    // Add more test cases for other menu items and submenus as needed...
  })
})
