using AJFIlfov.Entities.Entities;
using AutoMapper;
using QnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.QnA.Mappings
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerModel>().ReverseMap();
        }
    }
}
