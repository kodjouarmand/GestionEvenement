using AutoMapper;
using GestionEvenement.Domain.Entities;
using GestionEvenement.Domain.Assemblers;
using System.Text;
using GestionEvenement.BusinessLogic.Exceptions;
using GestionEvenement.BusinessLogic.Enums;
using GestionEvenement.DataAccess.Repositories.Contracts;
using GestionEvenement.BusinessLogic.Services.Contracts;
using GestionEvenement.DataAccess;
using System.Collections.Generic;

namespace GestionEvenement.BusinessLogic.Services
{
    public abstract class BaseService<TDto, TEntity, TEntityKey> : IBaseService<TDto, TEntityKey> where TDto : BaseDto<TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public abstract TDto GetById(TEntityKey id);
        public abstract IEnumerable<TDto> GetAll();

        public abstract TEntityKey Add(TDto dto);
        public abstract void Update(TDto dto);
        public abstract void Delete(TEntityKey dtoId);
        public abstract void Save();
        protected abstract StringBuilder Validate(TDto dto);
        protected abstract StringBuilder ValidateDelete(TDto dto);
        protected TDto MapEntityToDto(TEntity entity) => _mapper.Map<TDto>(entity);
        protected IEnumerable<TDto> MapEntitiesToDto(IEnumerable<TEntity> entities) => _mapper.Map<IEnumerable<TDto>>(entities);
        protected TEntity MapDtoToEntity(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            return entity;
        }

        protected TEntity BuildEntity(TDto dto)
        {
            StringBuilder validationErrors = Validate(dto);

            if (validationErrors!=null && validationErrors.Length != 0)
            {
                throw new ValidationException(validationErrors.ToString());
            }

            TEntity entity = MapDtoToEntity(dto);
            return entity;
        }
    }
}
