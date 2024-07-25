export interface Syllabus {
  topicCode: string
  topicName: string
  // technicalGroup: string
  publishStatus: string
  trainingPrinciple: string
  courseObjectives: string
  technicalRequirement: string
  trainingAudience: number
  level: SyllabusLevel
  version: number
  createdBy: string
  createdBy: string
  createdOn: string
  modifiedBy: string
  modifiedBy: string
  modifiedOn: string
  learningObjectives: LearningObjective[]
  trainingUnits: TrainingUnit[]
}

export enum SyllabusLevel {
  AutoDetect = "auto detect",
  Beginner = "beginner",
  Advanced = "advanced",
  AllLevel = "all level"
}

export interface SyllabusPreview
  extends Omit<
    Syllabus,
    | "technicalGroup"
    | "trainingPrinciple"
    | "trainingAudience"
    | "technicalRequirement"
    | "courseObjectives"
  > {}

export interface SyllabusCreate
  extends Omit<
    Syllabus,
    "publishStatus" | "createdOn" | "modifiedBy" | "modifiedOn"
  > {}

export interface TrainingUnit {
  unitCode: string
  unitName: string
  dayNumber: number
  trainingContents: TrainingContent[]
}

export enum DeliveryType {
  AssignmentLab = "Assignment/Lab",
  ConceptLecture = "Concept/Lecture",
  GuideReview = "Guide/Review",
  TestQuiz = "Test/Quiz",
  Exam = "Exam",
  SeminarWorkshop = "Seminar/Workshop"
}

export enum TrainingFormat {
  Online = "Online",
  Offline = "Offline"
}

export interface TrainingContent {
  contentId: string
  content: string
  deliveryType: DeliveryType
  duration: number // minutes
  trainingFormat: TrainingFormat
  learningObjectives: LearningObjective[]
}

export interface LearningObjective {
  code: string
  name: string
  description: string
}

export interface Program {
  trainingProgramCode: string
  name: string
  status: string
  startTime: string
  createdBy: string
  createdBy: string
  createdOn: string
  duration: number // day
  modifiedBy: string
  modifiedBy: string
  modifiedOn: string
  syllabuses: Syllabus[]
}

export interface ProgramPreview extends Omit<Program, "syllabuses"> {} // row in training program table

export enum ClassStatus {
  Planning = "Planning",
  Opening = "Opening",
  Closed = "Closed"
}

export enum AttendeeType {
  Fresher = "Fresher",
  Online = "Online fee-fresher",
  Offline = "Offline fee-fresher",
  Intern = "Intern"
}

export interface Class {
  classId: string
  className: string
  classCode: string
  duration: number // days
  status: ClassStatus
  startClassTime: string // hh:mm
  endClassTime: string // hh:mm
  location: string[]
  trainers: string[]
  admins: string[]
  fsu: string
  email: string
  startDate: string //mm//dd//yyyy
  endDate: string //mm//dd//yyyy
  createdBy: string
  createdOn: string
  modifiedBy: string
  modifiedOn: string
  attendee: AttendeeType
  trainingProgram: Program
}

export interface ClassGQL {
  classId: string
  className: string
  classCode: string
  duration: number
  status: string
  startClassTime: string
  endClassTime: string
  location: string[]
  trainers: string[]
  admins: string[]
  fsu: string
  email: string
  startDate: string
  endDate: string
  createdBy: string
  createdOn: string
  modifiedBy: string
  modifiedOn: string
  attendee: string
  trainingProgram: Program
}

type PermissionValue =
  | "access denied"
  | "view"
  | "modify"
  | "create"
  | "full access"
export interface UserPermission {
  superAdmin: {
    syllabus: PermissionValue
    trainingProgram: PermissionValue
    class: PermissionValue
    learningMaterial: PermissionValue
    user: PermissionValue
  }
  classAdmin: {
    syllabus: PermissionValue
    trainingProgram: PermissionValue
    class: PermissionValue
    learningMaterial: PermissionValue
    user: PermissionValue
  }
  trainer: {
    syllabus: PermissionValue
    trainingProgram: PermissionValue
    class: PermissionValue
    learningMaterial: PermissionValue
    user: PermissionValue
  }
}

export type Role = "superAdmin" | "classAdmin" | "trainer"
