using DataLayer.Entities;
using static DataLayer.Entities.TrainingProgram;

namespace FamsAPI.ViewModel
{
    public class TrainingProgramDetailViewModel
    {
        public string TrainingProgramCode { get; set; }
        public string Name { get; set; }
        public Statuses Status { get; set; }
        public int Duration { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public List<SyllabusInTrainingDetailViewModel> Syllabuses { get; internal set; }
    }
}
