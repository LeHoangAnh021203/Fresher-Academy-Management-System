using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.ClassControllers
{
    public class GetClassById
    {
        [Fact]
        public void GetClassById_Return_OkObjectResult_With_ClassViewModel()
        {
            //Arrange
            var classService = A.Fake<IClass>();
            A.CallTo(() => classService.GetAllClasses()).Returns(new List<ClassViewModel>
                {
                    new ClassViewModel
                    {
                        ClassID= "C00000001",
                        ClassName= "Java Introduction",
                        ClassCode= "J001",
                        Duration= 120,
                        Status= 0,
                        LocationId= "L001",
                        FsuId= "F001",
                        CreatedBy= "Super Admin",
                        CreatedDate= DateTime.Now,
                        ModifiedBy= "Super Admin",
                        ModifiedDate= DateTime.Now,
                        TrainingProgramCode= "T001"
                    },
                    new ClassViewModel
                    {
                        ClassID= "C00000002",
                        ClassName= "Python Introduction",
                        ClassCode= "P001",
                        Duration= 180,
                        Status= 0,
                        LocationId= "L001",
                        FsuId= "F001",
                        CreatedBy= "Super Admin",
                        CreatedDate= DateTime.Now,
                        ModifiedBy= "Super Admin",
                        ModifiedDate= DateTime.Now,
                        TrainingProgramCode= "T002"
                    },
                });
            var controller= new ClassController(classService);

            //Act
            var result= controller.GetAllClasses();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var OkResult = (OkObjectResult)result;
            OkResult.Value.Should().BeAssignableTo<List<ClassViewModel>>();

            var classes = (List<ClassViewModel>)OkResult.Value;

            classes.Should().HaveCount(2); 
            classes.Should().Contain(c => c.ClassID == "C00000001");
            classes.Should().Contain(c => c.ClassID == "C00000002");
        }

        [Fact]
        public void GetClassById_Return_Empty_With_ClassList()
        {
            //Arrange
            var classService = A.Fake<IClass>();
            A.CallTo(() => classService.GetAllClasses()).Returns(new List<ClassViewModel>
            {

            });

            var controller = new ClassController(classService);

            //Act
            var result = controller.GetClassById("C00000003");

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
