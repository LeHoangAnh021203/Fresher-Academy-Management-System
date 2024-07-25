using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;

namespace FamsAPI.Services
{
    public class AssessmentService : IAssessment
    {
        private readonly AssessmentRepository _assessmentRepository;
        private readonly SyllabusRepository _syllabusRepository;

        public AssessmentService(AssessmentRepository assessmentRepository, SyllabusRepository syllabusRepository)
        {
            _assessmentRepository = assessmentRepository;
            _syllabusRepository = syllabusRepository;
        }
        public void AddNew(int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe)
        {
            try
            {
                var assessment = new Assessment
                {
                    AssessmentID = GenerateAssessmentID(),
                    QuizCount = quizCount,
                    QuizPercent = quizPercent,
                    AssignmentCount = assignmentCount,
                    AssignmentPercent = assignmentPercent,
                    FinalTheoryPercent = fe,
                    FinalPracticePercent = pe
                };
                _assessmentRepository.Add(assessment);
                _assessmentRepository.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the assessment.", ex);
            }
        }

        public Assessment DuplicateAssessment(string AssessmentID)
        {
            try
            {
                var checkAssessment = _assessmentRepository.Get(a => a.AssessmentID == AssessmentID);
                if (checkAssessment == null)
                {
                    return null;
                }
                var newAssessment = new Assessment
                {
                    AssessmentID = GenerateAssessmentID(),
                    QuizCount = checkAssessment.QuizCount,
                    QuizPercent = checkAssessment.QuizPercent,
                    AssignmentCount = checkAssessment.AssignmentCount,
                    AssignmentPercent = checkAssessment.AssignmentPercent,
                    FinalTheoryPercent = checkAssessment.FinalTheoryPercent,
                    FinalPracticePercent = checkAssessment.FinalPracticePercent
                };
                _assessmentRepository.Add(newAssessment);
                _assessmentRepository.SaveChanges();
                return newAssessment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AssessmentViewModel> GetAllAssessment()
        {
            try
            {
                var assessmentsWithSyllabus = _assessmentRepository
                    .GetAll()
                    .ToList()
                    .Select(a => new AssessmentViewModel
                    {
                        AssessmentID = a.AssessmentID,
                        QuizCount = a.QuizCount,
                        QuizPercent = a.QuizPercent,
                        AssignmentCount = a.AssignmentCount,
                        AssignmentPercent = a.AssignmentPercent,
                        FinalTheoryPercent = a.FinalTheoryPercent,
                        FinalPracticePercent = a.FinalPracticePercent,
                        TopicCode = _syllabusRepository.Get(s => s.AssessmentID == a.AssessmentID)?.TopicCode ?? null
                    })
                    .ToList();

                return assessmentsWithSyllabus;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to get all assessments.", ex);
            }
        }

        public AssessmentViewModel GetAssessmentById(string id)
        {
            try
            {
                var checkAssessment = _assessmentRepository.Get(a => a.AssessmentID == id);
                if (checkAssessment == null)
                {
                    throw new ArgumentException("AssessmentId not found.", nameof(id));
                }
                return new AssessmentViewModel
                {
                    AssessmentID = checkAssessment.AssessmentID,
                    QuizCount = checkAssessment.QuizCount,
                    QuizPercent = checkAssessment.QuizPercent,
                    AssignmentCount = checkAssessment.AssignmentCount,
                    AssignmentPercent = checkAssessment.AssignmentPercent,
                    FinalTheoryPercent = checkAssessment.FinalTheoryPercent,
                    FinalPracticePercent = checkAssessment.FinalPracticePercent,
                    TopicCode = _syllabusRepository.Get(s => s.AssessmentID == id).TopicCode
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get the assessment by ID.", ex);
            }
        }
        
        public async Task<AssessmentViewModel> EditAssessment(AssessmentViewModel assessment)
        {
            try
            {
                var newAssessment = _assessmentRepository.Get(a => a.AssessmentID == assessment.AssessmentID);
                if (newAssessment != null)
                {
                    newAssessment.QuizCount = assessment.QuizCount;
                    newAssessment.QuizPercent = assessment.QuizPercent;
                    newAssessment.AssignmentCount = assessment.AssignmentCount;
                    newAssessment.AssignmentPercent = assessment.AssignmentPercent;
                    newAssessment.FinalTheoryPercent = assessment.FinalTheoryPercent;
                    newAssessment.FinalPracticePercent = assessment.FinalPracticePercent;

                    _assessmentRepository.Update(newAssessment);
                    await _assessmentRepository.SaveChangesAsync();

                    return assessment; // Return the updated assessment.
                }
                else
                {
                    throw new Exception("Assessment not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the assessment.", ex);
            }
        }

        private string GenerateAssessmentID()
        {     
            int maxSequentialNumber = _assessmentRepository.GetAll()
                .Select(a => int.TryParse(a.AssessmentID[2..], out int num) ? num : 0)
                .DefaultIfEmpty(0)
                .Max();
            int nextSequentialNumber = maxSequentialNumber + 1;
            if (nextSequentialNumber > 999)
            {
                throw new InvalidOperationException("Maximum Id count exceeded.");
            }
            string assessmentId = $"AS{nextSequentialNumber:D3}";

            return assessmentId;
        }
    }
}
