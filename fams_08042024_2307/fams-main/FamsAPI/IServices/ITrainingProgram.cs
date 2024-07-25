using DataLayer.Entities;
using System.Security.Claims;
using FamsAPI.ViewModel;
using static DataLayer.Entities.TrainingProgram;

using DataLayer.Entities;

using System.Threading.Tasks;

namespace FamsAPI.IServices
{
    public interface ITrainingProgram
    {
        TrainingProgram AddNewTrainingProgramAsync(ClaimsPrincipal user, string name, int duration, List<string> topicCode);
        public List<TrainingProgramViewModel> SearchTrainingPrograms(string? keyword, string? createBy, string? createDate, int? duration, Statuses? status);
        public Task<bool> DeleteTrainingProgram(string trainingCode);

        Task UpdateTrainingProgram(string trainingCode, string name, ClaimsPrincipal user);
        public List<TrainingProgram> GetAllTrainingProgram();
        Task<TrainingProgramDetailViewModel> ViewDetailTrainingProgramTrainer(string trainingCode);
        Task<TrainingProgramDetailViewModel> ViewDetailTrainingProgramAdmin(string trainingCode);
        Task<bool> RemoveSyllabusFromTrainingProgram(string trainingProgramCode, string topicCode);
        Task<bool> AddSyllabusToTrainingProgram(string trainingProgramCode, string topicCode);
        void UpdateTrainingProgramStatus(string trainingProgramCode, ClaimsPrincipal user);
        TrainingProgram GetTrainingProgramByTrainingProgramCode(string code);

    }
}
