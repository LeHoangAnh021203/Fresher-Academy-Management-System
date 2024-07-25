using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using System.Security.Claims;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.IdentityModel.Tokens;
using static DataLayer.Entities.TrainingProgram;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FamsAPI.Services
{
    public class TrainingProgramService : ITrainingProgram
    {
        private readonly TrainingProgramRepository _trainingProgramRepository;
        private readonly TrainingProgramSyllabusRepository _trainingProgramSyllabusRepository;
        private readonly ClassRepository _classRepository;
        private readonly TrainingContentRepository _trainingContent;
        private readonly SyllabusRepository _syllabusRepository;

        public TrainingProgramService(TrainingProgramRepository trainingProgramRepository, SyllabusRepository syllabusRepository, TrainingProgramSyllabusRepository trainingProgramSyllabusRepository, ClassRepository classRepository, TrainingContentRepository trainingContent)
        {
            _trainingProgramRepository = trainingProgramRepository;
            _syllabusRepository = syllabusRepository;
            _trainingProgramSyllabusRepository = trainingProgramSyllabusRepository;
            _classRepository = classRepository;
            _trainingContent = trainingContent;
        }
       
        public TrainingProgramService(TrainingProgramRepository trainingProgramRepository, TrainingProgramSyllabusRepository trainingProgramSyllabusRepository)
        {
            _trainingProgramRepository = trainingProgramRepository;
            _trainingProgramSyllabusRepository = trainingProgramSyllabusRepository;
        }

        public TrainingProgram AddNewTrainingProgramAsync(ClaimsPrincipal user, string name, int duration, List<string> topicCode)
        {
            var tpCode = GenerateTrainingProgramCode();

            var userId = Guid.Parse(user.FindFirst("UserId")?.Value);

            try
            {
                if (duration == 0)
                {
                    throw new Exception("Duration is not null!!! ");
                }
                else if (name == null || name == "")
                {
                    throw new Exception("Name is not null!!! ");
                }
                else if (topicCode.Count == 0)
                {
                    throw new Exception("At least 1 syllabus must added into training program!!! ");
                }
                else
                {
                    var newProgram = new TrainingProgram
                    {
                        TrainingProgramCode = tpCode,
                        Name = name,
                        StartTime = DateTime.Now,
                        Duration = duration,
                        UserId = userId,
                        CreateBy = user.FindFirst("UserName")?.Value,
                        CreateDate = DateTime.Now,
                        Status = Statuses.Inactive,
                        ModifyDate = DateTime.MinValue,
                    };

                    _trainingProgramRepository.Add(newProgram);
                    _trainingProgramRepository.SaveChanges();

                    var listTPC = new List<TrainingProgramSyllabus>();

                    foreach (var topic in topicCode)
                    {
                        var newTPS = new TrainingProgramSyllabus

                        {
                            TopicCode = topic,
                            TrainingProgramCode = tpCode,
                            Sequence = 1
                        };
                        listTPC.Add(newTPS);
                    }
                    _trainingProgramSyllabusRepository.AddRange(listTPC);
                    _trainingProgramSyllabusRepository.SaveChanges();


                    return newProgram;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the training program." + ex.Message);
            }
        }


        private string GenerateTrainingProgramCode()
        {
            int existingUnitProgram = _trainingProgramRepository.GetAll().Count();
            int nextSequentialNumber = existingUnitProgram + 1;
            string formattedSequentialNumber = nextSequentialNumber.ToString("D3");
            string trainingCode = "TP" + formattedSequentialNumber;
            return trainingCode;
        }

        public List<TrainingProgramViewModel> SearchTrainingPrograms(string? keyword, string? createBy, string? createDate, int? duration, Statuses? status)
        {
            try
            {
                List<TrainingProgram> listTrainingProgramFinal;
                if (keyword == null && createBy == null && createDate == null && duration == null && status == null)
                {
                    return null;
                }
                // If a keyword is provided, retrieve training programs by keyword
                else if (keyword != null)
                {
                    listTrainingProgramFinal = _trainingProgramRepository.GetTrainingProgramsByKeyword(keyword);
                }
                // If no keyword is provided, retrieve training programs by filter criteria
                else
                {
                    listTrainingProgramFinal = _trainingProgramRepository.GetTrainingProgramsByFilter(createBy, createDate, duration, status);
                }

                // Filter and merge results if necessary
                if (listTrainingProgramFinal.Count > 0 && keyword != null)
                {
                    var listTrainingProgramByFilter = _trainingProgramRepository.GetTrainingProgramsByFilter(createBy, createDate, duration, status);
                    listTrainingProgramFinal = listTrainingProgramFinal.Intersect(listTrainingProgramByFilter).ToList();
                }

                // Convert to ViewModel
                List<TrainingProgramViewModel> listTrainingProgramViews = listTrainingProgramFinal
                    .Select(s => new TrainingProgramViewModel
                    {
                        TrainingProgramCode = s.TrainingProgramCode,
                        Name = s.Name,
                        StartTime = s.StartTime,
                        Duration = s.Duration,
                        CreateBy = s.CreateBy,
                        CreateDate = s.CreateDate,
                        Status = s.Status
                    })
                    .ToList();
                if (listTrainingProgramViews.IsNullOrEmpty())
                {
                    return null;
                }

                return listTrainingProgramViews;
            }
            catch (Exception ex)
            {
                // Throw an exception with the original message and stack trace
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateTrainingProgram(string trainingCode, string name, ClaimsPrincipal user)
        {
            try
            {
                var existingProgram = _trainingProgramRepository.Get(tp => tp.TrainingProgramCode == trainingCode);
                if (existingProgram == null)
                {
                    throw new Exception("Training program not found.");
                }
                string userName = user.FindFirst("UserName")?.Value;

                existingProgram.Name = name;
                existingProgram.ModifyBy = userName;
                existingProgram.ModifyDate = DateTime.Now;

                _trainingProgramRepository.Update(existingProgram);
                await _trainingProgramRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the training program.", ex);
            }
        }
        public async Task<bool> DeleteTrainingProgram(string trainingCode)
        {
            var checkProgram = _trainingProgramRepository.Get(tp => tp.TrainingProgramCode == trainingCode);
            if (checkProgram != null)
            {
                _trainingProgramRepository.Remove(checkProgram);
                await _trainingProgramRepository.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TrainingProgram> GetAllTrainingProgram()
        {
            try
            {
                var trainingProgram = _trainingProgramRepository.GetAll().ToList();
                if (trainingProgram == null )
                {
                    return null;
                }
                return trainingProgram;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<TrainingProgramDetailViewModel> ViewDetailTrainingProgramTrainer(string trainingCode)
        {
            try
            {
                var trainingProgram = _trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingCode);
                var classTraining = _classRepository.Get(c => c.TrainingProgramCode.ToLower() == trainingCode.ToLower());

                if (trainingProgram == null)
                {
                    return null;
                }

                // Check if class training status is not equal to 2
                //if (classTraining.Status != 2)
                //{
                //    return null;
                //}

                var syllabuses = new List<SyllabusInTrainingDetailViewModel>();

                foreach (var programSyllabus in trainingProgram.TrainingProgramSyllabuses)
                {
                    var syllabus = programSyllabus.Syllabus;

                    if (syllabus != null)
                    {
                        var trainingUnits = new List<TrainingUnitViewModelV2>();

                        foreach (var unit in syllabus.TrainingUnits)
                        {
                            var trainingUnitViewModel = new TrainingUnitViewModelV2
                            {
                                UnitCode = unit.UnitCode,
                                UnitName = unit.UnitName,
                                DayNumber = unit.DayNumber,
                                TopicCode = unit.TopicCode,
                                TrainingContents = unit.TrainingContents != null ?
                                    unit.TrainingContents.Select(tc => new TrainingContentViewModelV2
                                    {
                                        ContentId = tc.ContentId,
                                        Content = tc.Content,
                                        Code = tc.Code,
                                        DeliveryType = tc.DeliveryType,
                                        Duration = tc.Duration,
                                        TrainingFormat = tc.TrainingFormat
                                    }).ToList() :
                                    new List<TrainingContentViewModelV2>()
                            };
                            trainingUnits.Add(trainingUnitViewModel);
                        }

                        syllabuses.Add(new SyllabusInTrainingDetailViewModel
                        {
                            TopicName = syllabus.TopicName,
                            Version = syllabus.Version,
                            PulishStatus = syllabus.PulishStatus,
                            CreatedBy = syllabus.CreatedBy,
                            CreatedDate = syllabus.CreatedDate,
                            ModifiedBy = syllabus.ModifiedBy,
                            ModifiedDate = syllabus.ModifiedDate,
                            TrainingUnits = trainingUnits
                        });
                    }
                }

                var trainingProgramDetail = new TrainingProgramDetailViewModel
                {
                    TrainingProgramCode = trainingProgram.TrainingProgramCode,
                    Name = trainingProgram.Name,
                    Duration = trainingProgram.Duration,
                    ModifyBy = trainingProgram.ModifyBy,
                    ModifyDate = trainingProgram.ModifyDate,
                    Status = trainingProgram.Status,
                    Syllabuses = syllabuses
                };

                return trainingProgramDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }



        public async Task<TrainingProgramDetailViewModel> ViewDetailTrainingProgramAdmin(string trainingCode)
        {
            try
            {
                var trainingProgram = _trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingCode);
                var classTraining = _classRepository.GetClassByTrainingCode(c => c.TrainingProgramCode.ToLower() == trainingCode.ToLower());

                if (trainingProgram == null )
                {
                    return null;
                }

                var syllabuses = new List<SyllabusInTrainingDetailViewModel>();

                foreach (var programSyllabus in trainingProgram.TrainingProgramSyllabuses)
                {
                    var syllabus = programSyllabus.Syllabus;

                    if (syllabus != null)
                    {
                        var trainingUnits = new List<TrainingUnitViewModelV2>();

                        foreach (var unit in syllabus.TrainingUnits)
                        {
                            var trainingUnitViewModel = new TrainingUnitViewModelV2
                            {
                                UnitCode = unit.UnitCode,
                                UnitName = unit.UnitName,
                                DayNumber = unit.DayNumber,
                                TopicCode = unit.TopicCode,
                                TrainingContents = unit.TrainingContents != null ?
                        unit.TrainingContents.Select(tc => new TrainingContentViewModelV2
                        {
                            ContentId = tc.ContentId,
                            Content = tc.Content,
                            Code = tc.Code,
                            DeliveryType = tc.DeliveryType,
                            Duration = tc.Duration,
                            TrainingFormat = tc.TrainingFormat
                        }).ToList() :
                        new List<TrainingContentViewModelV2>()
                            };
                            trainingUnits.Add(trainingUnitViewModel);
                        }

                        syllabuses.Add(new SyllabusInTrainingDetailViewModel
                        {
                            TopicCode = syllabus.TopicCode,
                            TopicName = syllabus.TopicName,
                            Version = syllabus.Version,
                            PulishStatus = syllabus.PulishStatus,
                            CreatedBy = syllabus.CreatedBy,
                            CreatedDate = syllabus.CreatedDate,
                            ModifiedBy = syllabus.ModifiedBy,
                            ModifiedDate = syllabus.ModifiedDate,
                            TrainingUnits = trainingUnits
                        });
                    }
                }

                var trainingProgramDetail = new TrainingProgramDetailViewModel
                {
                    TrainingProgramCode = trainingProgram.TrainingProgramCode,
                    Name = trainingProgram.Name,
                    Duration = trainingProgram.Duration,
                    Status = trainingProgram.Status,
                    ModifyBy = trainingProgram.ModifyBy,
                    ModifyDate = trainingProgram.ModifyDate,
                    Syllabuses = syllabuses
                };

                return trainingProgramDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> RemoveSyllabusFromTrainingProgram(string trainingProgramCode, string topicCode)
        {
            try
            {
                // Find the training program including related syllabuses
                var trainingProgram = _trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode);
                if (trainingProgram == null)
                {
                    return false; // Training program not found
                }

                // Find the syllabus to be removed
                var syllabusToRemove = trainingProgram.TrainingProgramSyllabuses.FirstOrDefault(tps => tps.Syllabus.TopicCode == topicCode);
                if (syllabusToRemove == null)
                {
                    return false; // Syllabus not found in the training program
                }

                // Remove the syllabus from the training program
                trainingProgram.TrainingProgramSyllabuses.Remove(syllabusToRemove);
                await _trainingProgramRepository.SaveChangesAsync();

                return true; // Successfully removed the syllabus from the training program
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the syllabus from the training program.", ex);
            }
        }
        public async Task<bool> AddSyllabusToTrainingProgram(string trainingProgramCode, string topicCode)
        {
            try
            {
                var trainingProgram = _trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode);
                if (trainingProgram == null)
                {
                    return false; // Training program not found
                }

                var syllabus = _syllabusRepository.GetSyllabusByTopicCode(topicCode);
                if (syllabus == null)
                {
                    return false; // Syllabus not found
                }

                var existingMapping = _trainingProgramSyllabusRepository.Get(tps =>
                    tps.TrainingProgramCode == trainingProgramCode && tps.TopicCode == topicCode);
                if (existingMapping != null)
                {
                    return false; // Syllabus already exists in the training program
                }

                var newMapping = new TrainingProgramSyllabus
                {
                    TrainingProgramCode = trainingProgramCode,
                    TopicCode = topicCode,
                    Sequence = 1 // You may need to adjust this sequence based on your requirements
                };

                _trainingProgramSyllabusRepository.Add(newMapping);
                await _trainingProgramSyllabusRepository.SaveChangesAsync();

                return true; // Successfully added the syllabus to the training program
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the syllabus to the training program.", ex);
            }
        }

        public async void UpdateTrainingProgramStatus(string trainingProgramCode, ClaimsPrincipal user)
        {
            try
            {
                var getTrainingProgram = _trainingProgramRepository.GetAll().FirstOrDefault(c => c.TrainingProgramCode.Equals(trainingProgramCode));
                if(getTrainingProgram is null)
                {
                    throw new Exception("The training program is not existed!");
                }
                else
                {
                    Statuses statusTrainingProgram = getTrainingProgram.Status;
                    switch (statusTrainingProgram)
                    {
                        case Statuses.Active:
                            getTrainingProgram.Status = Statuses.Inactive;
                            
                            break;

                        case Statuses.Inactive:
                            getTrainingProgram.Status = Statuses.Active;
                            break;
                    }
                    getTrainingProgram.ModifyBy = user.FindFirst("UserName")?.Value;
                    getTrainingProgram.ModifyDate = DateTime.Now;
                    _trainingProgramRepository.Update(getTrainingProgram);
                    _trainingProgramRepository.SaveChangesAsync().Wait();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           

        }
        public TrainingProgram GetTrainingProgramByTrainingProgramCode(string code)
        {
            try
            {
                var tp = _trainingProgramRepository.GetTrainingProgrambyTrainingCode(code.ToUpper());
                if(tp is null)
                {
                    throw new Exception("Training Program is not existed");
                }
                else
                {
                    return tp;
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

    }

}
