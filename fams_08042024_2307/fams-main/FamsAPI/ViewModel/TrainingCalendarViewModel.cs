using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class TrainingCalendarViewModel
    {
        public int CalenderId { get; set; }
        public string Date { get; set; }
        public string Admin {  get; set; }
        public string Trainer { get; set; }
        public string ClassCode {  get; set; }
        public string LocationId { get; set; }
        public string Time {  get; set; }
        public TypeOfAttendee Attendee { get; set; }
        public string TrainingProgramCode { get; set; }
    }
}
