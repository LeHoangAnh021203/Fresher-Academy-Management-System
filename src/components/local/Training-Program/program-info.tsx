import { Card } from "@/components/global/atoms/card"

interface ProgramInfoProps {
  id: string
  name: string
}
export const ProgramInfo = ({ id, name }: ProgramInfoProps) => {
  return (
    <>
      <h1 className="text-2xl font-bold">General Information</h1>
      <Card className="mt-2 space-y-2 p-4 text-sm">
        <p className="flex">Program ID: {id}</p>
        <p className="flex">Program Name: {name}</p>
      </Card>
    </>
  )
}
