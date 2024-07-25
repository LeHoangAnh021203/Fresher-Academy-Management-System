using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;

namespace FamsAPI.Services
{
    public class SyllabusObjectiveServices : ISyllabusObjective
    {
        private readonly SyllabusObjectiveRepository _syllabusObjectiveRepository;
        public SyllabusObjectiveServices(SyllabusObjectiveRepository syllabusObjectiveRepository)
        {
            _syllabusObjectiveRepository = syllabusObjectiveRepository;
        }

        public void DuplicateSyllabusObjectives(String oldTopicCode, Syllabus newSyllabus)
        {
            try
            {
                var oldListObj = GetObjectivesByTopicCode(oldTopicCode);
                foreach (var oldSyllabusObjective in oldListObj)
                {
                    var newSyllabusObjective = new SyllabusObjective
                    {
                        TopicCode = newSyllabus.TopicCode,
                        ObjectiveCode = oldSyllabusObjective.ObjectiveCode
                    };
                    _syllabusObjectiveRepository.Add(newSyllabusObjective);
                    _syllabusObjectiveRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SyllabusObjective> GetObjectivesByTopicCode(string oldTopicCode)
        {         
            try
            {
                var listObj = _syllabusObjectiveRepository.GetAll().Where(c => c.TopicCode == oldTopicCode).ToList();
                if (listObj == null)
                {
                    throw new InvalidOperationException("Objectives not found.");
                }
                return listObj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
