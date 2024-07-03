using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.EchipeService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.EchipeService.Mappings
{
    public class EchipeProfile : Profile
    {
        public EchipeProfile()
        {
            CreateMap<EchipaModel, Echipe>()
                .ForMember(e => e.IdEchipa, e => e.MapFrom(s => s.IdEchipa == Guid.Empty ? Guid.NewGuid() : s.IdEchipa))
                .ReverseMap();
        }
    }
}
