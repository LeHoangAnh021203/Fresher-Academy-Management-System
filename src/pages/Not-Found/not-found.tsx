import { Link } from "react-router-dom"

import { Button } from "@/components/global/atoms/button"

const NotFound = () => {
  const goBack = () => {
    window.history.back()
  }

  return (
    <div className="grid h-screen w-full place-content-center bg-white px-4 font-medium">
      <div className="text-center">
        <h1 className="text-9xl  font-bold text-gray-200">404</h1>
        <p className="text-2xl font-bold tracking-tight text-gray-900 sm:text-4xl">
          Uh-oh!
        </p>
        <p className="mt-4 text-gray-500">
          Something went wrong. Please try again later.
        </p>
        <div className="mt-4 flex items-center space-x-2">
          <Button variant="ghost" onClick={goBack}>
            Go back to the previous page
          </Button>
          <Link to="/">
            <Button>Home Page</Button>
          </Link>
        </div>
      </div>
    </div>
  )
}

export default NotFound
