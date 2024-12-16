using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using QnA.Models;

namespace AJFIlfov.BusinessLogic.Implementation.QnA.Mappings
{
    public class SuggestionProfile : Profile
    {
        public SuggestionProfile()
        {
            CreateMap<Suggestion, SuggestionModel>().ReverseMap();
        }
    }
}
