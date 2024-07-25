import { useParams } from "react-router-dom"

import { useGetUserById } from "@/apis/user-routes"

import { Loader } from "@/components/global/atoms/loader"
import Title2 from "@/components/global/organisms/Title2"
import { UserForm } from "@/components/local/User/update-user-form"

import NotFound from "../Not-Found/not-found"

const UserDetailPage = () => {
  const { id } = useParams()

  const { data, isLoading, isError } = useGetUserById(id)

  if (isLoading) {
    return <Loader />
  }

  if (isError) {
    return (
      <div className="w-full">
        <NotFound />
      </div>
    )
  }
  return (
    <section className="min-h-[90vh] w-full">
      <Title2 title="User Information" />
      <UserForm initData={data!} />
    </section>
  )
}

export default UserDetailPage
