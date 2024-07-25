using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using static DataLayer.Entities.Syllabus;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using AutoMapper;

namespace FamsAPI.Services
{
    public class SyllabusServices : ISyllabus
    {
        #region declaration
        private readonly SyllabusRepository _syllabusRepository;
        private readonly TrainingContentRepository _trainingContentRepository;
        private readonly IAssessment _assessmentService;
        private readonly ITrainingUnit _trainingUnitService;
        private readonly ISyllabusObjective _syllabusObjectiveService;
        private readonly FAMSDBContext _context;
        private readonly AssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;
        private readonly LearningObjectiveRepository _learningRepository;
        private readonly TrainingUnitRepository _trainingUnitRepository;
        private readonly SyllabusObjectiveRepository _syllabusObjectiveRepository;

        private static string AssessmentIDCreated;
        private static string SyllabusTopicCodeCreated;
        private static string UnitCodeCreated;
        private AssessmentRepository assessmentRepository;
        private SyllabusRepository syllabusRepository;
        private TrainingUnitRepository trainingUnitRepository;
        private TrainingContentRepository trainingContentRepository;
        private SyllabusObjectiveRepository syllabusObjectiveRepository;

        public SyllabusServices(SyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
        }


        public SyllabusServices(SyllabusRepository syllabusRepository,
            TrainingUnitRepository trainingUnitRepository, LearningObjectiveRepository learningObjective,
            TrainingContentRepository trainingContentRepository, ITrainingUnit trainingUnitService,
            ISyllabusObjective syllabusObjectiveService, IAssessment assessmentService,
            AssessmentRepository assessmentRepository, FAMSDBContext context, SyllabusObjectiveRepository syllabusObjectiveRepository, IMapper mapper)
        {
            _syllabusRepository = syllabusRepository;
            _trainingContentRepository = trainingContentRepository;
            _assessmentService = assessmentService;
            _mapper = mapper;
            _trainingUnitService = trainingUnitService;
            _syllabusObjectiveService = syllabusObjectiveService;
            _context = context;
            _assessmentRepository = assessmentRepository;
            _learningRepository = learningObjective;
            _trainingUnitRepository = trainingUnitRepository;
            _syllabusObjectiveRepository = syllabusObjectiveRepository;
            _mapper = mapper;
        }

       
        #endregion

