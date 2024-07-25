import { BookOpen, GraduationCap, MicroscopeIcon } from "lucide-react"

import { useGetAllSyllabus } from "@/apis/syllabus-routes"
import { useGetAllTrainingPrograms } from "@/apis/training-program-routes"

import {
  Card,
  CardContent,
  CardHeader,
  CardTitle
} from "@/components/global/atoms/card"
import { ScrollArea } from "@/components/global/atoms/scroll-area"
import Title2 from "@/components/global/organisms/Title2"
import { RecentUsers } from "@/components/local/Home/recent-users"

const HomePage = () => {
  const trainingPrograms = useGetAllTrainingPrograms()
  const syllabuses = useGetAllSyllabus()

  const totalTP = trainingPrograms.data?.length || 0
  const totalSyl = syllabuses.data?.length || 0

  return (
    <section className="pb-20 flex w-full flex-col">
      <div className="flex-1 space-y-4">
        <div className="flex items-center justify-between space-y-2">
          <Title2 title={"Dashboard"} />
        </div>
        <div className="px-5 grid gap-4 md:grid-cols-2 lg:grid-cols-3">
          <Card>
            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
              <CardTitle className="text-sm font-medium">
                Total Training Program
              </CardTitle>
              <MicroscopeIcon className="h-5 w-5" />
            </CardHeader>
            <CardContent>
              <p className="text-2xl font-bold">{totalTP} programs</p>
            </CardContent>
          </Card>
          <Card>
            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
              <CardTitle className="text-sm font-medium">
                Total Syllabuses
              </CardTitle>
              <BookOpen className="h-5 w-5" />
            </CardHeader>
            <CardContent>
              <p className="text-2xl font-bold">{totalSyl} syllabuses</p>
            </CardContent>
          </Card>
          <Card>
            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
              <CardTitle className="text-sm font-medium">
                Total Classes
              </CardTitle>
              <GraduationCap className="h-5 w-5" />
            </CardHeader>
            <CardContent>
              <p className="text-2xl font-bold">10 classes</p>
            </CardContent>
          </Card>
        </div>
        <div className="grid px-5 gap-4 md:grid-cols-2 lg:grid-cols-7">
          <Card className="col-span-3 ">
            <CardHeader>Recent Users Overview</CardHeader>
            <CardContent className="pr-2 h-full">
              <ScrollArea className="h-[500px]">
                <RecentUsers />
              </ScrollArea>
            </CardContent>
          </Card>
          <Card className="col-span-4 ">
            <CardHeader>Overview</CardHeader>
            <CardContent>Content</CardContent>
          </Card>
        </div>
      </div>
    </section>
  )
}

export default HomePage
