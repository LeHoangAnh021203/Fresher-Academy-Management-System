import { Button } from "@/components/global/atoms/button"

interface NavigationBarProps {
  currentStep: number
  steps: {
    name: string
    id: string
    fields: string[]
  }[]
  next: () => void
  prev: () => void
}

export const NavigationBar = ({
  currentStep,
  steps,
  next,
  prev
}: NavigationBarProps) => {
  return (
    <div className="m-4 flex justify-between">
      <Button variant="primary" onClick={prev} disabled={currentStep === 0}>
        Back
      </Button>
      <div>
        <Button type="button" variant="link">
          Cancel
        </Button>
        <Button
          variant="primary"
          type={currentStep === steps.length - 1 ? "submit" : "button"}
          onClick={next}
        >
          {currentStep === steps.length - 1 ? "Save" : "Next"}
        </Button>
      </div>
    </div>
  )
}
