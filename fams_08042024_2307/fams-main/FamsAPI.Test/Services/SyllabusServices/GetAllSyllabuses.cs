using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.SyllabusServices
{
    public class GetAllSyllabuses
    {
        [Fact]
        public void GetAllSyllabuses_ReturnsListOfSyllabusViewModels()
        {
            // Arrange
            var syllabusRepository = A.Fake<SyllabusRepository>(); // Assuming _syllabusRepository is of type ISyllabusRepository
            var syllabus1 = new Syllabus
            {
                TopicCode = "T001",
                TopicName = "Sample Topic 1",
                CreatedBy = "Creator 1",
                CreatedDate = DateTime.Now,
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
                TopicCode = "T002",
                TopicName = "Sample Topic 2",
                CreatedBy = "Creator 2",
                CreatedDate = DateTime.Now,
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
                                Duration = 120 // Assuming this is the duration of this training content
                            },
                            new TrainingContent
                            {
                                Duration = 75 // Another example duration
                            }
                        }
                    }
                },
                SyllabusObjectives = new List<SyllabusObjective>
                    {
                        new SyllabusObjective
                        {
                            ObjectiveCode = "O002" // Assuming this is the code of the objective
                        }
                    }
            };

            A.CallTo(() => syllabusRepository.getAllSyllabus()).Returns(new List<Syllabus> { syllabus1, syllabus2 });

            var syllabusService = new FamsAPI.Services.SyllabusServices(syllabusRepository); // Assuming you have a class named SyllabusService which contains the GetAllSyllabuses method

            // Act
            var result = syllabusService.GetAllSyllabuses();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }


        [Fact]
        public void GetAllSyllabuses_WhenRepositoryReturnsNull_ReturnsNull()
        {
            // Arrange
            var syllabusRepository = A.Fake<SyllabusRepository>(); // Assuming _syllabusRepository is of type ISyllabusRepository
            A.CallTo(() => syllabusRepository.getAllSyllabus()).Returns(null); // Simulate repository returning null

            var syllabusService = new FamsAPI.Services.SyllabusServices(syllabusRepository); // Assuming you have a class named SyllabusService which contains the GetAllSyllabuses method

            // Act
            var result = syllabusService.GetAllSyllabuses();

            // Assert
            result.Should().BeNull(); // Ensure the result is null when repository returns null
        }
    }
}
