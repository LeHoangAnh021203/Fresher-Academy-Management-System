import { render, screen } from "@testing-library/react"

import { ProgramSubHeader } from "../program-subheader"

describe("ProgramSubHeader Component", () => {
  test("does not render Skeleton when modifiedOn and modifiedBy are provided", () => {
    render(
      <ProgramSubHeader
        days={5}
        hours={30.5}
        modifiedOn="2022-03-21"
        modifiedBy="John Doe"
      />
    )

    const skeletonElement = screen.queryByTestId("skeleton")
    expect(skeletonElement).not.toBeInTheDocument()
  })

  // test("renders the correct number of days", () => {
  //   render(
  //     <ProgramSubHeader
  //       days={10}
  //       hours={50.25}
  //       modifiedOn="2022-03-21"
  //       modifiedBy="John Doe"
  //     />
  //   )

  //   const daysElement = screen.getByText("10 days")
  //   expect(daysElement).toBeInTheDocument()
  // })

  test("renders the correct number of hours with two decimal places", () => {
    render(
      <ProgramSubHeader
        days={10}
        hours={50.25}
        modifiedOn="2022-03-21"
        modifiedBy="John Doe"
      />
    )

    const hoursElement = screen.getByText("(50.25 hours)")
    expect(hoursElement).toBeInTheDocument()
  })

  test("renders no modified information when modifiedOn and modifiedBy are not provided", () => {
    render(<ProgramSubHeader days={10} hours={50.25} />)

    const modifiedOnElement = screen.queryByText("Modified on")
    const modifiedByElement = screen.queryByText("by")

    expect(modifiedOnElement).not.toBeInTheDocument()
    expect(modifiedByElement).not.toBeInTheDocument()
  })

  test("renders the correct number of hours with two decimal places", () => {
    render(
      <ProgramSubHeader
        days={10}
        hours={50.25}
        modifiedOn="2022-03-21"
        modifiedBy="John Doe"
      />
    )

    const hoursElement = screen.getByText("(50.25 hours)")
    expect(hoursElement).toBeInTheDocument()
  })
})
