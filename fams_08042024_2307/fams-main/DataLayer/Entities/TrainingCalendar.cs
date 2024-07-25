using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class TrainingCalendar
    {
        [Key]
        public int CalendarId {  get; set; }

        public string? Date {  get; set; }

        [Required]
        public string Admin {  get; set; }

        [Required]
        public string Trainer { get; set; }

        [Required]
        public string Time {  get; set; }

        [Required]
        public TypeOfAttendee Attendee { get; set; }

        [Required]
        public string ClassId { get; set; }

        public virtual Class? Class { get; set; }
    }

    public enum TypeOfAttendee
    {
        Intern,
        Fresher,
        OnlineFeeFresher,
        OfflineFeeFresher
    }
}
