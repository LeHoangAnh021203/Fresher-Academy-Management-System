using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class InputTrainingCalendarViewModel
    {
        public string ClassId { get; set; }
        public string Date {  get; set; }
        public string Time {  get; set; }
        public string Admin {  get; set; }
        public string Trainer { get; set; }
        public TypeOfAttendee Attendee { get; set; }
    }
}
