using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.StadioaneService.Mappings
{
    public class StadioaneProfile : Profile
    {
        public StadioaneProfile()
        {
            CreateMap<StadionModel, Stadioane>()
                .ForMember(s => s.IdStadion, s => s.MapFrom(src => src.IdStadion == Guid.Empty ? Guid.NewGuid() : src.IdStadion))
                .ReverseMap();
        }
    }
}
