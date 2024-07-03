using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Mappings
{
    public class DisponibilitateProfile : Profile
    {
        public DisponibilitateProfile()
        {
            CreateMap<CerereModel, Disponibilitate>()
                .ForMember(a => a.IdDisponibilitate, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Zi, a => a.MapFrom(s => s.Zi))
                .ForMember(a => a.OraIncheiere, a => a.MapFrom(s => s.OraIncheiere))
                .ForMember(a => a.OraIncepere, a => a.MapFrom(s => s.OraIncepere))
                .ForMember(a => a.Status, a => a.MapFrom(s => s.Status));
        }
    }
}
        
