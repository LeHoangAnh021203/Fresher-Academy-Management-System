import { fireEvent, render, screen, waitFor } from "@testing-library/react"
import userEvent from "@testing-library/user-event"

import { AddUser } from "../add-user-form"

// Mock the console.log function
console.log = jest.fn()

describe("AddUser Component", () => {
  test("renders form with all input fields and controls", () => {
    render(<AddUser />)

    // Assert presence of form input fields and controls
    expect(screen.getByLabelText("User type")).toBeInTheDocument()
    expect(screen.getByLabelText("Name")).toBeInTheDocument()
    expect(screen.getByLabelText("Email address")).toBeInTheDocument()
    expect(screen.getByLabelText("Date of birth")).toBeInTheDocument()
    expect(screen.getByLabelText("Gender")).toBeInTheDocument()
    expect(screen.getByLabelText("Status")).toBeInTheDocument()
    expect(screen.getByRole("button", { name: "Save" })).toBeInTheDocument()
  })

  test("submits form with valid data", async () => {
    render(<AddUser />)

    // Fill form fields with valid data
    userEvent.selectOptions(screen.getByLabelText("User type"), "Trainer")
    userEvent.type(screen.getByLabelText("Name"), "John Doe")
    userEvent.type(screen.getByLabelText("Email address"), "john@example.com")
    userEvent.type(screen.getByLabelText("Date of birth"), "2000-01-01")
    userEvent.click(screen.getByLabelText("Male"))
    userEvent.click(screen.getByLabelText("Active"))

    // Submit the form
    fireEvent.click(screen.getByRole("button", { name: "Save" }))

    // Assert that form data is logged (replace with actual submission verification)
    await waitFor(() => {
      expect(console.log).toHaveBeenCalledWith({
        role: "Trainer",
        name: "John Doe",
        email: "john@example.com",
        dob: "2000-01-01", // Date is converted to string due to optional field
        gender: "Male",
        status: "Active"
      })
    })
  })

  test("displays validation errors for invalid data", async () => {
    render(<AddUser />)

    // Submit the form without filling any fields
    fireEvent.click(screen.getByRole("button", { name: "Save" }))

    // Assert presence of validation errors
    expect(screen.getByText("Name is required")).toBeInTheDocument()
    expect(screen.getByText("Email is required")).toBeInTheDocument()
  })

  test("form resets to default values after submission", async () => {
    render(<AddUser />)

    // Fill form fields with valid data
    userEvent.selectOptions(screen.getByLabelText("User type"), "Trainer")
    userEvent.type(screen.getByLabelText("Name"), "John Doe")
    userEvent.type(screen.getByLabelText("Email address"), "john@example.com")
    userEvent.type(screen.getByLabelText("Date of birth"), "2000-01-01")
    userEvent.click(screen.getByLabelText("Male"))
    userEvent.click(screen.getByLabelText("Active"))

    // Submit the form
    fireEvent.click(screen.getByRole("button", { name: "Save" }))

    // Assert that form is reset to default values
    await waitFor(() => {
      expect(screen.getByLabelText("User type")).toHaveValue("Trainer")
      expect(screen.getByLabelText("Name")).toHaveValue("")
      expect(screen.getByLabelText("Email address")).toHaveValue("")
      expect(screen.getByLabelText("Date of birth")).toHaveValue("")
      expect(screen.getByLabelText("Male")).toBeChecked()
      expect(screen.getByLabelText("Active")).toBeChecked()
    })
  })
})
