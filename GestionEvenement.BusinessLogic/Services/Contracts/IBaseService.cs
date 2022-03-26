using GestionEvenement.Domain.Assemblers;
using System.Collections.Generic;
using GestionEvenement.BusinessLogic.Enums;

namespace GestionEvenement.BusinessLogic.Services.Contracts
{
    public interface IBaseService<TDto, TEntityKey> where TDto : BaseDto<TEntityKey>
    {
        TDto GetById(TEntityKey id);
        IEnumerable<TDto> GetAll();
        TEntityKey Add(TDto dto);
        void Update(TDto dto);
        void Delete(TEntityKey dtoId);
        void Save();
    }
}