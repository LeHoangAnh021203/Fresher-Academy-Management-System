export interface ISyllabusPreview {
  topicCode: string
  topicName: string
  createdBy: string
  createdDate: string | Date
  modifiedBy: string
  modifiedDate: string | Date
  duration: number // in minutes
  publishStatus: IPublishStatus
  outputStandard: string[]
}

export interface ISyllabus {
  topicCode: string
  topicName: string
  version: number
  technicalGroup: string
  trainingAudience: string
  topicOutline: string
  createdBy: string
  createdDate: string | Date
  modifiedBy: string
  modifiedDate: string | Date
  duration: number // in minutes
  publishStatus: IPublishStatus
  technicalRequirement: string
  courseObjective: string
  trainingMaterials: string
  trainingUnits: ITrainingUnit[]
  trainingContents: ITrainingContent[]
  trainingPrinciple: string
  assessment: IAssessment
}

export interface ITrainingUnit {
  unitCode: string
  unitName: string
  dayNumber: number
  topicCode: string
  trainingContents: ITrainingContent[] //!CHECK THIS
}

export interface ITrainingContent {
  contentId: string
  content: string
  code: string
  deliveryType: IDeliveryType
  duration: number // in minutes
  trainingFormat: ITrainingFormat
  note: string //!CHECK THIS
  unitCode: string //!CHECK THIS
  // trainingUnit?: null //can be remove
  learningObjectives: ILearningObjective[]
}

export interface IAssessment {
  assessmentID: string
  quizCount: number
  quizPercent: number
  assignmentCount: number
  assignmentPercent: number
  finalTheoryPercent: number
  finalPracticePercent: number
  // syllabus?: null //can be remove
}

export interface ILearningObjective {
  code: string
  name: string
  description: string
}

export enum IPublishStatus {
  Denied = 0,
  Editing = 1,
  Pending = 2,
  Published = 3
}

export enum ITrainingFormat {
  Online = "Online",
  Offline = "Offline"
}

export enum IDeliveryType {
  AssignmentLab = "Assignment/Lab",
  ConceptLecture = "Concept/Lecture",
  GuideReview = "Guide/Review",
  TestQuiz = "Test/Quiz",
  Exam = "Exam",
  SeminarWorkshop = "Seminar/Workshop"
}
