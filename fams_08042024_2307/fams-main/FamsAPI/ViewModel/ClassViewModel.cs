using DataLayer.Entities;
using Newtonsoft.Json;

namespace FamsAPI.ViewModel
{
    public class ClassViewModel
    {
        public string? ClassID { get; set; }
        public string? ClassName { get; set; }
        public string? ClassCode { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public string LocationId { get; set; }
        public string FsuId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? TrainingProgramCode { get; set; } 
        public TrainingProgramViewModel? TrainingProgram { get; set; }
        public ICollection<ClassUserViewModel>? ClassUsers { get; set; }
        public ICollection<TrainingCalendarViewModel>? TrainingCalendars { get; set; }
    }
}
