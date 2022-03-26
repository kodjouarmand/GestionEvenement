using AutoMapper;
using GestionEvenement.Domain.Assemblers;
using GestionEvenement.Domain.Entities;

namespace GestionEvenement.Mvc.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Evenement, EvenementDto>();
            CreateMap<EvenementDto, Evenement>();
        }
    }
}
