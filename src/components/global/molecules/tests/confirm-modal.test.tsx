import { fireEvent, render } from "@testing-library/react"

import { ConfirmModal } from "../Confirm-Modal"

describe("ConfirmModal component", () => {
  let counter = 0
  const onConfirmMock = jest.fn(() => counter++)

  // Reset counter for each test
  beforeEach(() => {
    counter = 0
  })

  it('calls onConfirm callback and increments counter when "Continue" button is clicked', () => {
    // Render the ConfirmModal component
    const { getByText } = render(
      <ConfirmModal onConfirm={onConfirmMock}>
        <button>Open Modal</button>
      </ConfirmModal>
    )

    // Click the button to open the modal
    fireEvent.click(getByText("Open Modal"))

    // Click the "Continue" button in the modal
    fireEvent.click(getByText("Continue"))

    // Assert that the onConfirm callback is called once
    expect(onConfirmMock).toHaveBeenCalledTimes(1)

    // Assert that the counter is incremented to 1
    expect(counter).toBe(1)
  })
})
