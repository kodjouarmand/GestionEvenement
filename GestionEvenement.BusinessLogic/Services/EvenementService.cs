using AutoMapper;
using GestionEvenement.BusinessLogic.Enums;
using GestionEvenement.BusinessLogic.Exceptions;
using GestionEvenement.DataAccess;
using GestionEvenement.Domain.Assemblers;
using GestionEvenement.Domain.Entities;
using GestionEvenement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionEvenement.BusinessLogic.Services.Contracts
{
    public class EvenementService : BaseService<EvenementDto, Evenement, Guid>, IEvenementService
    {
        public EvenementService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<EvenementDto> GetAll()
        {
            var evenements = _unitOfWork.Evenement.GetAll();
            var evenementDtos = MapEntitiesToDto(evenements);
            ToLocalTime(ref evenementDtos);
            return evenementDtos;
        }

        public override EvenementDto GetById(Guid evenementId)
        {
            var evenement = _unitOfWork.Evenement.GetById(evenementId);
            var evenementDto = MapEntityToDto(evenement);
            ToLocalTime(ref evenementDto);
            return evenementDto;
        }

        protected override StringBuilder Validate(EvenementDto evenementDto)
        {
            StringBuilder validationErrors = new();

            if (string.IsNullOrEmpty(evenementDto.Name))
            {
                validationErrors.Append(StaticHelper.NameRequiredErrorMessage);
            }
            else if (evenementDto.Name.Length > 32)
            {
                validationErrors.Append(StaticHelper.NameMaxLengthErrorMessage);
            }
            if (string.IsNullOrEmpty(evenementDto.Description))
            {
                validationErrors.Append(StaticHelper.DescriptionRequiredErrorMessage);
            }
            if (evenementDto.StartDateAndTime == default)
            {
                validationErrors.Append(StaticHelper.StartDateAndTimeRequiredErrorMessage);
            }
            if (evenementDto.EndDateAndTime == default)
            {
                validationErrors.Append(StaticHelper.EndDateAndTimeRequiredErrorMessage);
            }
            if (evenementDto.StartDateAndTime > evenementDto.EndDateAndTime)
            {
                validationErrors.Append(StaticHelper.StartEndDateComparisonErrorMessage);
            }

            return validationErrors;
        }

        public override Guid Add(EvenementDto evenementDto)
        {
            var evenement = BuildEntity(evenementDto);
            ToUniversalTime(ref evenement);
            _unitOfWork.Evenement.Add(evenement);
            return evenement.Id;
        }

        public override void Update(EvenementDto evenementDto)
        {
            var evenement = BuildEntity(evenementDto);
            ToUniversalTime(ref evenement);
            _unitOfWork.Evenement.Update(evenement);
        }

        protected override StringBuilder ValidateDelete(EvenementDto evenementDto = null)
        {
            StringBuilder validationErrors = new();
            return validationErrors;
        }

        public override void Delete(Guid evenementId)
        {
            var validationErrors = ValidateDelete();
            if (validationErrors.Length == 0)
            {
                _unitOfWork.Evenement.Delete(evenementId);
                return;
            }
            throw new ValidationException(validationErrors.ToString());
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }

        private static void ToUniversalTime(ref Evenement evenement)
        {
            evenement.StartDateAndTime = evenement.StartDateAndTime.ToUniversalTime();
            evenement.EndDateAndTime = evenement.EndDateAndTime.ToUniversalTime();
        }

        private static void ToLocalTime(ref EvenementDto evenementDto)
        {
            evenementDto.StartDateAndTime = evenementDto.StartDateAndTime.ToLocalTime();
            evenementDto.EndDateAndTime = evenementDto.EndDateAndTime.ToLocalTime();
        }

        private static void ToLocalTime(ref IEnumerable<EvenementDto> evenementDtos)
        {
            foreach (var evenementDto in evenementDtos)
            {
                evenementDto.StartDateAndTime = evenementDto.StartDateAndTime.ToLocalTime();
                evenementDto.EndDateAndTime = evenementDto.EndDateAndTime.ToLocalTime();
            }
        }
    }
}
