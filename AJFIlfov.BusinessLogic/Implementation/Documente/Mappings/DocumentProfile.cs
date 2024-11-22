// BusinessLogic/Mapping/AnDuntProfile.cs
using AJFIlfov.BusinessLogic.Implementation.Documente.Models;
using AJFIlfov.Entities.Entities;
using AutoMapper;

namespace AJFIlfov.BusinessLogic.Mapping
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentModel>().ReverseMap();
        }
    }
}