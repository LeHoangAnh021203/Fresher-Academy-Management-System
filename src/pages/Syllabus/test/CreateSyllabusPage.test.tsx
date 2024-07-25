import { fireEvent, render, screen } from "@testing-library/react"
import { describe } from "node:test"

import CreateSyllabusPage from "../CreateSyllabusPage"

describe("CreateSyllabusPage component", () => {
  test("renders create syllabus page", () => {
    render(<CreateSyllabusPage />)
    expect(screen.getByText("Syllabus"))
  })
  test("input Syllabus name", () => {
    const { getByPlaceholderText } = render(<CreateSyllabusPage />)
    const inputField = getByPlaceholderText("Syllabus Name*")
    expect(inputField).toBeInTheDocument()
  })
  test("Typing into input syllabus name field", () => {
    const { getByPlaceholderText } = render(<CreateSyllabusPage />)
    const inputField = getByPlaceholderText("Syllabus Name*")
    fireEvent.change(inputField, { target: { value: "New Syllabus" } })
    expect(inputField).toHaveValue("New Syllabus")
  })
  test("input Syllabus Code", () => {
    const { getByPlaceholderText } = render(<CreateSyllabusPage />)
    const inputField = getByPlaceholderText("Code*")
    expect(inputField).toBeInTheDocument()
  })
  test("Typing into input syllabus code field", () => {
    const { getByPlaceholderText } = render(<CreateSyllabusPage />)
    const inputField = getByPlaceholderText("Code*")
    fireEvent.change(inputField, { target: { value: "T01" } })
    expect(inputField).toHaveValue("T01")
  })
  test("Button compobox", () => {
    const { getByRole } = render(<CreateSyllabusPage />)
    const buttonField = getByRole("combobox")
    expect(buttonField).toBeInTheDocument()
  })
  test("Select different values from button compobox", () => {
    render(<CreateSyllabusPage />)
    const buttonField = screen.getByRole("combobox")
    fireEvent.click(buttonField)
    const beginner = screen.getByText("Beginner")
    fireEvent.click(beginner)
    expect(beginner).toBeInTheDocument()
  })
  // test("Add Day in outline tab", () => {
  //   render(<CreateSyllabusPage />)
  //   const inputTech = document.querySelector(".ql-editor")
  //   if (inputTech) {
  //     fireEvent.change(inputTech, {
  //       target: { textContent: "Enter" }
  //     })
  //   } else {
  //     throw new Error("Quill editor not found")
  //   }
  //   expect(inputTech).toHaveValue("aaa")
  // const buttonNext = screen.getByRole("buttonNext")
  // fireEvent.click(buttonNext)
  // expect(screen.getByRole("btnAddDay")).toBeInTheDocument()
  // expect(screen.getByText("Test"))
  // expect(screen.getByText("Unit 1"))
  // expect(screen.getByRole("DeleteDay"))
  // expect(screen.getByRole("Add"))
  // })
})
