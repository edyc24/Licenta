// BusinessLogic/Mapping/AnuntProfile.cs
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.Entities.Entities;
using AutoMapper;

namespace AJFIlfov.BusinessLogic.Mapping
{
    public class AnuntProfile : Profile
    {
        public AnuntProfile()
        {
            CreateMap<Anunt, AnuntModel>().ReverseMap();
        }
    }
}