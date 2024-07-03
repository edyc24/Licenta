using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.LocalitatiService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.LocalitatiService.Mappings
{
    public class LocalitatiProfile : Profile
    {
        public LocalitatiProfile()
        {
            CreateMap<LocalitateModel, Localitati>()
                .ForMember(l => l.IdLocalitate, l => l.MapFrom(s => s.IdLocalitate == Guid.Empty ? Guid.NewGuid() : s.IdLocalitate))
                .ReverseMap();
        }
    }
}
