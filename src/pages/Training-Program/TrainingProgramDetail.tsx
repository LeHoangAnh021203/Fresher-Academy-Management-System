import { useEffect, useState } from "react"

import axios from "axios"
import { format } from "date-fns"
import { useNavigate, useParams } from "react-router-dom"

import { Program } from "@/lib/types"
import { calculateTotalDurationOfTrainingProgram } from "@/lib/utils"

import { Card } from "@/components/global/atoms/card"
import { Skeleton } from "@/components/global/atoms/skeleton"
import SyllabusCard from "@/components/global/molecules/SyllabusCard"
import { ProgramHeader } from "@/components/local/Training-Program/program-header"
import { ProgramInfo } from "@/components/local/Training-Program/program-info"
import { ProgramSubHeader } from "@/components/local/Training-Program/program-subheader"

export const TrainingProgramDetail = () => {
  const { id } = useParams()
  const navigate = useNavigate()
  const [program, setProgram] = useState<Program>()
  const [totalDuration, setTotalDuration] = useState<{
    days: number
    learningTime: number
  }>({
    days: 0,
    learningTime: 0
  })

  useEffect(() => {
    const fetchProgram = async () => {
      try {
        const { data } = await axios.get(
          `https://648867740e2469c038fda6cc.mockapi.io/program/${id}`
        )
        setProgram(data)
        const duration = calculateTotalDurationOfTrainingProgram(data)
        setTotalDuration(duration)
      } catch (error) {
        navigate("/not-found")
      }
    }

    fetchProgram()
  }, [id, navigate])

  return (
    <section className="w-full">
      <div className="flex h-full min-h-screen w-full flex-col">
        <ProgramHeader title={program?.name} status={program?.status} />
        <ProgramSubHeader
          days={totalDuration.days}
          hours={totalDuration.learningTime}
          modifiedBy="MNhat123"
          modifiedOn={
            program?.modifiedOn
              ? format(new Date(program.modifiedOn), "MM/dd/yyyy")
              : ""
          }
        />

        <div className="px-5 ">
          <ProgramInfo id={program?.trainingProgramCode} name={program?.name} />
        </div>
        <div className="p-5">
          <h1 className="text-xl">Content</h1>
          <div className="mt-2 space-y-4 ">
            {!program?.syllabuses &&
              Array.from({ length: 5 }).map((_, index) => (
                <Card key={index}>
                  <div className="flex h-full flex-col">
                    <div className="flex flex-col p-4">
                      <div className="flex w-full items-center justify-between">
                        <div className="flex w-full items-center space-x-4">
                          <Skeleton className="h-8 w-[40%]" />
                          <Skeleton className="h-5 w-16" />
                        </div>
                      </div>
                      <div className="mt-2 flex space-x-3">
                        <Skeleton className="h-5 w-10" />
                        <span>|</span>
                        <Skeleton className="h-5 w-16" />
                        <span>|</span>
                        <Skeleton className="h-5 w-48" />
                      </div>
                    </div>
                  </div>
                </Card>
              ))}

            {program?.syllabuses.map((syllabus) => (
              <SyllabusCard syllabus={syllabus} />
            ))}
          </div>
        </div>
      </div>
    </section>
  )
}
