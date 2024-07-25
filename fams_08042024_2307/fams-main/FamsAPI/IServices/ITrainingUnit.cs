using DataLayer.Entities;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface ITrainingUnit
    {
        public void AddUnit(string unitName, int dayNumber, string topicCode, out bool success, out string message);
        public void DuplicateTrainingUnitsAndContents(string oldSyllabus, SyllabusViewModel newSyllabus);
        public Task EditUnit(string unitCode, string newUnitName);
        public void RemoveDay(int day, string topicCode);
        public TrainingUnit GetTrainingUnitByUnitCode(string unitCode);
        List<TrainingUnit> GetTrainingUnitsByDay(int dayNumber);
        List<TrainingUnit> GetTrainingUnitListByTopicCode(string topicCode);
        public void RemoveUnit (string  unitCode);

    }
}
