using AutoMapper;
using GestionEvenement.Domain.Entities;
using System.Collections.Generic;
using GestionEvenement.Domain.Assemblers;
using GestionEvenement.Service.Queries.Contracts;
using System.Linq;
using System;
using GestionEvenement.DataAccess;

namespace GestionEvenement.Service.Queries
{
    public class EvenementQuery : BaseQuery<EvenementDto, Evenement, Guid>, IEvenementQuery
    {
        public EvenementQuery(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override IEnumerable<EvenementDto> GetAll()
        {
            var evenements = _unitOfWork.Evenement.GetAll();           
            return MapEntitiesToDto(evenements);
        }

        public override EvenementDto GetById(Guid evenementId)
        {
            var Evenement = _unitOfWork.Evenement.GetAll(c =>  c.Id == evenementId).FirstOrDefault();
            return MapEntityToDto(Evenement);
        }
    }
}
