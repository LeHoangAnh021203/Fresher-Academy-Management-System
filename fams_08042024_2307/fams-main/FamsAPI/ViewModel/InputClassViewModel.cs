using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class InputClassViewModel
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
    }
}
