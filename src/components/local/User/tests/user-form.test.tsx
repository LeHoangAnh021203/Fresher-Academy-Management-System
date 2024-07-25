import { fireEvent, render, screen, waitFor } from "@testing-library/react"
import userEvent from "@testing-library/user-event"
import axios from "axios"

import { UserForm } from "../update-user-form"

// Mock useMutation
jest.mock("@tanstack/react-query", () => ({
  useMutation: jest.fn()
}))

// Mock useQueryClient
jest.mock("react-query", () => ({
  useQueryClient: jest.fn(() => ({
    invalidateQueries: jest.fn(),
    resetQueries: jest.fn()
  }))
}))

// Mock axios
jest.mock("axios")

describe("UserForm Component", () => {
  const initData = {
    userId: "1",
    role: "Trainer",
    name: "John Doe",
    email: "john@example.com",
    dob: "2000-01-01",
    gender: "Male",
    status: "Active"
  }

  test("renders form with initial data", () => {
    render(<UserForm initData={initData} />)

    // Assert presence of form input fields with initial values
    expect(screen.getByLabelText("User type")).toHaveValue("Trainer")
    expect(screen.getByLabelText("Name")).toHaveValue("John Doe")
    expect(screen.getByLabelText("Email address")).toHaveValue(
      "john@example.com"
    )
    expect(screen.getByLabelText("Date of birth")).toHaveValue("2000-01-01")
    expect(screen.getByLabelText("Male")).toBeChecked()
    expect(screen.getByLabelText("Active")).toBeChecked()
  })

  test("submits form with updated data", async () => {
    render(<UserForm initData={initData} />)

    // Simulate updating form fields
    userEvent.clear(screen.getByLabelText("Name"))
    userEvent.type(screen.getByLabelText("Name"), "John Smith")

    // Mock axios.put to resolve with updated data
    axios.put.mockResolvedValueOnce({
      data: { ...initData, name: "John Smith" }
    })

    // Submit the form
    fireEvent.click(screen.getByRole("button", { name: "Save" }))

    // Wait for form submission
    await waitFor(() => {
      // Verify that axios.put is called with updated data
      expect(axios.put).toHaveBeenCalledWith(
        `https://648867740e2469c038fda6cc.mockapi.io/api/v1/user/${initData.userId}`,
        { ...initData, name: "John Smith" }
      )
      // Verify success message
      expect(screen.getByText("User updated successfully")).toBeInTheDocument()
    })
  })

  test("navigates to users page after successful submission", async () => {
    const navigateMock = jest.fn()
    jest.mock("react-router-dom", () => ({
      ...jest.requireActual("react-router-dom"),
      useNavigate: () => navigateMock
    }))

    render(<UserForm initData={initData} />)

    // Mock axios.put to resolve with updated data
    axios.put.mockResolvedValueOnce({ data: { ...initData } })

    // Submit the form
    fireEvent.click(screen.getByRole("button", { name: "Save" }))

    // Wait for navigation
    await waitFor(() => {
      // Verify navigation to users page
      expect(navigateMock).toHaveBeenCalledWith("/users")
    })
  })

  // Add more test cases as needed...
})
