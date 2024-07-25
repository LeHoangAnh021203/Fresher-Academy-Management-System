using DataLayer.Entities;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface ITrainingContent
    {
        public void AddContent(string unitCode, string content, string code, int duration, string deliveryType, string trainingFormat, string note);
        public Task<String> EditContent(string unitCode, string contentId, TrainingContent trainingContent);
        public void DeleteContent(string contentId);
        public void DuplicateTrainingContents(string oldUnitCode, TrainingUnit newTrainingUnit);
        public TrainingContent GetTrainingContentByContentId(string contentId);
        List<TrainingContent> GetAllTrainingContentByUnitCode(string unitCode);
    }
}
