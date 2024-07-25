import { TrainingUnit } from "./../lib/types"
import { trainingContentData } from "./training-content"

export const trainingUnitData: TrainingUnit[] = [
  {
    unitCode: "U1",
    unitName: "Programming Fundamentals",
    dayNumber: 1,
    trainingContents: [trainingContentData[0], trainingContentData[1]]
  },
  {
    unitCode: "U2",
    unitName: "Advanced Programming Concepts",
    dayNumber: 2,
    trainingContents: [trainingContentData[2]]
  },
  {
    unitCode: "U3",
    unitName: "Web Development Basics",
    dayNumber: 2,
    trainingContents: [
      trainingContentData[3],
      trainingContentData[4],
      trainingContentData[5]
    ]
  },
  {
    unitCode: "UNIT-01",
    unitName: "Web Development",
    dayNumber: 1,
    trainingContents: [
      trainingContentData[6],
      trainingContentData[7],
      trainingContentData[8]
    ]
  },
  {
    unitCode: "UNIT-02",
    unitName: "Data Science 1",
    dayNumber: 2,
    trainingContents: [
      trainingContentData[9],
      trainingContentData[10],
      trainingContentData[11]
    ]
  },
  {
    unitCode: "UNIT-03",
    unitName: "Data Science 2",
    dayNumber: 2,
    trainingContents: [
      trainingContentData[9],
      trainingContentData[10],
      trainingContentData[11]
    ]
  },
  {
    unitCode: "UNIT-04",
    unitName: "Data Science 3",
    dayNumber: 3,
    trainingContents: [
      trainingContentData[9],
      trainingContentData[10],
      trainingContentData[11]
    ]
  }
]
