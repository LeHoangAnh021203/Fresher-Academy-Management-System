import AuthProvider from "@/contexts/auth-provider"
import { render } from "@testing-library/react"
import { MemoryRouter, Route, Routes } from "react-router-dom"

import AuthLayout from "./AuthLayout"

describe("AuthLayout Component", () => {
  test("Redirects to login page when accessToken is null", () => {
    // Mock authentication context values
    const authContextValues = {
      accessToken: null
    }

    const { queryByTestId } = render(
      <MemoryRouter initialEntries={["/dashboard"]} initialIndex={0}>
        <AuthProvider {...authContextValues}>
          <Routes>
            <Route path="/dashboard" element={<AuthLayout />} />
            <Route
              path="/dashboard/home"
              element={<div data-testid="home-page" />}
            />
            <Route
              path="/dashboard/profile"
              element={<div data-testid="profile-page" />}
            />
          </Routes>
        </AuthProvider>
      </MemoryRouter>
    )

    // Assert that component redirects to login page when accessToken is null
    expect(queryByTestId("home-page")).not.toBeInTheDocument()
    expect(queryByTestId("profile-page")).not.toBeInTheDocument()
    // Add assertions for redirection to the login page
  })

  test("Redirects to home page when user is authenticated", () => {
    const authContextValues = {
      accessToken: "mockAccessToken"
    }

    const { queryByTestId } = render(
      <MemoryRouter initialEntries={["/dashboard"]} initialIndex={0}>
        <AuthProvider {...authContextValues}>
          <Routes>
            <Route path="/dashboard" element={<AuthLayout />} />
            <Route path="/other" element={<div data-testid="other-page" />} />
          </Routes>
        </AuthProvider>
      </MemoryRouter>
    )

    expect(queryByTestId("other-page")).not.toBeInTheDocument()
  })
})
