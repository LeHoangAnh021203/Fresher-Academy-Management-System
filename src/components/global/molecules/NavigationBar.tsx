import { Button } from "@/components/global/atoms/button"

interface NavigationBarProps {
  currentStep: number
  maxStep: number
  next: () => void
  prev: () => void
}

export const NavigationBar = ({
  currentStep,
  maxStep,
  next,
  prev
}: NavigationBarProps) => {
  return (
    <div className=" flex justify-between px-2">
      <Button variant="primary" onClick={prev} disabled={currentStep === 1}>
        Back
      </Button>
      <div>
        <Button type="button" variant="link">
          Cancel
        </Button>
        <Button
          role="buttonNext"
          variant="primary"
          type={currentStep === maxStep ? "submit" : "button"}
          onClick={next}
        >
          {currentStep === maxStep ? "Save" : "Next"}
        </Button>
      </div>
    </div>
  )
}
