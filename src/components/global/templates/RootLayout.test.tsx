import AuthProvider from "@/contexts/auth-provider"
import { render } from "@testing-library/react"
import { MemoryRouter } from "react-router-dom"

import RootLayout from "./RootLayout"

describe("RootLayout Component", () => {
  test("Redirects to login page if user is not authenticated", () => {
    const authContextValues = {
      role: null,
      accessToken: null
    }

    // Render the RootLayout component within a MemoryRouter and AuthProvider
    const { queryByTestId } = render(
      <MemoryRouter initialEntries={["/dashboard"]} initialIndex={0}>
        <AuthProvider {...authContextValues}>
          <RootLayout />
        </AuthProvider>
      </MemoryRouter>
    )

    // Assert that the main content elements are not rendered
    expect(queryByTestId("header")).not.toBeInTheDocument()
    expect(queryByTestId("sidebar")).not.toBeInTheDocument()
    expect(queryByTestId("footer")).not.toBeInTheDocument()
  })

  test("Redirects to login page if user role is not allowed", () => {
    // Mock authentication context values
    const authContextValues = {
      role: "user", // Assuming user role is not allowed
      accessToken: "mockAccessToken"
    }

    // Render the RootLayout component within a MemoryRouter and AuthProvider
    const { queryByTestId } = render(
      <MemoryRouter initialEntries={["/dashboard"]} initialIndex={0}>
        <AuthProvider {...authContextValues}>
          <RootLayout />
        </AuthProvider>
      </MemoryRouter>
    )

    // Assert that the main content elements are not rendered
    expect(queryByTestId("header")).not.toBeInTheDocument()
    expect(queryByTestId("sidebar")).not.toBeInTheDocument()
    expect(queryByTestId("footer")).not.toBeInTheDocument()
  })
})
