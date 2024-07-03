using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.GrupeService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeService.Mappings
{
    public class GrupeProfile : Profile
    {
        public GrupeProfile()
        {
            CreateMap<GrupaModel, Grupe>()
                .ForMember(g => g.IdGrupa, g => g.MapFrom(s => s.IdGrupa == Guid.Empty ? Guid.NewGuid() : s.IdGrupa))
                .ReverseMap();
        }
    }
}
