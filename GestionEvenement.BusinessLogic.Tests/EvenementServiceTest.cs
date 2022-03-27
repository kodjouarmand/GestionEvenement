using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GestionEvenement.BusinessLogic.Exceptions;
using GestionEvenement.BusinessLogic.Services.Contracts;
using GestionEvenement.DataAccess;
using GestionEvenement.Domain.Assemblers;
using GestionEvenement.Domain.Entities;
using GestionEvenement.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace GestionEvenement.BusinessLogic.Tests
{
    [TestClass]
    public class EvenementServiceTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
        private IEvenementService _evenementService;

        [TestInitialize]
        public void TestInitialize()
        {
            _evenementService = new EvenementService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        private Evenement CreateEvenement()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(2);
            var evenement = _fixture.Build<Evenement>()
                .With(u => u.Name, "Christmas")
                .With(u => u.Description, "Christmas Party")
                .With(u => u.StartDateAndTime, startDate)
                .With(u => u.EndDateAndTime, endDate).Create();
            return evenement;
        }

        private EvenementDto CreateEvenementDto()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(2);
            var evenementDto = _fixture.Build<EvenementDto>()
                .With(u => u.Name, "Christmas")
                .With(u => u.Description, "Christmas Party")
                .With(u => u.StartDateAndTime, startDate)
                .With(u => u.EndDateAndTime, endDate).Create();
            return evenementDto;
        }

        private static Evenement MapEvenement(EvenementDto evenementDto)
        {
            return new Evenement()
            {
                Id = evenementDto.Id,
                Name = evenementDto.Name,
                Description = evenementDto.Description,
                StartDateAndTime = evenementDto.StartDateAndTime,
                EndDateAndTime = evenementDto.EndDateAndTime
            };
        }

        private static EvenementDto MapEvenementDto(Evenement evenement)
        {
            return new EvenementDto()
            {
                Id = evenement.Id,
                Name = evenement.Name,
                Description = evenement.Description,
                StartDateAndTime = evenement.StartDateAndTime,
                EndDateAndTime = evenement.EndDateAndTime,
            };
        }

        [TestMethod]
        public void GetAll_ReturnAllTheExpectedEvents()
        {
            //Arrange
            var evenements = _fixture.CreateMany<Evenement>();
            var expected = _fixture.CreateMany<EvenementDto>();

            _mockUnitOfWork.Setup(p => p.Evenement.GetAll()).Returns(evenements);
            _mockMapper.Setup(p => p.Map<IEnumerable<EvenementDto>>(evenements)).Returns(expected);

            //Act
            var actual = _evenementService.GetAll();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetById_ReturnTheExpectedEvent()
        {
            //Arrange
            var Id = Guid.NewGuid();
            var evenement = CreateEvenement();
            var expected = CreateEvenementDto();

            _mockUnitOfWork.Setup(p => p.Evenement.GetById(Id)).Returns(evenement);
            _mockMapper.Setup(p => p.Map<EvenementDto>(evenement)).Returns(expected);

            //Act
            var actual = _evenementService.GetById(Id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetById_ReturnTheExpectedEventWithDatesInLocalTime()
        {
            //Arrange
            var Id = Guid.NewGuid();
            var evenement = CreateEvenement();
            evenement.StartDateAndTime = evenement.StartDateAndTime.ToUniversalTime();
            evenement.EndDateAndTime = evenement.EndDateAndTime.ToUniversalTime();

            var evenementDto = MapEvenementDto(evenement);

            var expectedStartDateAndTime = evenementDto.StartDateAndTime.ToLocalTime();
            var expectedEndDateAndTime = evenementDto.EndDateAndTime.ToLocalTime();

            _mockUnitOfWork.Setup(p => p.Evenement.GetById(Id)).Returns(evenement);
            _mockMapper.Setup(p => p.Map<EvenementDto>(evenement)).Returns(evenementDto);

            //Act
            var actual = _evenementService.GetById(Id);

            //Assert
            actual.StartDateAndTime.Should().Be(expectedStartDateAndTime);
            actual.EndDateAndTime.Should().Be(expectedEndDateAndTime);
        }

        [TestMethod]
        public void Add_ValidateEventAndNotThrowException()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();

            //Act
            Action action = () => _evenementService.Add(evenementDto);

            //Arrange
            action.Should().NotThrow<ValidationException>();
        }

        [TestMethod]
        public void Add_ValidateNameLengthAndThrowException()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            evenementDto.Name += _fixture.Build<string>();

            //Act
            Action action = () => _evenementService.Add(evenementDto);

            //Assert
            action.Should().Throw<ValidationException>().WithMessage(StaticHelper.NameMaxLengthErrorMessage);
        }

        [TestMethod]
        public void Add_ValidateStartAndEndDateAndThrowException()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            evenementDto.EndDateAndTime = evenementDto.StartDateAndTime.AddHours(-1);

            //Act
            Action action = () => _evenementService.Add(evenementDto);


            //Assert
            action.Should().Throw<ValidationException>().WithMessage(StaticHelper.StartEndDateComparisonErrorMessage);
        }

        [TestMethod]
        public void Add_CallUnitOfWorkAddMethod()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            var evenement = MapEvenement(evenementDto);
            var expected = evenement.Id;

            _mockMapper.Setup(p => p.Map<Evenement>(evenementDto)).Returns(evenement);
            _mockUnitOfWork.Setup(p => p.Evenement.Add(It.IsAny<Evenement>()));

            //Act
            var actual = _evenementService.Add(evenementDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.Evenement.Add(evenement), Times.Once());
        }

        [TestMethod]
        public void Add_ReturnTheExpectedId()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            var evenement = MapEvenement(evenementDto);
            var expected = evenement.Id;

            _mockMapper.Setup(p => p.Map<Evenement>(evenementDto)).Returns(evenement);
            _mockUnitOfWork.Setup(p => p.Evenement.Add(It.IsAny<Evenement>()));

            //Act
            var actual = _evenementService.Add(evenementDto);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Add_SaveStartAndEndDateInUniversalTime()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            var evenement = MapEvenement(evenementDto);

            var expectedStartDateAndTime = evenement.StartDateAndTime.ToUniversalTime();
            var expectedEndDateAndTime = evenement.EndDateAndTime.ToUniversalTime();

            _mockMapper.Setup(p => p.Map<Evenement>(evenementDto)).Returns(evenement);
            _mockUnitOfWork.Setup(p => p.Evenement.Add(It.IsAny<Evenement>()));

            //Act
            var actual = _evenementService.Add(evenementDto);

            //Assert
            evenement.StartDateAndTime.Should().Be(expectedStartDateAndTime);
            evenement.EndDateAndTime.Should().Be(expectedEndDateAndTime);
        }

        [TestMethod]
        public void Update_CallUnitOfWorkUpdateMethod()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();
            var evenement = MapEvenement(evenementDto);

            _mockMapper.Setup(p => p.Map<Evenement>(evenementDto)).Returns(evenement);
            _mockUnitOfWork.Setup(p => p.Evenement.Update(It.IsAny<Evenement>()));

            //Act
            _evenementService.Update(evenementDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.Evenement.Update(evenement), Times.Once());
        }

        [TestMethod]
        public void Delete_CallUnitOfWorkDeleteMethod()
        {
            //Arrange
            var evenementDto = CreateEvenementDto();

            _mockUnitOfWork.Setup(p => p.Evenement.Delete(evenementDto.Id));

            //Act
            _evenementService.Delete(evenementDto.Id);

            //Assert
            _mockUnitOfWork.Verify(u => u.Evenement.Delete(evenementDto.Id), Times.Once());
        }
    }
}
