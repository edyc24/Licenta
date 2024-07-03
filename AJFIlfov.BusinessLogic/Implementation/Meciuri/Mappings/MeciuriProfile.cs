using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.MeciuriService.Mappings
{
    public class MeciuriProfile : Profile
    {
        public MeciuriProfile()
        {
            CreateMap<Meciuri, MeciArbitruModel>()
                .ForMember(dest => dest.Rezultat, opt => opt.MapFrom(src => src.Rezultat))
                .ForMember(dest => dest.Observatii, opt => opt.MapFrom(src => src.Observatii))
                .ForMember(dest => dest.Raport, opt => opt.Ignore());

            CreateMap<Meciuri, MeciAdminModel>()
                .ForMember(dest => dest.IdMeci, opt => opt.MapFrom(src => src.IdMeci))
                .ForMember(dest => dest.IdGrupa, opt => opt.MapFrom(src => src.IdEchipaGazdaNavigation != null ? src.IdEchipaGazdaNavigation.IdGrupa : (Guid?)null))
                .ForMember(dest => dest.IdEchipaGazda, opt => opt.MapFrom(src => src.IdEchipaGazda))
                .ForMember(dest => dest.IdEchipaOaspete, opt => opt.MapFrom(src => src.IdEchipaOaspete))
                .ForMember(dest => dest.DataJoc, opt => opt.MapFrom(src => src.DataJoc))
                .ForMember(dest => dest.IdArbitru, opt => opt.MapFrom(src => src.IdArbitru))
                .ForMember(dest => dest.IdArbitruAsistent1, opt => opt.MapFrom(src => src.IdArbitruAsistent1))
                .ForMember(dest => dest.IdArbitruAsistent2, opt => opt.MapFrom(src => src.IdArbitruAsistent2))
                .ForMember(dest => dest.IdArbitruRezerva, opt => opt.MapFrom(src => src.IdArbitruRezerva))
                .ForMember(dest => dest.IdObservator, opt => opt.MapFrom(src => src.IdObservator))
                .ForMember(dest => dest.IdStadionLocalitate, opt => opt.MapFrom(src => src.IdStadionLocalitate));

            // Reverse mappings for saving changes from model to entity
            CreateMap<MeciArbitruModel, Meciuri>()
                .ForMember(dest => dest.Rezultat, opt => opt.MapFrom(src => src.Rezultat))
                .ForMember(dest => dest.Observatii, opt => opt.MapFrom(src => src.Observatii))
                .ForMember(dest => dest.Raport, opt => opt.Ignore()); // Handle file upload separately

            CreateMap<MeciAdminModel, Meciuri>()
                .ForMember(dest => dest.IdMeci, opt => opt.MapFrom(src => src.IdMeci))
                .ForMember(dest => dest.IdEchipaGazda, opt => opt.MapFrom(src => src.IdEchipaGazda))
                .ForMember(dest => dest.IdEchipaOaspete, opt => opt.MapFrom(src => src.IdEchipaOaspete))
                .ForMember(dest => dest.DataJoc, opt => opt.MapFrom(src => src.DataJoc))
                .ForMember(dest => dest.IdArbitru, opt => opt.MapFrom(src => src.IdArbitru))
                .ForMember(dest => dest.IdArbitruAsistent1, opt => opt.MapFrom(src => src.IdArbitruAsistent1))
                .ForMember(dest => dest.IdArbitruAsistent2, opt => opt.MapFrom(src => src.IdArbitruAsistent2))
                .ForMember(dest => dest.IdArbitruRezerva, opt => opt.MapFrom(src => src.IdArbitruRezerva))
                .ForMember(dest => dest.IdObservator, opt => opt.MapFrom(src => src.IdObservator))
                .ForMember(dest => dest.IdStadionLocalitate, opt => opt.MapFrom(src => src.IdStadionLocalitate))
                .ForMember(dest => dest.IdDeleted, opt => opt.Ignore()); // Ensure we don't change deletion status inadvertently
        }
    }
}
