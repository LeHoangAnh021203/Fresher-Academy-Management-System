using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class SyllabusSearchViewModel
    {
        public string? TopicCode { get; set; }
        public string? TopicName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Duration { get; set; }
        public int PublishStatus { get; set; }
        public string? OutputStandard { get; set; }
    }
}
