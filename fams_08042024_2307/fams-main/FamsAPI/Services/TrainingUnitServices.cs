using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System.Security.Cryptography;
using Xunit.Sdk;
#pragma warning disable
namespace FamsAPI.Services
{
    public class TrainingUnitServices : ITrainingUnit
    {
        private readonly TrainingUnitRepository _trainingUnitRepository;
        private readonly TrainingContentRepository _trainingContentRepository;
        private readonly LearningObjectiveRepository _learningObjectiveRepository;
        private readonly ITrainingContent _trainingContentService;
        private readonly SyllabusRepository _syllabusRepository;

        public TrainingUnitServices(TrainingUnitRepository trainingUnitRepository, TrainingContentRepository trainingContentRepository, LearningObjectiveRepository learningObjectiveRepository, ITrainingContent trainingContentService, SyllabusRepository syllabusRepository)
        {
            _trainingUnitRepository = trainingUnitRepository;
            _trainingContentRepository = trainingContentRepository;
            _learningObjectiveRepository = learningObjectiveRepository;
            _trainingContentService = trainingContentService;
            _syllabusRepository = syllabusRepository;
        }

        public void AddUnit(string unitName, int dayNumber, string topicCode, out bool success, out string message)
        {
            success = false;
            message = "";

            if (!GetUnitName(unitName, topicCode))
            {
                message = "Unit Name already exists!";
                return;
            }
            if (!IsTopicCodeExists(topicCode))
            {
                message = "Topic Code does not exist!";
                return;
            }

            try
            {
                var name = unitName;
                var newDay = new TrainingUnit
                {
                    UnitCode = GenerateUnitCode(),
                    UnitName = name,
                    DayNumber = dayNumber,
                    TopicCode = topicCode
                };
                _trainingUnitRepository.Add(newDay);
                _trainingUnitRepository.SaveChanges();

                success = true;
                message = "Unit added successfully!";
            }
            catch (Exception ex)
            {
                message = "Error adding unit: " + ex.Message;
            }
        }

        public void DuplicateTrainingUnitsAndContents(string oldTopicCode, SyllabusViewModel newSyllabus)
        {
            try
            {
                var listUnitCode = GetTrainingUnitListByTopicCode(oldTopicCode);
                foreach (var oldTrainingUnit in listUnitCode)
                {
                    var newTrainingUnit = new TrainingUnit
                    {
                        UnitCode = GenerateUnitCode(),
                        UnitName = oldTrainingUnit.UnitName,
                        DayNumber = oldTrainingUnit.DayNumber,
                        TopicCode = newSyllabus.TopicCode
                    };

                    _trainingUnitRepository.Add(newTrainingUnit);
                    _trainingUnitRepository.SaveChanges();
                    _trainingContentService.DuplicateTrainingContents(oldTrainingUnit.UnitCode,newTrainingUnit);                                            
                    }
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditUnit(string unitCode, string newUnitName)
        {
            try
            {
                var unit = _trainingUnitRepository.Get(u => u.UnitCode == unitCode);
                if (unit != null)
                {
                    unit.UnitName = newUnitName;
                    _trainingUnitRepository.Update(unit);
                    await _trainingUnitRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Unit not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the unit.", ex);
            }
        }

        public List<TrainingUnit> GetTrainingUnitListByTopicCode(string topicCode) {
            try
            {
                var checkUnit = _trainingUnitRepository.GetAll()
                                                .Where(d => d.TopicCode == topicCode)
                                                .ToList();
                if (checkUnit == null)
                {
                    return null;
                }
                return checkUnit;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving training units by topic code.", ex);
            }
        }

        public TrainingUnit GetTrainingUnitByUnitCode(string unitCode)
        {
            var checkUnitCode = _trainingUnitRepository.Get(u => u.UnitCode == unitCode);
            return checkUnitCode;
        }

        public List<TrainingUnit> GetTrainingUnitsByDay(int dayNumber)
        {
            try
            {
                var checkDay = _trainingUnitRepository.GetAll()
                                                .Where(d => d.DayNumber == dayNumber)
                                                .ToList();
                if (checkDay == null)
                {
                    return null;
                }
                return checkDay;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void RemoveDay(int dayNumber, string topicCode)
        {
            try
            {
                var unitsToRemove = _trainingUnitRepository
                                    .GetAll()
                                    .Where(u => u.DayNumber == dayNumber && u.TopicCode == topicCode)
                                    .ToList();

                foreach (var unit in unitsToRemove)
                {
                    var contentsToRemove = _trainingContentRepository
                                            .GetAll()
                                            .Where(c => c.UnitCode == unit.UnitCode)
                                            .ToList();

                    foreach (var content in contentsToRemove)
                    {
                        _trainingContentRepository.Remove(content);
                    }

                    var subUnits = _trainingUnitRepository
                                    .GetAll()
                                    .Where(u => u.DayNumber > dayNumber && u.TopicCode == topicCode)
                                    .ToList();
                    foreach (var subUnit in subUnits)
                    {
                        subUnit.DayNumber = unit.DayNumber;
                        _trainingUnitRepository.Update(subUnit);
                    }

                    _trainingUnitRepository.Remove(unit);
                }
                _trainingUnitRepository.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #region Remove Unit By UnitCode
        public void RemoveUnit(string unitCode)
        {
            try
            {
                int currentDay;
                string currentSyllabus;

                var unitRemove = _trainingUnitRepository.GetAll().FirstOrDefault(c => c.UnitCode.Equals(unitCode));
                if (unitRemove != null)
                {
                    currentDay = unitRemove.DayNumber;
                    currentSyllabus = unitRemove.TopicCode;

                    var listDayOfUnit = _trainingUnitRepository.GetAll().Where(c => c.DayNumber.Equals(currentDay) && c.TopicCode.Equals(currentSyllabus)).ToList();

                    if (listDayOfUnit.Count > 1)
                    {
                        var listContent = _trainingContentService.GetAllTrainingContentByUnitCode(unitCode).ToList();
                        //Remove all content in a unit
                        foreach(var content in listContent)
                        {
                           _trainingContentRepository.Remove(content);
                            _trainingContentRepository.SaveChanges();
                        }

                        _trainingUnitRepository.Remove(unitRemove);
                        _trainingUnitRepository.SaveChanges();
                    }
                    else
                    {
                        RemoveDay(currentDay,currentSyllabus);
                    }
                     

                }
                else
                {
                    throw new Exception("This unit doesn't existed in our system!");
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        public string GenerateUnitCode()
        {
            string maxUnitCode = _trainingUnitRepository.GetAll()
                                    .Max(u => u.UnitCode);

            // Extract the numeric part of the unit code and parse it
            //My understand: U0001 then sub the U out then take only number left put it as PARSENUMBER
            int maxUnitNumber = int.TryParse(maxUnitCode?.Substring(1), out int parsedNumber) ? parsedNumber : 0;
            int nextUnitNumber = maxUnitNumber + 1;
            string unitCode = $"U{nextUnitNumber:D8}";
            return unitCode;
        }

        public bool GetUnitName(string unitName, string topicCode)
        {
            var check = true;
            var trainingList = _trainingUnitRepository.GetTrainingUnitByTopicCode(topicCode);

            foreach (var item in trainingList)
            {
                if (item.UnitName.Equals(unitName))
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        private bool IsTopicCodeExists(string topicCode)
        {
            return _trainingUnitRepository.GetAll().Any(u => u.TopicCode == topicCode);
        }
    }
}
