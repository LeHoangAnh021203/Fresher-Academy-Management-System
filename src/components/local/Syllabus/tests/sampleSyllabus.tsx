import {
  DeliveryType,
  Syllabus,
  SyllabusLevel,
  TrainingFormat
} from "@/lib/types"

export const exampleSyllabus: Syllabus = {
  topicCode: "T1",
  topicName: "Introduction to Programming",
  publishStatus: "Published",
  trainingPrinciple: "Active Learning",
  courseObjectives: "To introduce students to the basics of programming.",
  technicalRequirement: "Laptop with code editor installed.",
  trainingAudience: 30,
  level: SyllabusLevel.Beginner,
  version: 1,
  createdBy: {
    name: "John Doe",
    userId: "JD123"
  },
  createdOn: "2024-03-21",
  modifiedBy: {
    name: "Jane Smith",
    userId: "JS456"
  },
  modifiedOn: "2024-03-22",
  learningObjectives: [
    {
      code: "LO001",
      name: "Understand basic programming concepts",
      description:
        "Students should be able to understand variables, loops, and conditionals."
    },
    {
      code: "LO002",
      name: "Write simple programs",
      description:
        "Students should be able to write simple programs in a programming language."
    }
  ],
  trainingUnits: [
    {
      unitCode: "UNIT001",
      unitName: "Introduction",
      dayNumber: 1,
      trainingContents: [
        {
          contentId: "CONTENT001",
          content: "Introduction to programming languages.",
          deliveryType: DeliveryType.AssignmentLab,
          duration: 60,
          trainingFormat: TrainingFormat.Online,
          learningObjectives: [
            {
              code: "LO001",
              name: "Understand basic programming concepts",
              description:
                "Students should be able to understand variables, loops, and conditionals."
            }
          ]
        },
        {
          contentId: "CONTENT002",
          content: "Hands-on practice with code examples.",
          deliveryType: DeliveryType.Exam,
          duration: 90,
          trainingFormat: TrainingFormat.Offline,
          learningObjectives: [
            {
              code: "LO002",
              name: "Write simple programs",
              description:
                "Students should be able to write simple programs in a programming language."
            }
          ]
        }
      ]
    }
  ]
}
