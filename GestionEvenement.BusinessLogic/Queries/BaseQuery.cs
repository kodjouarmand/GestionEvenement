using AutoMapper;
using GestionEvenement.Domain.Entities;
using GestionEvenement.Domain.Assemblers;
using System.Collections.Generic;
using GestionEvenement.Service.Queries.Contracts;
using GestionEvenement.DataAccess;

namespace GestionEvenement.Service.Queries
{
    public abstract class BaseQuery<TDto, TEntity, TEntityKey> : IBaseQuery<TDto, TEntityKey> where TDto : BaseDto<TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }

        public abstract TDto GetById(TEntityKey id);
        public abstract IEnumerable<TDto> GetAll();

        protected TDto MapEntityToDto(TEntity entity) => _mapper.Map<TDto>(entity);
        
        protected IEnumerable<TDto> MapEntitiesToDto(IEnumerable<TEntity> entities) => _mapper.Map<IEnumerable<TDto>>(entities);
    }
}
