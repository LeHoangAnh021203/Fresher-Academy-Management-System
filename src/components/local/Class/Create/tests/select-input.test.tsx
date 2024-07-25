import { render, screen } from "@testing-library/react"

import SelectInput from "../SelectInput"

describe("SelectInput Component", () => {
  const mockOnChange = jest.fn()
  const selectItems = ["Item 1", "Item 2", "Item 3"]

  test("renders without crashing", () => {
    render(
      <SelectInput
        placeHolder="Select item"
        selectItems={selectItems}
        type="test"
        onChange={mockOnChange}
      />
    )
    expect(screen.getByText("Select item")).toBeInTheDocument()
  })
})
