using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService.Mappings
{
    public class StadionLocalitateProfile : Profile
    {
        public StadionLocalitateProfile()
        {
            CreateMap<StadionLocalitateModel, StadionLocalitate>()
                .ForMember(sl => sl.IdStadionLocalitate, sl => sl.MapFrom(s => s.IdStadionLocalitate == Guid.Empty ? Guid.NewGuid() : s.IdStadionLocalitate))
                .ReverseMap();
        }
    }
}
