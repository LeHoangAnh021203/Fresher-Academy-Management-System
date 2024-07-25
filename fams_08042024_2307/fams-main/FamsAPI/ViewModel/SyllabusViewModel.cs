﻿using DataLayer.Entities;
using static DataLayer.Entities.Syllabus;

namespace FamsAPI.ViewModel
{
    public class SyllabusViewModel
    {
        public string? TopicCode { get; set; }
        public string? TopicName { get; set; }
        public string? TechnicalGroup { get; set; }
        public string TechnicalRequirement { get; set; }
        public string CourseObjective { get; set; }
        public int Version { get; set; }
        public string? TrainingAudience { get; set; }
        public string? TopicOutline { get; set; }

        public string? TrainingMaterials { get; set; }

        public string? TrainingPrinciple { get; set; }
        public Priorities Priority { get; set; }

        public PulishStatuses PulishStatus { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Guid UserId { get; set; }

        public string AssessmentID { get; set; }

        public string PublishStatus { get; set; }
        public List<TrainingContentViewModel> TrainingContents { get; set; }
    }
}
