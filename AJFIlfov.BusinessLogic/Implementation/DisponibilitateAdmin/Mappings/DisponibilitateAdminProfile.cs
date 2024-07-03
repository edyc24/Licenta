using AutoMapper;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdmiService.Mappings
{
    public class MeciuriProfile : Profile
    {
        public MeciuriProfile()
        {
            CreateMap<CerereDisponibilitateModel, DisponibilitateAdmin>()
                .ForMember(a => a.IdDisponibilitateAdmin, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.Zi, a => a.MapFrom(s => s.Zi))
                .ForMember(a => a.CreatedOn, a => a.MapFrom(s => DateTime.Now));
        }
    }
}
        
