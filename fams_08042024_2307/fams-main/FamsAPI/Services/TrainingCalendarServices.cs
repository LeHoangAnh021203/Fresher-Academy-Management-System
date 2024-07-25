using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;

namespace FamsAPI.Services
{
    public class TrainingCalendarServices : ITrainingCalendar
    {
        private readonly TrainingCalendarRepository _trainingCalendarRepository;
        private readonly ClassRepository _classRepository;
        private readonly IMapper _mapper;

        public TrainingCalendarServices(TrainingCalendarRepository trainingCalendarRepository, ClassRepository classRepository, IMapper mapper)
        {
            _trainingCalendarRepository = trainingCalendarRepository;
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public List<TrainingCalendarViewModel> GetAllTrainingCalendars()
        {
            try
            {
                var trainingCalendars = _trainingCalendarRepository
                    .GetAll()
                    .Where(tc => tc.ClassId != null)
                    .ToList();

                foreach (var trainingCalendar in trainingCalendars)
                {
                    var classDetails = _classRepository.Get(c => c.ClassID == trainingCalendar.ClassId);
                    if (classDetails != null)
                    {
                        trainingCalendar.Class = classDetails;
                    }
                }
                var trainingCalendarViewModels = trainingCalendars
                    .Select(tc => new TrainingCalendarViewModel
                    {
                        CalenderId = tc.CalendarId,
                        Date = tc.Date,
                        Admin = tc.Admin,
                        Trainer = tc.Trainer,
                        ClassCode = tc.Class.ClassCode,
                        LocationId = tc.Class.LocationId,
                        Time = tc.Time,
                        Attendee = tc.Attendee,
                        TrainingProgramCode = tc.Class.TrainingProgramCode
                    })
                    .ToList();

                return trainingCalendarViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all training calendars.", ex);
            }
        }

        public List<TrainingCalendarViewModel> GetTrainingCalendarByClassId(string classId)
        {
            try
            {
                var trainingCalendars = _trainingCalendarRepository
                    .GetAll()
                    .Where(tc => tc.ClassId == classId)
                    .ToList();

                foreach (var trainingCalendar in trainingCalendars)
                {
                    var classDetails = _classRepository.Get(c => c.ClassID == trainingCalendar.ClassId);
                    if (classDetails != null)
                    {
                        trainingCalendar.Class = classDetails;
                    }
                }

                var trainingCalendarViewModels = trainingCalendars
                    .Select(tc => new TrainingCalendarViewModel
                    {
                        CalenderId = tc.CalendarId,
                        Date = tc.Date,
                        Admin = tc.Admin,
                        Trainer = tc.Trainer,
                        ClassCode = tc.Class?.ClassCode ?? "",
                        LocationId = tc.Class?.LocationId ?? "",
                        Time = tc.Time,
                        Attendee = tc.Attendee,
                        TrainingProgramCode = tc.Class?.TrainingProgramCode ?? ""
                    })
                    .ToList();

                return trainingCalendarViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting training calendars by ClassId.", ex);
            }
        }

        public async Task<List<InputTrainingCalendarViewModel>> AddNewTrainingCalendar(List<InputTrainingCalendarViewModel> trainingCalendarViewModels)
        {
            try
            {
                var newTrainingCalendars = new List<InputTrainingCalendarViewModel>();

                foreach (var trainingCalendarViewModel in trainingCalendarViewModels)
                {
                    var existedClass = _classRepository
                                           .GetAll()
                                           .FirstOrDefault(c => c.ClassID == trainingCalendarViewModel.ClassId);

                    if (existedClass != null)
                    {
                        var newTrainingCalendar = _mapper.Map<InputTrainingCalendarViewModel>(trainingCalendarViewModel);
                        newTrainingCalendar.ClassId = existedClass.ClassID;
                        newTrainingCalendars.Add(newTrainingCalendar);
                    }
                    else
                    {
                        throw new InvalidOperationException("Class does not exist.");
                    }
                }
               _trainingCalendarRepository.AddRange(_mapper.Map<List<TrainingCalendar>>(newTrainingCalendars));
                await _trainingCalendarRepository.SaveChangesAsync();

                return newTrainingCalendars;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding new training calendars.", ex);
            }
        }
    }
}
