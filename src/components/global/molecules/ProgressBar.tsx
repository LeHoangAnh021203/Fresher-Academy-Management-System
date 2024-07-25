import React from "react"

interface ProgressBarProps {
  currentStep: number
}

const ProgressBar: React.FC<ProgressBarProps> = ({ currentStep }) => {
  const items = [
    { title: "General", color: "bg-primary" },
    { title: "Outline", color: "bg-ghost-white" },
    { title: "Other", color: "bg-orange" },
    { title: "Done", color: "bg-green" }
  ]

  return (
    <div className="mb-8 flex w-96 items-center" role="progressbar">
      {items.map((item, index) => (
        <>
          <div
            className={`h-3 flex-1 transition duration-500 ease-in-out ${
              currentStep >= index + 1
                ? items[currentStep - 1].color
                : "bg-gray-300"
            } ${index === 0 && "rounded-l-full"}`}
            key={index}
            role="step"
          ></div>
          <div className="relative flex items-center bg-primary ">
            <div
              className={`absolute left-[-6px] h-3 w-3 rounded-full border-2 transition duration-500  ease-in-out  ${
                currentStep == index + 1
                  ? "border-primary bg-white"
                  : "border-none bg-none"
              }`}
            ></div>
            <div className="absolute top-0 -ml-16 mt-2 w-32 text-center text-xs font-medium  text-black">
              {item.title}
            </div>
          </div>
        </>
      ))}
      <div
        className={`h-3 flex-auto rounded-r-full transition duration-500 ease-in-out ${
          currentStep >= items.length ? "bg-primary" : "bg-gray-300"
        }`}
      ></div>
    </div>
  )
}

export default ProgressBar
