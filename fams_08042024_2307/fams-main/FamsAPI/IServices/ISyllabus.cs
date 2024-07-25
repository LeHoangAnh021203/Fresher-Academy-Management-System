using DataLayer.Entities;
using FamsAPI.ViewModel;
using System.Security.Claims;

namespace FamsAPI.IServices
{
    public interface ISyllabus
    {
        string DuplicateSyllabusGeneralAsDraft(Syllabus syllabus, Assessment assessment);
        SyllabusViewModel DuplicateSyllabus(String keyword);
        Task<string> SaveSyllabusAsDraftGeneral(Syllabus syllabus, ClaimsPrincipal user);
        List<Object> GetAllSyllabuses();
        Task<Syllabus> UpdateSyllabus(Syllabus syllabus, ClaimsPrincipal user);
        public List<SyllabusSearchViewModel> SearchSyllabusByKeyword(string keyword);
        public List<SyllabusSearchViewModel> SearchSyllabusByDate(string CreatedDate);
        Task<SyllabusDetailViewModel> ViewSyllabusDetail(string key);
        Task<string> GetDataFromExcelFile(string fileDir, ClaimsPrincipal user);
        Task<string> AddNewSyllabusWithAssessmentAndUnit(string topicName, string technicalGroup, string trainingAudience, string topicOutline, string trainingMatirials, string trainingPrinciple, string courseObjective, string technicalRequirement, ClaimsPrincipal user, int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe, List<TrainingUnitViewModel> trainingUnits);
    }
}
