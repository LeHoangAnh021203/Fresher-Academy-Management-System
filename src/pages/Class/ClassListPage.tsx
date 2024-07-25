import { useEffect, useState } from "react"

import { getClasses } from "@/graphql/queries/GetClass"
import { useQuery } from "@tanstack/react-query"
import axios from "axios"

import { IClass } from "@/types/class.interface"

import { Loader } from "@/components/global/atoms/loader"
import Title from "@/components/global/organisms/Title"
import { columns } from "@/components/local/Class/data-table/column"
import { DataTable } from "@/components/local/Class/data-table/data-table"

function ClassListPage() {
  // const { data, isLoading } = useQuery<IClass[]>({
  //   queryKey: ["classes"],
  //   queryFn: getClasses
  // })

  // console.log(data)

  const [classData, setClassData] = useState<IClass[]>([])
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const fetchClassData = async () => {
      setIsLoading(true)
      try {
        const response = await axios.get(
          "https://659f784d5023b02bfe89a64e.mockapi.io/api/v1/classes"
        )
        setClassData(response.data)
      } catch (error) {
        console.error("Error fetching data:", error)
      } finally {
        setIsLoading(false)
      }
    }

    fetchClassData()
  }, [])

  if (isLoading) {
    return <Loader />
  }

  return (
    <section className="min-h-[90vh] w-full ">
      <Title title="Training Class" />
      <div className="py-5">
        <DataTable data={classData!} columns={columns} />
      </div>
    </section>
  )
}

export default ClassListPage
