using System.Security.Cryptography.Pkcs;
using static DataLayer.Entities.Syllabus;

namespace FamsAPI.ViewModel
{
    public class SyllabusInTrainingDetailViewModel
    {
        public string? TopicCode { get; set; }   
        public string? TopicName { get; set; }
        public int Version { get; set; }
        public PulishStatuses PulishStatus { get; set; }
        public string? CreatedBy { get; set; }   
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<TrainingUnitViewModelV2> TrainingUnits { get; set; }
    }
}
