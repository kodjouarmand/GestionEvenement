using GestionEvenement.Domain.Assemblers;
using GestionEvenement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionEvenement.Service.Queries.Contracts
{
    public interface IEvenementQuery : IBaseQuery<EvenementDto, Guid>
    {

    }
}