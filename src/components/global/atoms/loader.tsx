import { ClipLoader } from "react-spinners"

export const Loader = () => {
  return (
    <div className="w-full mx-auto flex min-h-screen items-center justify-center">
      <ClipLoader color="#00000" size={70} />
    </div>
  )
}