        #region Update Syllabus
        public async Task<Syllabus> UpdateSyllabus(Syllabus syllabus, ClaimsPrincipal user)
        {
            try
            {
                var existingSyllabus = _syllabusRepository.Get(s => s.TopicCode == syllabus.TopicCode);
                if (existingSyllabus != null)
                {
                    existingSyllabus.TopicName = syllabus.TopicName;
                    existingSyllabus.TechnicalGroup = syllabus.TechnicalGroup;
                    existingSyllabus.TechnicalRequirement = syllabus.TechnicalRequirement;
                    existingSyllabus.CourseObjective = syllabus.CourseObjective;
                    existingSyllabus.Version+=1 ;
                    existingSyllabus.TrainingAudience = syllabus.TrainingAudience;
                    existingSyllabus.TopicOutline = syllabus.TopicOutline;
                    existingSyllabus.TrainingMaterials = syllabus.TrainingMaterials;
                    existingSyllabus.TrainingPrinciple = syllabus.TrainingPrinciple;
                    existingSyllabus.Priority = syllabus.Priority;
                    existingSyllabus.PulishStatus = syllabus.PulishStatus;
                    string userName = user.FindFirst("UserName")?.Value;
                    existingSyllabus.ModifiedBy = userName;
                    existingSyllabus.ModifiedDate = DateTime.Now;
                    _syllabusRepository.Update(existingSyllabus);
                    await _syllabusRepository.SaveChangesAsync();

                    return existingSyllabus;
                }
                else
                {
                    throw new Exception("Syllabus not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public SyllabusViewModel DuplicateSyllabus(string oldTopicCode)
        {
            try
            {
                // Get the old syllabus
                Syllabus oldSyllabus = _syllabusRepository.SearchSyllabusByTopicCode(oldTopicCode);

                if (oldSyllabus == null)
                {
                    // Handle the case where the old syllabus is not foundge
                    return null;
                }
                // Duplicate Assessment
                var assessment = _assessmentService.DuplicateAssessment(oldSyllabus.AssessmentID);

                // Save the old syllabus as a draft and get the new topic code
                string newTopicCode = DuplicateSyllabusGeneralAsDraft(oldSyllabus, assessment);

                // Get the new syllabus
                var newSyllabus = _syllabusRepository.SearchSyllabusByTopicCode(newTopicCode);
                 
                if (newSyllabus == null)
                {
                    // Handle the case where the new syllabus is not found
                    return null;
                }
                //Generate a SyllabusViewModel
                var syllabusViewModel = new SyllabusViewModel
                {
                    TopicCode = newSyllabus.TopicCode,
                    TopicName = newSyllabus.TopicName,
                    TechnicalGroup = newSyllabus.TechnicalGroup,
                    TechnicalRequirement = newSyllabus.TechnicalRequirement,
                    CourseObjective = newSyllabus.CourseObjective,
                    Version = newSyllabus.Version,
                    TrainingAudience = newSyllabus.TrainingAudience,
                    TopicOutline = newSyllabus.TopicOutline,
                    TrainingMaterials = newSyllabus.TrainingMaterials,
                    TrainingPrinciple = newSyllabus.TrainingPrinciple,
                    Priority = newSyllabus.Priority,
                    PulishStatus = newSyllabus.PulishStatus,
                    CreatedBy = newSyllabus.CreatedBy,
                    CreatedDate = newSyllabus.CreatedDate,
                    ModifiedBy = newSyllabus.ModifiedBy,
                    ModifiedDate = newSyllabus.ModifiedDate,
                    UserId = newSyllabus.UserId,
                    AssessmentID = newSyllabus.AssessmentID
                };

                // Duplicate training units and contents
                _trainingUnitService.DuplicateTrainingUnitsAndContents(oldTopicCode, syllabusViewModel);

                // Duplicate SyllabusObjectives
                _syllabusObjectiveService.DuplicateSyllabusObjectives(oldTopicCode, newSyllabus);

                return syllabusViewModel;            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SyllabusSearchViewModel> SearchSyllabusByDate(string createdDate)
        {
            var syllabuses = _syllabusRepository.SearchSyllabusByCreatedDate(createdDate);

            if (syllabuses == null || !syllabuses.Any())
            {
                // Handle the case where the syllabuses are not found, e.g., return null or throw an exception
                return null;
            }

            var syllabusViewModels = syllabuses.Select(s => new SyllabusSearchViewModel
            {
                TopicCode = s.TopicCode,
                TopicName = s.TopicName,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Duration = s.TrainingUnits?.SelectMany(u => u.TrainingContents)?.Sum(c => c.Duration) ?? 0,
                PublishStatus = (int)s.PulishStatus,
                OutputStandard = s.SyllabusObjectives?.Select(o => o.ObjectiveCode)?.FirstOrDefault()
            }).ToList();

            return syllabusViewModels;
        }

        public List<SyllabusSearchViewModel> SearchSyllabusByKeyword(string keyword)
        {
            var syllabuses = _syllabusRepository.SearchSyllabusByKeyword(keyword);


            if (syllabuses == null)
            {
                // Handle the case where the syllabuses are not found, e.g., return null or throw an exception
                return null;
            }

            var syllabusViewModels = syllabuses.Select(s => new SyllabusSearchViewModel
            {
                TopicCode = s.TopicCode,
                TopicName = s.TopicName,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Duration = s.TrainingUnits?.SelectMany(u => u.TrainingContents)?.Sum(c => c.Duration) ?? 0,
                PublishStatus = (int)s.PulishStatus,
                OutputStandard = s.SyllabusObjectives?.Select(o => o.ObjectiveCode)?.FirstOrDefault()
            }).ToList();

            return syllabusViewModels;
        }

        public virtual List<Object> GetAllSyllabuses()
        {
            try
            {
                var syllabuses = _syllabusRepository.getAllSyllabus();


                if (syllabuses == null)
                {
                    // Handle the case where the syllabuses are not found, e.g., return null or throw an exception
                    return null;
                }


                var syllabusViewModels = syllabuses.Select(s => new
                {
                    s.TopicCode,
                    s.TopicName,
                    s.CreatedBy,
                    s.CreatedDate,
                    s.ModifiedBy,
                    s.ModifiedDate,
                    Duration = s.TrainingUnits?.SelectMany(u => u.TrainingContents)?.Sum(c => c.Duration) ?? 0,
                    PublishStatus = (int)s.PulishStatus,
                    OutputStandard = s.SyllabusObjectives.Select(o => o.ObjectiveCode)
                }).ToList<Object>();
                return syllabusViewModels;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DuplicateSyllabusGeneralAsDraft(Syllabus syllabus, Assessment assessment)
        {
            try
            {
                syllabus.Assessment = assessment;
                syllabus.Priority = Priorities.Low;
                syllabus.PulishStatus = PulishStatuses.Pending;
                string TopicCode = GenerateDuplicateTopicCode(syllabus.TechnicalGroup);
                syllabus.TopicCode = TopicCode;
                string TechnicalRequirement = syllabus.TechnicalRequirement;
                string CourseObjective = syllabus.CourseObjective;
                syllabus.CreatedDate = DateTime.Now;
                _syllabusRepository.Add(syllabus);
                _syllabusRepository.SaveChanges();
                return TopicCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string GenerateDuplicateTopicCode(string technicalGroup)
        {
            if (technicalGroup.StartsWith("."))
            {
                technicalGroup = technicalGroup.Substring(1);
            }

            //Get List Syllabus Start with 
            var listSyllabus = _syllabusRepository.SearchSyllabusTechnicalGroup(technicalGroup);
            string topicName;

            //Not exist code start with technicalGroup first letter
            if (listSyllabus.Count() == 0)
            {
                topicName = technicalGroup[0] + "01";
            }
            else
            {
                //Get list of syllabus have technical group as input
                var listExistCode = listSyllabus.Where(s => s.TechnicalGroup.Trim().Equals(technicalGroup));
                //Not Exist => Not exist code have technical group of input => The same first letter
                if (listExistCode.Count() == 0)
                {
                    topicName = technicalGroup.Substring(0, 2) + "01";
                }
                else
                {
                    var lastSyllabus = listExistCode.OrderByDescending(code => code.TopicCode).FirstOrDefault();
                    int numberOfStartLetter = lastSyllabus.TopicCode.Count(char.IsLetter);
                    string lastId = lastSyllabus.TopicCode;
                    string topicStartCode = technicalGroup.Substring(0, numberOfStartLetter);
                    int topicNumber = int.Parse(lastId.Substring(numberOfStartLetter)) + 1;
                    if (topicNumber < 10)
                    {
                        topicName = topicStartCode + "0" + topicNumber;
                    }
                    else
                    {
                        topicName = topicStartCode + topicNumber;
                    }
                }
            }
            return topicName;
        }


        public async Task<string> SaveSyllabusAsDraftGeneral(Syllabus syllabus, ClaimsPrincipal user)
        {
            try
            {
                syllabus.Priority = Priorities.Low;
                syllabus.PulishStatus = PulishStatuses.Pending;
                string TopicCode = GenerateTopicCode(syllabus.TechnicalGroup);
                syllabus.TopicCode = TopicCode;
                string TechnicalRequirement = syllabus.TechnicalRequirement;
                string CourseObjective = syllabus.CourseObjective;
                syllabus.CreatedDate = DateTime.Now;
                string userName = user.FindFirst("UserName")?.Value;
                syllabus.CreatedBy = userName;
                _context.Syllabuses.Add(syllabus);
                await _context.SaveChangesAsync();
                /* _context.LearningObjectives.Add(syllabus.SyllabusObjectives.First().LearningObjective);
                 await _context.SaveChangesAsync();
                 syllabus.SyllabusObjectives.First().TopicCode = TopicCode;
                 syllabus.SyllabusObjectives.First().ObjectiveCode = syllabus.SyllabusObjectives.First().LearningObjective.Code;

                 _context.SyllabusObjectives.Add(syllabus.SyllabusObjectives.First());
                 await _context.SaveChangesAsync();*/
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GenerateTopicCode(string technicalGroup)
        {
            if (technicalGroup.StartsWith("."))
            {
                technicalGroup = technicalGroup.Substring(1);
            }

            //Get List Syllabus Start with 
            var listSyllabus = _syllabusRepository.GetAll().Where(s => s.TopicCode.StartsWith(technicalGroup.Substring(0, 1)));
            string topicName;

            //Not exist code start with technicalGroup first letter
            if (!listSyllabus.Any())
            {
                topicName = technicalGroup[0] + "01";
            }
            else
            {
                //Get list of syllabus have technical group as input
                var listExistCode = listSyllabus.Where(s => s.TechnicalGroup.Trim().Equals(technicalGroup));
                //Not Exist => Not exist code have technical group of input => The same first letter
                if (!listExistCode.Any())
                {
                    topicName = technicalGroup.Substring(0, 2) + "01";
                }
                else
                {
                    var lastSyllabus = listExistCode.OrderByDescending(code => code).FirstOrDefault();
                    int numberOfStartLetter = lastSyllabus.TopicCode.Count(char.IsLetter);
                    string lastId = lastSyllabus.TopicCode;
                    string topicStartCode = technicalGroup.Substring(0, numberOfStartLetter);
                    int topicNumber = int.Parse(lastId.Substring(numberOfStartLetter)) + 1;
                    if (topicNumber < 10)
                    {
                        topicName = topicStartCode + "0" + topicNumber;
                    }
                    else
                    {
                        topicName = topicStartCode + topicNumber;
                    }
                }
            }
            return topicName;
        }

        public async Task<SyllabusDetailViewModel> ViewSyllabusDetail(string key)
        {
            try
            {
                var syllabuses = _syllabusRepository.GetByKeyword(key);
                if (syllabuses == null)
                {
                    return null;
                }
                
                var trainingUnit = _trainingUnitRepository.GetByKeyword(key);
                if (trainingUnit == null)
                {
                    return null;
                }
                
                var assessment = _assessmentRepository.Get(a => a.AssessmentID == syllabuses.AssessmentID);
                if (assessment == null)
                {
                    return null;
                }


                var traiNingContent = new List<TrainingContent>();
                var traiNingUnits = new List<TrainingUnit>();

                foreach (var unit in syllabuses.TrainingUnits)
                {
                    var unitCode = unit.UnitCode;

                    // Get the list of TrainingContent based on UnitCode
                    var unitContents = _trainingContentRepository.GetByUnitCode(unitCode)?.Select(content => new TrainingContent
                    {
                        ContentId = content.ContentId,
                        Content = content.Content,
                        Code = content.Code,
                        DeliveryType = content.DeliveryType,
                        Duration = content.Duration
                    }).ToList();

                    traiNingUnits.Add(new TrainingUnit
                    {
                        UnitCode = unit.UnitCode,
                        UnitName = unit.UnitName,
                        DayNumber = unit.DayNumber,
                        TopicCode = unit.TopicCode,
                        TrainingContents = unitContents // Set the TrainingContents property here
                    });
                }

                var syllabusDetail = new SyllabusDetailViewModel
                {
                    TopicCode = syllabuses.TopicCode,
                    TopicName = syllabuses.TopicName,
                    TechnicalGroup = syllabuses.TechnicalGroup,
                    Version = syllabuses.Version,
                    TrainingAudience = syllabuses.TrainingAudience,
                    TopicOutline = syllabuses.TopicOutline,
                    Priority = syllabuses.Priority,
                    PulishStatus = syllabuses.PulishStatus,
                    CreatedBy = syllabuses.CreatedBy,
                    CreatedDate = syllabuses.CreatedDate,
                    ModifiedBy = syllabuses.ModifiedBy,
                    ModifiedDate = syllabuses.ModifiedDate,
                    UserId = syllabuses.UserId,
                    TechnicalRequirement = syllabuses.TechnicalRequirement,
                    CourseObjective = syllabuses.CourseObjective,
                    TrainingMaterials = syllabuses.TrainingMaterials,
                    TrainingPrinciple = syllabuses.TrainingPrinciple,
                    Assessment = new Assessment
                    {
                        AssessmentID = assessment.AssessmentID,
                        QuizCount = assessment.QuizCount,
                        QuizPercent = assessment.QuizPercent,
                        AssignmentCount = assessment.AssignmentCount,
                        AssignmentPercent = assessment.AssignmentPercent,
                        FinalTheoryPercent = assessment.FinalTheoryPercent,
                        FinalPracticePercent = assessment.FinalPracticePercent
                    },
                    TrainingUnits = traiNingUnits,
                    TrainingContents = traiNingContent,
                };

                return syllabusDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #region GetDataFromExcelFile
        /// <summary>
        /// This is Import Data from Excel file which pass from API and processing right here
        /// </summary>
        /// <param name="fileDir">This is the directory of file after save in tempory folder</param>
        public async Task<string> GetDataFromExcelFile(string fileDir, ClaimsPrincipal user)
        {
            try
            {
                using ExcelPackage package = new ExcelPackage(new FileInfo(fileDir));
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var sheet = package.Workbook.Worksheets["<Topic Code>_Syllabus"];
                string hashPass = sheet.Cells["H2"].Text;
                bool isValidTemplate = ComputeSHA256(hashPass);
                if (isValidTemplate == true)
                {
                    GetDataForAssessment(fileDir);
                    _assessmentRepository.SaveChanges();


                    GetDataFromSyllabus(fileDir, user);
                    _syllabusRepository.SaveChanges();

                    GetLearningObjective(fileDir);

                    var trainingContentsList = GetTrainingContent(fileDir);



                    _learningRepository.SaveChanges();

                    _trainingContentRepository.SaveChanges();

                    return "";
                }
                else
                {
                    return "The file is not valid // Or this is not template from us. Please considering your file upload";

                }
            }
            catch (Exception ex)

            {
                return ex.Message;
            }

        }
        #endregion

        #region Get Data from Syllabus Sheet
        private void GetDataFromSyllabus(string fileDir, ClaimsPrincipal user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("Session is invalided");
                }
                else
                {

                    Syllabus syllabus = new Syllabus();

                    using ExcelPackage package = new ExcelPackage(new FileInfo(fileDir));
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    var sheet = package.Workbook.Worksheets["<Topic Code>_Syllabus"];
                    //check field is null or not
                    syllabus.TechnicalGroup = sheet.Cells["C2"].Text;

                    syllabus.TopicName = sheet.Cells["A1"].Text;
                    //if the value of Version in sheet is not a number, the default value is 1

                    bool versionNumberic = int.TryParse(sheet.Cells["C4"].Text, out int version);
                    if (versionNumberic) syllabus.Version = version;
                    else syllabus.Version = 1;

                    //Add list Learning Objective
                    syllabus.CourseObjective = sheet.Cells["C15"].Text;
                    //this line will be changed due to missing information about lessons of each day ;
                    syllabus.TopicOutline = sheet.Cells["C17"].Text;
                    syllabus.TrainingMaterials = sheet.Cells["D24"].Text;
                    syllabus.TechnicalRequirement = sheet.Cells["D26"].Text;
                    syllabus.TrainingAudience = sheet.Cells["C5"].Text;

                    string trainingPrinciple = "";
                    for (int row = 31; row <= 37; row++)
                    {
                        var dataRow = sheet.Cells[row, 3, row, 4];
                        var dataValue = dataRow.Select(cell => cell.Text).ToList();
                        trainingPrinciple += dataValue[0] + ": \n" + dataValue[1] + ".\n";
                    }
                    syllabus.TrainingPrinciple = trainingPrinciple;

                    string shortTechnicalGroup = sheet.Cells["G2"].Text;


                    syllabus.TopicCode = GenerateTopicCodeForImport(shortTechnicalGroup);


                    syllabus.PulishStatus = Syllabus.PulishStatuses.Editing;


                    syllabus.CreatedDate = DateTime.Now;
                    syllabus.ModifiedDate = DateTime.Now;
                    syllabus.CreatedBy = user.FindFirst("UserName")?.Value;
                    syllabus.AssessmentID = AssessmentIDCreated;
                    string uid = user.FindFirst("UserId")?.Value;
                    Guid userID = Guid.Parse(uid);

                    //var user = _userRepository.Get(u => u.UserId == userID);

                    //syllabus.CreatedBy = user.Name;
                    //syllabus.ModifiedBy = user.Name;
                    syllabus.UserId = userID;

                    SyllabusTopicCodeCreated = syllabus.TopicCode;

                    _syllabusRepository.Add(syllabus);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetLearningObjective
        private List<LearningObjective> GetLearningObjective(string fileDir)
        {
            int result = 0;
            List<LearningObjective> learningObjectives = new List<LearningObjective>();
            List<SyllabusObjective> syllabusObjectives = new List<SyllabusObjective>();
            using ExcelPackage package = new ExcelPackage(new FileInfo(fileDir));


            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var sheet = package.Workbook.Worksheets["<Topic Code>_Syllabus"];

            for (int row = 8; row <= 14; row++)
            {
                var dataRow = sheet.Cells[row, 3, row, 6];
                var dataValues = dataRow.Select(cell => cell.Text).ToList();

                var code = dataValues[1];
                if (code == "" || code is null) break;
                if (_learningRepository.Get(l => l.Code != code) == null) // Check if a LearningObjective with the same Code already exists
                {
                    var learningObjective = new LearningObjective
                    {
                        Name = dataValues[0],
                        Code = code,
                        Description = dataValues[2],
                        Type = "",
                    };

                    learningObjectives.Add(learningObjective);
                    result = _learningRepository.SaveChanges();


                    var syllabusObjective = new SyllabusObjective
                    {
                        TopicCode = SyllabusTopicCodeCreated,
                        ObjectiveCode = learningObjective.Code
                    };
                    syllabusObjectives.Add(syllabusObjective);
                    result = _learningRepository.SaveChanges();

                }
                else
                {
                    var syllabusObjective = new SyllabusObjective
                    {
                        TopicCode = SyllabusTopicCodeCreated,
                        ObjectiveCode = code
                    };
                    syllabusObjectives.Add(syllabusObjective);
                    result = _learningRepository.SaveChanges();
                }
            }
            _learningRepository.AddRange(learningObjectives);
            _learningRepository.SaveChanges();

            _syllabusObjectiveRepository.AddRange(syllabusObjectives);
            _syllabusObjectiveRepository.SaveChanges();

            return learningObjectives;
        }
        #endregion

        #region Get Data For Assessment
        private void GetDataForAssessment(string fileDir)
        {
            try
            {
                Assessment assessment = new Assessment();
                using ExcelPackage package = new ExcelPackage(new FileInfo(fileDir));
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var sheet = package.Workbook.Worksheets["<Topic Code>_Syllabus"];

                assessment.AssessmentID = GenerateAssessmentID();

                if (int.TryParse(sheet.Cells["D27"].Text, out int quizCount)) assessment.QuizCount = quizCount;
                else assessment.QuizCount = 1;

                if (int.TryParse(sheet.Cells["D28"].Text, out int assignmentCount)) assessment.AssignmentCount = assignmentCount;
                else assessment.AssignmentCount = 1;

                assessment.AssignmentPercent = (double)sheet.Cells["E28"].Value;
                assessment.QuizPercent = (double)sheet.Cells["E27"].Value;
                assessment.FinalTheoryPercent = (double)sheet.Cells["E29"].Value;

                assessment.FinalPracticePercent = (double)sheet.Cells["E30"].Value;

                //store Assessment ID for fill the db;
                AssessmentIDCreated = assessment.AssessmentID;

                _assessmentRepository.Add(assessment);


            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Get All Training Content
        /// <summary>
        /// This is a function will get the schedule details from excel file to object
        /// </summary>
        /// <param name="fileDir">string - directory of file</param>
        /// <returns>List<Training Unit> which will contain List<TrainingContent></returns>
        private List<TrainingContent> GetTrainingContent(string fileDir)
        {
            try
            {
                int result = 0;
                List<TrainingUnit> trainingUnitsList = new List<TrainingUnit>();
                List<TrainingContent> trainingContentsList = new List<TrainingContent>();

                using ExcelPackage package = new ExcelPackage(new FileInfo(fileDir));
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var sheet = package.Workbook.Worksheets["<Topics Code>_ScheduleDetail"];

                string dataDayCurrent;
                string dataUnitCurrent;

                int dayValue = 1;
                //First i will adding Training Unit Table
                for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                {

                    string currentDayCells = "A" + i;

                    //checking if the cell Day have color different with null, the for loop will break
                    var colourCells = sheet.Cells[i, 1].Style.Fill.BackgroundColor.Rgb;
                    if (colourCells != null) break;

                    string currentTrainingUnitCells = "B" + i;

                    dataDayCurrent = sheet.Cells[currentDayCells].Text;
                    dataUnitCurrent = sheet.Cells[currentTrainingUnitCells].Text;

                    if (!dataUnitCurrent
                        .IsNullOrEmpty())
                    {
                        if (!dataDayCurrent.IsNullOrEmpty())
                        {
                            if (int.TryParse(sheet.Cells[currentDayCells].Text, out dayValue))
                            {
                                int countTrainingUnitListCurrently = trainingUnitsList.Count;
                                var trainingUnit = new TrainingUnit
                                {
                                    TopicCode = SyllabusTopicCodeCreated,
                                    DayNumber = dayValue,
                                    UnitCode = GenerateUnitCodeForImport(countTrainingUnitListCurrently),
                                    UnitName = sheet.Cells[currentTrainingUnitCells].Text
                                };
                                trainingUnitsList.Add(trainingUnit);
                                result = _trainingUnitRepository.SaveChanges();
                                UnitCodeCreated = trainingUnit.UnitCode;
                            }

                        }
                        else
                        {
                            int countTrainingUnitListCurrently = trainingUnitsList.Count;
                            var trainingUnit = new TrainingUnit
                            {

                                TopicCode = SyllabusTopicCodeCreated,
                                DayNumber = dayValue,
                                UnitCode = GenerateUnitCodeForImport(countTrainingUnitListCurrently),
                                UnitName = sheet.Cells[currentTrainingUnitCells].Text
                            };
                            trainingUnitsList.Add(trainingUnit);
                            result = _trainingUnitRepository.SaveChanges();

                            UnitCodeCreated = trainingUnit.UnitCode;
                        }

                    }
                    int countContentListCurrently = trainingContentsList.Count();
                    string contentAddress = "D" + i;
                    string codeAddress = "E" + i;
                    string deliverytypeAddress = "F" + i;
                    string durationAddress = "G" + i;
                    string trainingFormatAddress = "H" + i;
                    string noteAddress = "I" + i;

                    var trainingContent = new TrainingContent
                    {
                        Content = sheet.Cells[contentAddress].Text,
                        ContentId = GenerateContentIdForImport(countContentListCurrently),
                        Code = sheet.Cells[codeAddress].Text,
                        DeliveryType = sheet.Cells[deliverytypeAddress].Text,
                        Duration = Convert.ToInt32(sheet.Cells[durationAddress].Value),
                        TrainingFormat = sheet.Cells[trainingFormatAddress].Text,
                        Note = sheet.Cells[noteAddress].Text,
                        UnitCode = UnitCodeCreated,
                    };

                    trainingContentsList.Add(trainingContent);
                    result = _trainingContentRepository.SaveChanges();
                }
                _trainingUnitRepository.AddRange(trainingUnitsList);
                _trainingContentRepository.AddRange(trainingContentsList);
                _trainingUnitRepository.SaveChanges();
                _trainingContentRepository.SaveChanges();
                return trainingContentsList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        private string GenerateTopicCodeForImport(string shortGroupTechnical)
        {
            int countAssessment = _syllabusRepository.GetAll().Where(c => c.TopicCode.Contains(shortGroupTechnical)).Count();

            int nextSequenceNumber = countAssessment + 1;

            string formattedSequentialNumber = nextSequenceNumber.ToString("D5");

            string topicCode = shortGroupTechnical + formattedSequentialNumber;
            return topicCode;
        }

        private string GenerateUnitCodeForImport(int existingListCount)
        {

            int existingUnitCount = _trainingUnitRepository.GetAll().Count();

            existingUnitCount += existingListCount;

            int nextSequentialNumber = existingUnitCount + 1;

            string formattedSequentialNumber = nextSequentialNumber.ToString("D8");

            string unitCode = "U" + formattedSequentialNumber;
            return unitCode;
        }

        private string GenerateContentIdForImport(int existingListCount)
        {

            int existingContentCount = _trainingContentRepository.GetAll().Count();

            existingContentCount += existingListCount;

            int nextSequentialNumber = existingContentCount + 1;

            string formattedSequentialNumber = nextSequentialNumber.ToString("D5");

            string unitCode = "Co" + formattedSequentialNumber;
            return unitCode;
        }
        //this will automatically auth the right template
        private static bool ComputeSHA256(string input)
        {
            string password = "b60ca76df7830852ef344b334f1f42206b26dde75786daba1f595ef5e6aaf552";
            string match;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                match = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            return password.Equals(match);
        }

        public string GenerateAssessmentID()
        {
            int countAssessment = _assessmentRepository.GetAll().Count();

            int nextSequenceNumber = countAssessment + 1;

            string formattedSequentialNumber = nextSequenceNumber.ToString("D2");

            string assessmentID = "AS" + formattedSequentialNumber;
            return assessmentID;
        }

        public virtual async Task<string> AddNewSyllabusWithAssessmentAndUnit(string topicName, string technicalGroup, string trainingAudience, string topicOutline, string trainingMatirials, string trainingPrinciple, string courseObjective, string technicalRequirement, ClaimsPrincipal user, int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe, List<TrainingUnitViewModel> trainingUnits)
        {
            try
            {
                // Add new assessment
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

                // Save syllabus as draft
                var syllabus = new Syllabus();
                syllabus.TopicName = topicName;
                syllabus.TechnicalGroup = technicalGroup;
                syllabus.TrainingAudience = trainingAudience;
                syllabus.TopicOutline = topicOutline;
                syllabus.TrainingMaterials = trainingMatirials;
                syllabus.TrainingPrinciple = trainingPrinciple;
                syllabus.Priority = Priorities.Low;
                syllabus.PulishStatus = PulishStatuses.Pending;
                syllabus.TopicCode = GenerateTopicCode(technicalGroup);
                syllabus.TechnicalRequirement = technicalRequirement;
                syllabus.CourseObjective = courseObjective;
                syllabus.CreatedDate = DateTime.Now;
                string userName = user.FindFirst("UserName")?.Value;
                syllabus.UserId = Guid.Parse(user.FindFirst("UserId")?.Value);
                syllabus.CreatedBy = userName;
                syllabus.AssessmentID = assessment.AssessmentID;
                _syllabusRepository.Add(syllabus);
                _syllabusRepository.SaveChanges();

                int existingUnitCount = _trainingUnitRepository.GetAll().Count();
                int existingContentCount = _trainingContentRepository.GetAll().Count();
                List<TrainingUnit> newUnits = new List<TrainingUnit>();

                foreach (var unitViewModel in trainingUnits)
                {
                    var unit = _mapper.Map<TrainingUnit>(unitViewModel);
                    unit.TopicCode = syllabus.TopicCode;
                    unit.DayNumber = unitViewModel.DayNumber;
                    // Tạo UnitCode tự động
                    int nextSequentialNumber = existingUnitCount + 1;
                    string formattedSequentialNumber = nextSequentialNumber.ToString("D8");
                    unit.UnitCode = "U" + formattedSequentialNumber;

                    foreach (var content in unit.TrainingContents)
                    {
                        // Tạo ContentId tự động
                        /*int nextContentNumber = existingContentCount + 1;
                        string formattedContentNumber = nextContentNumber.ToString("D8");
                        content.ContentId = "C" + formattedContentNumber;
*/
                        content.UnitCode = unit.UnitCode;
                        //existingContentCount++;
                    }
                    newUnits.Add(unit);
                    existingUnitCount++;
                }
                _trainingUnitRepository.AddRange(newUnits);
                _trainingUnitRepository.SaveChanges();
                // Tạo danh sách SyllabusObjective
                List<SyllabusObjective> newObjectives = new List<SyllabusObjective>();
                HashSet<string> addedCodes = new HashSet<string>();
                foreach (var unit in newUnits)
                {
                    foreach (var content in unit.TrainingContents)
                    {
                        // Kiểm tra xem code đã được thêm chưa
                        if (!addedCodes.Contains(content.Code))
                        {
                            var objective = new SyllabusObjective
                            {
                                TopicCode = syllabus.TopicCode,
                                ObjectiveCode = content.Code
                            };

                            newObjectives.Add(objective);
                            addedCodes.Add(content.Code); // Thêm code vào HashSet
                        }
                    }
                }
                // Lưu SyllabusObjective
                _syllabusObjectiveRepository.AddRange(newObjectives);
                _syllabusObjectiveRepository.SaveChanges();

                return "Add successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



    }
}