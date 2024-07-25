import { Button } from "@/components/global/atoms/button"

type FooterProps = {
  onCancel: () => void
  onSave: () => void
}

function Footer({ onCancel, onSave }: FooterProps) {
  return (
    <div className="flex w-full justify-end px-5">
      <Button
        type="button"
        variant="link"
        className="px-7 font-bold text-[#E74A3B] underline hover:no-underline"
        onClick={onCancel}
      >
        Cancel
      </Button>

      <Button variant="primary" type="submit" className="px-7" onClick={onSave}>
        Save
      </Button>
    </div>
  )
}

export default Footer
