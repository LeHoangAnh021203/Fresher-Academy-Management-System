using DataLayer.Entities;
namespace FamsAPI.ViewModel
{
    public class TrainingProgramViewModel
    {
        public string TrainingProgramCode { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DataLayer.Entities.TrainingProgram.Statuses Status { get; set; }
    }
}
