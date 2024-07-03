using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Mappings
{
    public class GrupeEchipaProfile : Profile
    {
        public GrupeEchipaProfile()
        {
            CreateMap<GrupaEchipaModel, GrupeEchipa>()
                .ForMember(ge => ge.IdGrupaEchipa, ge => ge.MapFrom(s => s.IdGrupaEchipa == Guid.Empty ? Guid.NewGuid() : s.IdGrupaEchipa))
                .ReverseMap();
        }
    }
}
