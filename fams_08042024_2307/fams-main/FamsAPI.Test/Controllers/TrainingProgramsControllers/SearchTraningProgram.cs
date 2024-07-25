using DataLayer.Entities;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.TrainingProgramsControllers
{
    public class SearchTraningProgram
    {
        private TestsFixture tf = new TestsFixture();   
        [Fact]
        public void SearchTraningProgram_WithNotExistKeyword_ReturnNotFound()
        {
            var result = tf._trainingProgramController.SearchTrainingProgram("hi",null,null,null,null);
            result.Should().BeOfType<NotFoundObjectResult>();
            tf.Dispose();
        }
        [Fact]
        public void SearchTraningProgram_WithExistKeyword_ReturnTraingProgram()
        {
            var result = tf._trainingProgramController.SearchTrainingProgram("TP001", null, null, null, null);
            result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<TrainingProgramViewModel>>();

            var listTrainingProgram = (List<TrainingProgramViewModel>)okResult.Value;
            listTrainingProgram.Should().NotBeNull();
            listTrainingProgram.Should().HaveCount(1);
            listTrainingProgram.Should().Contain(c => c.Name.Equals("Training Program 1"));

            tf.Dispose();
        }

        [Fact]
        public void SearchTraningProgram_WithFilterCreateBy_ReturnListTrainingProgram()
        {
            var result = tf._trainingProgramController.SearchTrainingProgram(null, "SuperAdmin", null, null, null);
            result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<TrainingProgramViewModel>>();

            var listTrainingProgram = (List<TrainingProgramViewModel>)okResult.Value;
            listTrainingProgram.Should().NotBeNull();
            listTrainingProgram.Should().HaveCount(2);
            listTrainingProgram.Should().Contain(c => c.Name.Equals("Training Program 1"));

            tf.Dispose();
        }

        [Fact]
        public void SearchTraningProgram_WithFilterCreateDate_ReturnListTrainingProgram()
        {
            var result = tf._trainingProgramController.SearchTrainingProgram(null, null, "2023-12-21 00:00:00.0000000", null, null);
            result.Should().BeOfType<OkObjectResult>();
            
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<TrainingProgramViewModel>>();

            var listTrainingProgram = (List<TrainingProgramViewModel>)okResult.Value;
            listTrainingProgram.Should().NotBeNull();
            listTrainingProgram.Should().HaveCount(2);
            listTrainingProgram.Should().Contain(c => c.Name.Equals("Training Program 2"));
            listTrainingProgram.Should().Contain(c => c.Duration == 10);

            tf.Dispose();
        }
    }
}
