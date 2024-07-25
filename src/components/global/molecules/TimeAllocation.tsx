import {
  ArcElement,
  Chart as ChartJS,
  ChartOptions,
  Legend,
  Tooltip
} from "chart.js"
import { Pie } from "react-chartjs-2"

ChartJS.register(ArcElement, Tooltip, Legend)

interface CircleChartProps {
  items: {
    values: number[]
    labels: string[]
  }
  title: string
  horizontal: boolean
  state?: string | "default"
}

interface PieChartOptions extends ChartOptions<"pie"> {
  plugins?: {
    datalabels?: any
    legend?: any
  }
  cutoutPercentage?: any
}

const TimeAllocationTable = ({
  items,
  title,
  horizontal,
  state
}: CircleChartProps) => {
  const backgroundColor = {
    default: ["#F4BE37", "#FF9F40", "#0D2535", "#5388D8", "#7FACF0"],
    disabled: ["#F4BE37", "#FF9F40", "#0D2535", "#5388D8", "#7FACF0"].map(
      (hexColor) => hexColor + "80"
    )
  }

  const chartData = {
    labels: items.labels,
    datasets: [
      {
        data: items.values,
        backgroundColor:
          backgroundColor[state as keyof typeof backgroundColor] ||
          backgroundColor.default
      }
    ]
  }

  const chartOption: PieChartOptions = {
    responsive: true,
    plugins: {
      datalabels: {
        display: false
      },
      legend: {
        position: horizontal ? "right" : "bottom",
        fullSize: true,
        align: horizontal ? "center" : "start",
        layout: {
          padding: {
            left: 50
          }
        },
        labels: {
          font: {
            size: horizontal ? 14 : 12,
            weight: "500",
            lineHeight: 1.5
          },
          boxWidth: 6,
          boxHeight: 6,
          usePointStyle: true,
          generateLabels: function (chart: any) {
            const data = chart.data
            if (data.labels.length && data.datasets.length) {
              return data.labels.map(function (label: any, i: number) {
                const dataset = data.datasets[0]
                const value = dataset.data[i]
                const totalSum = dataset.data.reduce(
                  (accumulator: any, currentValue: any) => {
                    return accumulator + currentValue
                  },
                  0
                )
                const percentage = (value / totalSum) * 100
                return {
                  text: `${label} \n ${percentage.toFixed(0)}% `,
                  fillStyle: dataset.backgroundColor[i],
                  hidden: isNaN(dataset.data[i]) || dataset.data[i] === 0,
                  index: i
                }
              })
            } else {
              return []
            }
          }
        },
        onClick: (e: any) => {
          e.stopPropagation()
        }
      }
    },
    aspectRatio: horizontal ? 2 : 0.7,
    layout: {
      padding: {
        left: horizontal ? 0 : 5,
        right: horizontal ? 0 : 5,
        bottom: horizontal ? 0 : 0,
        top: horizontal ? 0 : 10
      }
    }
  }

  return (
    <div
      id="time_allocation_table"
      className={`flex flex-col ${horizontal ? "h-[400px] w-[40rem]" : "h-[450px] w-[12rem]"} items-center justify-start rounded-bl-xl rounded-br-xl bg-white shadow-xl `}
    >
      <>
        {items.values.some((value) => value !== 0) ? (
          <>
            <div className="flex h-10 w-full items-center justify-center rounded-t-[15px] bg-primary font-medium text-white">
              {title}
            </div>
            <div className="my-auto flex w-full flex-col">
              <Pie data={chartData} options={chartOption} />
            </div>
          </>
        ) : (
          <>
            <div className="flex h-10 w-full items-center justify-center rounded-t-[15px] bg-primary font-medium text-white">
              {title}
            </div>
            <div className="flex h-full flex-col items-center justify-center">
              No data
            </div>
          </>
        )}
      </>
    </div>
  )
}

export default TimeAllocationTable
