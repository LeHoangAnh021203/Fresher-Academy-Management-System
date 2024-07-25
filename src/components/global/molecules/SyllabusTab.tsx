import React from "react"

interface SyllabusTabProps {
  tabs: string[]
  currentTab: number
  onTabClick?: (index: number) => void
}

const SyllabusTab: React.FC<SyllabusTabProps> = ({
  tabs,
  currentTab,
  onTabClick
}) => {
  return (
    <div className="flex gap-[1px]">
      {tabs.map((item, index) => (
        <div
          className={`w-[200px] py-[4px] text-center ${
            currentTab === index + 1 ? "bg-primary" : "bg-primary/70"
          } rounded-t-[15px] text-sm text-white`}
          key={index}
          onClick={() => onTabClick(index + 1)}
        >
          {item}
        </div>
      ))}
    </div>
  )
}

export default SyllabusTab
