import { DeliveryType, TrainingContent, TrainingFormat } from "@/lib/types"

import { learningObjectiveData } from "./learning-objective"

export const trainingContentData: TrainingContent[] = [
  {
    contentId: "1",
    content: "Introduction to Programming",
    deliveryType: DeliveryType.AssignmentLab,
    duration: 60,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[1],
      learningObjectiveData[2]
    ]
  },
  {
    contentId: "2",
    content: "Conditional Statements",
    deliveryType: DeliveryType.ConceptLecture,
    duration: 45,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [learningObjectiveData[2]]
  },
  {
    contentId: "3",
    content: "Arrays and Loops",
    deliveryType: DeliveryType.GuideReview,
    duration: 75,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [learningObjectiveData[1]]
  },
  {
    contentId: "4",
    content: "Functions in Programming",
    deliveryType: DeliveryType.SeminarWorkshop,
    duration: 90,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[3],
      learningObjectiveData[5]
    ]
  },
  {
    contentId: "5",
    content: "Database Basics",
    deliveryType: DeliveryType.Exam,
    duration: 60,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [learningObjectiveData[1], learningObjectiveData[4]]
  },
  {
    contentId: "6",
    content: "Responsive Web Design",
    deliveryType: DeliveryType.TestQuiz,
    duration: 120,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [learningObjectiveData[2], learningObjectiveData[5]]
  },
  {
    contentId: "7",
    content: "Introduction to JavaScript Basics",
    deliveryType: DeliveryType.TestQuiz,
    duration: 90,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [learningObjectiveData[2], learningObjectiveData[5]]
  },
  {
    contentId: "8",
    content: "Advanced Python Programming",
    deliveryType: DeliveryType.Exam,
    duration: 120,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [learningObjectiveData[2], learningObjectiveData[5]]
  },
  {
    contentId: "9",
    content: "Machine Learning Fundamentals",
    deliveryType: DeliveryType.ConceptLecture,
    duration: 150,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [learningObjectiveData[2], learningObjectiveData[5]]
  },
  {
    contentId: "10",
    content: "Data Science for Beginners",
    deliveryType: DeliveryType.Exam,
    duration: 120,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [learningObjectiveData[2], learningObjectiveData[5]]
  },
  {
    contentId: "11",
    content: "Agile Project Management",
    deliveryType: DeliveryType.GuideReview,
    duration: 90,
    trainingFormat: TrainingFormat.Online,
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[3],
      learningObjectiveData[5]
    ]
  },
  {
    contentId: "12",
    content: "UX/UI Design Principles",
    deliveryType: DeliveryType.SeminarWorkshop,
    duration: 150,
    trainingFormat: TrainingFormat.Offline,
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[3],
      learningObjectiveData[5]
    ]
  }
]
