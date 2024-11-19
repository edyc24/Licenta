// BusinessLogic/Mapping/AnuntProfile.cs
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.BusinessLogic.Implementation.Audituri.Models;
using AJFIlfov.Entities.Entities;
using AutoMapper;

namespace AJFIlfov.BusinessLogic.Mapping
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<Audit, AuditModel>().ReverseMap();
        }
    }
}