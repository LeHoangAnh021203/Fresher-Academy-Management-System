import { useGetAllTrainingPrograms } from "@/apis/training-program-routes"

import { Loader } from "@/components/global/atoms/loader"
import Title from "@/components/global/organisms/Title"
import { columns } from "@/components/local/Training-Program/data-table/columns"
import { DataTable } from "@/components/local/Training-Program/data-table/data-table"

import NotFound from "../Not-Found/not-found"

const TrainingProgramList = () => {
  const { data, isLoading, isError } = useGetAllTrainingPrograms()

  if (isLoading) {
    return <Loader />
  }

  if (isError) {
    return <NotFound />
  }

  return (
    <section className="min-h-[90vh] w-full ">
      <Title title="Training program" />

      <div className="py-5">
        <DataTable data={data || []} columns={columns} />
      </div>
    </section>
  )
}

export default TrainingProgramList
