using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace FamsAPI.Services
{
    public class TrainingContentServices : ITrainingContent
    {
        private readonly TrainingContentRepository _trainingContentRepository;

        public TrainingContentServices(TrainingContentRepository trainingContentRepository)
        {
            _trainingContentRepository = trainingContentRepository;
        }
        public TrainingContent GetTrainingContentByContentId(string contentId)
        {
            var checkContent = _trainingContentRepository.Get(t => t.ContentId == contentId);
            return checkContent;
        }
        public void AddContent(string unitCode, string content, string code, int duration, string deliveryType, string trainingFormat, string note)
        {
            try
            {
                var newContent = new TrainingContent
                {
                    UnitCode = unitCode,
                    ContentId = GenerateUnitCode(),
                    Content = content,
                    Code = code,
                    Duration = duration,
                    DeliveryType = deliveryType,
                    TrainingFormat = trainingFormat,
                    Note = note
                };
                _trainingContentRepository.Add(newContent);
                _trainingContentRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the content.", ex);
            }
        }

        public void DuplicateTrainingContents(string oldUnitCode, TrainingUnit newTrainingUnit)
        {
            var oldUnitList = GetAllTrainingContentByUnitCode(oldUnitCode);
            foreach (var oldTrainingContent in oldUnitList)
            {
                var newTrainingContent = new TrainingContent
                {
                    UnitCode = newTrainingUnit.UnitCode,
                    ContentId = GenerateUnitCode(),
                    Content = oldTrainingContent.Content,
                    Code = oldTrainingContent.Code,
                    Duration = oldTrainingContent.Duration,
                    DeliveryType = oldTrainingContent.DeliveryType,
                    TrainingFormat = oldTrainingContent.TrainingFormat,
                    Note = oldTrainingContent.Note
                };

                _trainingContentRepository.Add(newTrainingContent);
                _trainingContentRepository.SaveChanges();
            }
        }

        public void DeleteContent(string contentId)
        {
            var checkContent = _trainingContentRepository.Get(c => c.ContentId == contentId);
            if (checkContent != null)
            {
                _trainingContentRepository.Delete(contentId);
                _trainingContentRepository.SaveChanges();
            }
        }

        public async Task<String> EditContent(string unitCode, string contentId, TrainingContent trainingContent)
        {
            try
            {
                var checkUnitCode = _trainingContentRepository.Get(u => u.UnitCode == unitCode && u.ContentId == trainingContent.ContentId);
                if (checkUnitCode != null)
                {
                    checkUnitCode.Content = trainingContent.Content;
                    checkUnitCode.Code = trainingContent.Code;
                    checkUnitCode.Duration = trainingContent.Duration;
                    checkUnitCode.DeliveryType = trainingContent.DeliveryType;
                    checkUnitCode.TrainingFormat = trainingContent.TrainingFormat;
                    checkUnitCode.Note = trainingContent.Note;

                    _trainingContentRepository.Update(checkUnitCode);
                    await _trainingContentRepository.SaveChangesAsync();

                    return "Update success";
                }
                else
                {
                    throw new ArgumentException("Content not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the content.", ex);
            }
        }

        public List<TrainingContent> GetAllTrainingContentByUnitCode(string unitCode)
        {
            try
            {
                var checkContent = _trainingContentRepository.GetAll()
                                                              .Where(u => u.UnitCode == unitCode)
                                                              .ToList();
                if (checkContent == null)
                {
                    throw new InvalidOperationException("Content not found.");
                }
                return checkContent;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string GenerateUnitCode()
        {

            int existingUnitCount =_trainingContentRepository.GetAll().Count();

            int nextSequentialNumber = existingUnitCount + 1;

            string formattedSequentialNumber = nextSequentialNumber.ToString("D8");

            string unitCode = "C" + formattedSequentialNumber;
            return unitCode;
        }
    }
}
