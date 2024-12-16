// BusinessLogic/Mapping/AnuntProfile.cs
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using QnA.Models;

namespace AJFIlfov.BusinessLogic.Mapping
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionModel>().ReverseMap();
        }
    }
}