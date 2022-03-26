using GestionEvenement.Domain.Assemblers;
using System.Collections.Generic;
using GestionEvenement.Service.Enums;

namespace GestionEvenement.Service.Queries.Contracts
{
    public interface IBaseQuery<TDto, TEntityKey> where TDto : BaseDto<TEntityKey>
    {
        TDto GetById(TEntityKey id);
        IEnumerable<TDto> GetAll();        
    }
}