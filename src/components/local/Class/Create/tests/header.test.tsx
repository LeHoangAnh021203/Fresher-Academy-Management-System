import "@testing-library/jest-dom"
import { render, screen } from "@testing-library/react"

import Header from "../Header"

describe("Header Component", () => {
  const testClassName = "Test Class"

  test("renders with class name", () => {
    render(<Header className={testClassName} />)
    expect(screen.getByText(testClassName)).toBeInTheDocument()
  })

  test("renders '0 days' when days is not provided", () => {
    render(<Header className={testClassName} />)
    expect(screen.getByText("0 days")).toBeInTheDocument()
  })

  test("renders correct number of days", () => {
    const testDays = 5
    render(<Header className={testClassName} days={testDays} />)
    expect(screen.getByText(`${testDays} days`)).toBeInTheDocument()
  })
})
