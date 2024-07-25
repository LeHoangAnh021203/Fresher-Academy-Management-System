import { useGetAllSyllabus } from "@/apis/syllabus-routes"

import { Loader } from "@/components/global/atoms/loader"
import { columns } from "@/components/local/Syllabus/data-table/columns"
import { DataTable } from "@/components/local/Syllabus/data-table/data-table"

const SyllabusList = () => {
  const { data, isLoading } = useGetAllSyllabus()

  if (isLoading) {
    return <Loader />
  }
  return (
    <section className="min-h-[90vh] w-full ">
      <h2 className="mb-[30px] px-5 py-[15px] text-2xl font-semibold leading-9 tracking-[4.8px] text-primary">
        Syllabus
      </h2>

      <div className="py-5">
        <DataTable data={data || []} columns={columns} />
      </div>
    </section>
  )
}

export default SyllabusList
