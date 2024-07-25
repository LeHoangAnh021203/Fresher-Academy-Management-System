using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.SyllabusServices
{
    public class DuplicateSyllabus
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

        public DuplicateSyllabus()
        {
            _syllabusRepository = A.Fake<SyllabusRepository>();
            _trainingContentRepository = A.Fake<TrainingContentRepository>();
            _assessmentService = A.Fake<IAssessment>();
            _trainingUnitService = A.Fake<ITrainingUnit>();
            _syllabusObjectiveService = A.Fake<ISyllabusObjective>();
            _context = A.Fake<FAMSDBContext>();
            _assessmentRepository = A.Fake<AssessmentRepository>();
            _mapper = A.Fake<IMapper>();
            _learningRepository = A.Fake<LearningObjectiveRepository>();
            _trainingUnitRepository = A.Fake<TrainingUnitRepository>();
            _syllabusObjectiveRepository = A.Fake<SyllabusObjectiveRepository>();
        }
        #endregion

        [Fact]
        public void GetAllSyllabuses_ReturnsListOfSyllabusViewModels()
        {
            // Arrange
            var syllabus1 = new Syllabus
            {
                TopicCode = "T01",
                TopicName = "Sample Topic 1",
                CreatedBy = "Creator 1",
                CreatedDate = DateTime.Now,
                TechnicalGroup = "Testing",
                PulishStatus = Syllabus.PulishStatuses.Published,
                TrainingUnits = new List<TrainingUnit>
                      {
                            new TrainingUnit
                            {
                                // Populate properties as needed
                                TrainingContents = new List<TrainingContent>
                                {
                                    new TrainingContent
                                    {
                                        Duration = 60 // Assuming this is the duration of this training content
                                    },
                                    new TrainingContent
                                    {
                                        Duration = 90 // Another example duration
                                    }
                                }
                            }
                          },
                AssessmentID = "AS001",
                SyllabusObjectives = new List<SyllabusObjective>
                    {
                        new SyllabusObjective
                        {
                            ObjectiveCode = "O001" // Assuming this is the code of the objective
                        }
                    }
            };

            var syllabus2 = new Syllabus
            {
                TopicCode = "T02",
                TopicName = "Sample Topic 1",
                CreatedBy = "Creator 1",
                CreatedDate = DateTime.Now,
                PulishStatus = Syllabus.PulishStatuses.Published,
                TechnicalGroup = "Testing",
                TrainingUnits = new List<TrainingUnit>
                {
                    new TrainingUnit
                    {
                        // Populate properties as needed
                        TrainingContents = new List<TrainingContent>
                        {
                            new TrainingContent
                            {
                                Duration = 60 // Assuming this is the duration of this training content
                            },
                            new TrainingContent
                            {
                                Duration = 90 // Another example duration
                            }
                        }
                    }
                },
                AssessmentID = "AS001",
                SyllabusObjectives = new List<SyllabusObjective>
                    {
                        new SyllabusObjective
                        {
                            ObjectiveCode = "O001" // Assuming this is the code of the objective
                        }
                    }
            };
            List <Syllabus> list = new List<Syllabus>();
            list.Add(syllabus1);
            

            A.CallTo(() => _syllabusRepository.SearchSyllabusByTopicCode("T01")).Returns(syllabus1);
            A.CallTo(() => _syllabusRepository.SearchSyllabusByTopicCode("T02")).Returns(syllabus2);
            A.CallTo(() => _syllabusRepository.SearchSyllabusTechnicalGroup("Testing")).Returns(list);
            

            var syllabusService = new FamsAPI.Services.SyllabusServices(_syllabusRepository, _trainingUnitRepository, _learningRepository, _trainingContentRepository, _trainingUnitService, _syllabusObjectiveService, _assessmentService, _assessmentRepository, _context, _syllabusObjectiveRepository, _mapper);

            // Act
            var result = syllabusService.DuplicateSyllabus("T01");
            

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<SyllabusViewModel>();
            result.TopicName.Should().BeEquivalentTo(syllabus1.TopicName);
            result.TopicCode.Should().BeEquivalentTo("T02");
        }

        [Fact]
        public void GetAllSyllabuses_ReturnsNull()
        {
            // Arrange
            A.CallTo(() => _syllabusRepository.SearchSyllabusByTopicCode("T01")).Returns(null);


            var syllabusService = new FamsAPI.Services.SyllabusServices(_syllabusRepository, _trainingUnitRepository, _learningRepository, _trainingContentRepository, _trainingUnitService, _syllabusObjectiveService, _assessmentService, _assessmentRepository, _context, _syllabusObjectiveRepository, _mapper);

            // Act
            var result = syllabusService.DuplicateSyllabus("T01");


            // Assert
            result.Should().BeNull();
        }
    }
}
