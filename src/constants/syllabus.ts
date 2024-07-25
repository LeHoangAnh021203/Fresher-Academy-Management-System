import { Syllabus, SyllabusLevel, SyllabusPreview } from "@/lib/types"

import { learningObjectiveData } from "./learning-objective"
import { trainingUnitData } from "./training-unit"
import { userData } from "./user"

export const syllabusData: Syllabus[] = [
  {
    topicCode: "T1",
    topicName: "Introduction to Programming",
    publishStatus: "Published",
    version: 1,
    courseObjectives:
      '<h2>Where does it come from?</h2><p class="ql-align-justify">Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32.</p><p><br></p>',
    technicalRequirement:
      "<h2>Why do we use it?</h2><p class=\"ql-align-justify\">It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).</p><p><br></p>",
    trainingPrinciple:
      '<h2>What is Lorem Ipsum?</h2><p class="ql-align-justify"><strong>Lorem Ipsum</strong>&nbsp;is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>',
    level: SyllabusLevel.AllLevel,
    trainingAudience: 11,
    createdBy: userData[0],
    createdOn: "2022-02-10",
    modifiedBy: userData[1],
    modifiedOn: "2022-02-15",
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[1],
      learningObjectiveData[2]
    ],
    trainingUnits: [
      trainingUnitData[0],
      trainingUnitData[1],
      trainingUnitData[2]
    ]
  },
  {
    topicCode: "T2",
    topicName: "Database Management Systems",
    publishStatus: "Draft",
    version: 1,
    courseObjectives:
      '<h2>Where does it come from?</h2><p class="ql-align-justify">Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32.</p><p><br></p>',
    technicalRequirement:
      "<h2>Why do we use it?</h2><p class=\"ql-align-justify\">It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).</p><p><br></p>",
    trainingPrinciple:
      '<h2>What is Lorem Ipsum?</h2><p class="ql-align-justify"><strong>Lorem Ipsum</strong>&nbsp;is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>',
    trainingAudience: 22,
    level: SyllabusLevel.AllLevel,
    createdBy: userData[1],
    createdOn: "2022-03-05",
    modifiedBy: userData[1],
    modifiedOn: "2022-03-10",
    learningObjectives: [
      learningObjectiveData[2],
      learningObjectiveData[4],
      learningObjectiveData[5]
    ],
    trainingUnits: [
      trainingUnitData[3],
      trainingUnitData[4],
      trainingUnitData[5],
      trainingUnitData[6]
    ]
  }
]

export const syllabusDataPreview: SyllabusPreview[] = [
  {
    topicCode: "T1",
    topicName: "Introduction to Programming",
    publishStatus: "Published",
    version: 1,
    level: SyllabusLevel.AllLevel,
    createdBy: userData[0],
    createdOn: "2022-02-10",
    modifiedBy: userData[1],
    modifiedOn: "2022-02-15",
    learningObjectives: [
      learningObjectiveData[0],
      learningObjectiveData[1],
      learningObjectiveData[2]
    ],
    trainingUnits: [
      trainingUnitData[0],
      trainingUnitData[1],
      trainingUnitData[2]
    ]
  },
  {
    topicCode: "T2",
    topicName: "Database Management Systems",
    publishStatus: "Draft",
    version: 1,
    level: SyllabusLevel.AllLevel,
    createdBy: userData[1],
    createdOn: "2022-03-05",
    modifiedBy: userData[1],
    modifiedOn: "2022-03-10",
    learningObjectives: [
      learningObjectiveData[2],
      learningObjectiveData[4],
      learningObjectiveData[5]
    ],
    trainingUnits: [
      trainingUnitData[3],
      trainingUnitData[4],
      trainingUnitData[5],
      trainingUnitData[6]
    ]
  }
]
