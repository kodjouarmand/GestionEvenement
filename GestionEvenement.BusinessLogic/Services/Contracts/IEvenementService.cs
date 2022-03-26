using GestionEvenement.Domain.Assemblers;
using System;

namespace GestionEvenement.BusinessLogic.Services.Contracts
{
    public interface IEvenementService : IBaseService<EvenementDto, Guid>
    {

    }
}