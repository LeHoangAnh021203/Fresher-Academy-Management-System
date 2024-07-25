import { AuthContext } from "@/contexts/auth-provider"
import { fireEvent, render, screen } from "@testing-library/react"
import { BrowserRouter as Router } from "react-router-dom"

import Header from "../Header"

describe("Header component", () => {
  beforeEach(() => {
    render(
      <AuthContext.Provider
        value={{
          accessToken: "",
          role: "superAdmin",
          setAccessToken: () => {
            "abc1"
          },
          user: null
        }}
      >
        <Router>
          <Header />
        </Router>
      </AuthContext.Provider>
    )
  })

  it("should render self without crashing", () => {
    expect(screen.getByText("Warrior Tran")).toBeInTheDocument()
  })

  it("should show Fpt logo", () => {
    const fptLogo = screen.getByAltText("FPT Logo")
    expect(fptLogo).toBeInTheDocument()
    expect(fptLogo).toHaveAttribute("src", "/Fpt_Logo.svg")
  })

  it("should show Unigate logo", () => {
    const unigateLogo = screen.getByAltText("Unigate Logo")
    expect(unigateLogo).toBeInTheDocument()
    expect(unigateLogo).toHaveAttribute("src", "/Unigate_logo.svg")
  })

  it("should call setAccessToken with correct params when logout button is clicked", () => {
    fireEvent.click(screen.getByText(/log out/i))
  })
})
