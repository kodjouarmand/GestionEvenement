using AutoFixture;
using GestionEvenement.Areas.Admin.Controllers;
using GestionEvenement.BusinessLogic.Services.Contracts;
using GestionEvenement.Domain.Assemblers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System;

namespace GestionEvenement.Mvc.Tests
{
    [TestClass]
    public class GestionEvenementMvcTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IEvenementService> _mockEvenementService = new ();
        private EvenementController _evenementController;

        [TestInitialize]
        public void TestInitialize()
        {
            _evenementController = new EvenementController(_mockEvenementService.Object);
        }

        [TestMethod]
        public void GetAll_ReturnJson()
        {
            //Arrange
            var evenementDtos = _fixture.CreateMany<EvenementDto>();

            _mockEvenementService.Setup(u => u.GetAll()).Returns(evenementDtos);

            //Act
            var result = _evenementController.GetAll();

            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        [TestMethod]
        public void GetAll_ReturnAllTheExpectedEvents()
        {
            //Arrange
            var evenementDtos = _fixture.CreateMany<EvenementDto>();
            var expected = _evenementController.Json(new { data = evenementDtos });

            _mockEvenementService.Setup(u => u.GetAll()).Returns(evenementDtos);

            //Act
            var actual = _evenementController.GetAll();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddOrEdit_ReturnViewWithModel()
        {
            //Act
            var result = (ViewResult)_evenementController.AddOrEdit();

            //Assert
            result.Model.Should().BeOfType<EvenementDto>();
        }

        [TestMethod]
        public void AddOrEdit_ReturnAddViewWithModel()
        {
            //Arrange
            var expected = new EvenementDto
            {
                StartDateAndTime = DateTime.Now.Date,
                EndDateAndTime = DateTime.Now.Date
            };

            //Act
            var result = (ViewResult)_evenementController.AddOrEdit();
            var actual = (EvenementDto)result.Model;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddOrEdit_CallServiceGetByIdMethod()
        {
            //Arrange
            var Id = Guid.NewGuid();
            var evenementDto = _fixture.Build<EvenementDto>()
                .With(u => u.Id, Id).Create();

            _mockEvenementService.Setup(u => u.GetById(Id)).Returns(evenementDto);

            //Act
            _evenementController.AddOrEdit(Id);

            //Assert
            _mockEvenementService.Verify(u => u.GetById(Id), Times.Once());
        }

        [TestMethod]
        public void AddOrEdit_ReturnNotFound()
        {
            //Arrange
            EvenementDto evenementDto = null;
            _mockEvenementService.Setup(u => u.GetById(It.IsNotNull<Guid>())).Returns(evenementDto);

            //Act
            var result = _evenementController.AddOrEdit(It.IsNotNull<Guid>());

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void AddOrEdit_ReturnEditViewWithModel()
        {
            //Arrange
            var Id = Guid.NewGuid();
            var expected = _fixture.Build<EvenementDto>()
                .With(u => u.Id, Id).Create();

            _mockEvenementService.Setup(u => u.GetById(Id)).Returns(expected);

            //Act
            var result = (ViewResult)_evenementController.AddOrEdit(Id);
            var actual = (EvenementDto)result.Model;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddOrEdit_CallServiceAddMethod()
        {
            //Arrange
            var evenementDto = _fixture.Build<EvenementDto>()
                .With(u => u.Id, Guid.Empty).Create();

            //Act
            _evenementController.AddOrEdit(evenementDto);

            //Assert
            _mockEvenementService.Verify(u => u.Add(evenementDto), Times.Once());
        }

        [TestMethod]
        public void AddOrEdit_CallServiceAddMethodWithNewId()
        {
            //Arrange
            var evenementDto = _fixture.Build<EvenementDto>()
                .With(u => u.Id, Guid.Empty).Create();

            //Act
            _evenementController.AddOrEdit(evenementDto);

            //Assert
            evenementDto.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void AddOrEdit_CallServiceUpdateMethod()
        {
            //Arrange
            var evenementDto = _fixture.Build<EvenementDto>()
                .With(u => u.Id, Guid.NewGuid).Create();

            //Act
            _evenementController.AddOrEdit(evenementDto);

            //Assert
            _mockEvenementService.Verify(u => u.Update(evenementDto), Times.Once());
        }

        [TestMethod]
        public void AddOrEdit_CallServiceSaveMethod()
        {
            //Arrange
            var evenementDto = _fixture.Create<EvenementDto>();

            //Act
            _evenementController.AddOrEdit(evenementDto);

            //Assert
            _mockEvenementService.Verify(u => u.Save(), Times.Once());
        }

        [TestMethod]
        public void AddOrEdit_ModelNotValidThrowException()
        {
            //Arrange
            _evenementController.ModelState.AddModelError("0", "Invalid");

            //Act
            Action action = () => _evenementController.AddOrEdit(It.IsAny<EvenementDto>());

            //Assert
            action.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Delete_CallServiceDeleteMethod()
        {
            //Arrange
            var evenementDto = _fixture.Create<EvenementDto>();

            _mockEvenementService.Setup(u => u.GetById(It.IsAny<Guid>())).Returns(evenementDto);

            //Act
            _evenementController.Delete(It.IsAny<Guid>());

            //Assert
            _mockEvenementService.Verify(u => u.Delete(It.IsAny<Guid>()), Times.Once());
        }

        [TestMethod]
        public void Delete_CallServiceSaveMethod()
        {
            //Arrange
            var evenementDto = _fixture.Create<EvenementDto>();

            _mockEvenementService.Setup(u => u.GetById(It.IsAny<Guid>())).Returns(evenementDto);

            //Act
            _evenementController.Delete(It.IsAny<Guid>());

            //Assert
            _mockEvenementService.Verify(u => u.Save(), Times.Once());
        }
    }
}
