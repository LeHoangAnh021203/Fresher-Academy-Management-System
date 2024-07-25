import { useGetAllUser } from "@/apis/user-routes"

import { Loader } from "@/components/global/atoms/loader"
import Title2 from "@/components/global/organisms/Title2"
import { columns } from "@/components/local/User/data-table/column"
import { DataTable } from "@/components/local/User/data-table/data-table"

import NotFound from "../Not-Found/not-found"

function UserListPage() {
  const { data, isLoading, isError } = useGetAllUser()

  if (isLoading) {
    return <Loader />
  }

  if (isError) {
    return <NotFound />
  }
  return (
    <div className="flex h-full min-h-screen w-full flex-col">
      <Title2 title={"User Management"} />
      <DataTable columns={columns} data={data || []} />
    </div>
  )
}

export default UserListPage
