using DataLayer.Repositories;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface ITrainingCalendar
    {
        public List<TrainingCalendarViewModel> GetAllTrainingCalendars();
        public List<TrainingCalendarViewModel> GetTrainingCalendarByClassId(string classId);
        Task<List<InputTrainingCalendarViewModel>> AddNewTrainingCalendar(List<InputTrainingCalendarViewModel> trainingCalendarViewModels);
    }
}
