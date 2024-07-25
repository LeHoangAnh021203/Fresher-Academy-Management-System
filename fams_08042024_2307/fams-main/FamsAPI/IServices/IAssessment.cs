using DataLayer.Entities;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface IAssessment
    {
        public void AddNew(int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe);
        public Assessment DuplicateAssessment(string id);
        public AssessmentViewModel GetAssessmentById(string id);
        List<AssessmentViewModel> GetAllAssessment();
        Task<AssessmentViewModel> EditAssessment(AssessmentViewModel assessment);
    }
}
