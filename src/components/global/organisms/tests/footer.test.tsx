import { render } from "@testing-library/react"

import Footer from "@/components/global/organisms/Footer"

describe("Footer component", () => {
  test("renders current year in copyright text", () => {
    const { getByText } = render(<Footer />)
    const currentYear = new Date().getFullYear()
    const copyrightText = getByText(
      `Copyright © ${currentYear} BA Warrior. All Rights Reserved.`
    )
    expect(copyrightText).toBeInTheDocument()
  })
})
