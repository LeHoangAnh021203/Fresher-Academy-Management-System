import Title2 from "@/components/global/organisms/Title2"
import { UserPermissionTable } from "@/components/local/User/user-permission-table"

const UserPermissionPage = () => {
  return (
    <section className="min-h-[90vh] w-full">
      <Title2 title={"User Permission"} />
      <UserPermissionTable />
    </section>
  )
}

export default UserPermissionPage
