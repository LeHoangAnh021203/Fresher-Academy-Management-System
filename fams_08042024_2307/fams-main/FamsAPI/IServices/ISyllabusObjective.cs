using DataLayer.Entities;

namespace FamsAPI.IServices
{
    public interface ISyllabusObjective
    {
        public void DuplicateSyllabusObjectives(String oldTopicCode, Syllabus newSyllabus);
    }
}
