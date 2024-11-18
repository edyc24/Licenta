// BusinessLogic.Implementation.AnuntService/AnuntService.cs
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.DataAccess;
using AJFIlfov.DataAccess.data_acces;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.Anunturi
{
    public class AnuntService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnuntService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<AnuntModel> GetAllAnunturi()
        {
            var anunturi = _unitOfWork.Anunturi.Get();
            return _mapper.Map<List<AnuntModel>>(anunturi);
        }

        public List<AnuntModel> GetAnunturiComisii()
        {
            return _mapper.Map<List<AnuntModel>>(_unitOfWork.Anunturi.Get()
                .Where(anunt => anunt.TipAnunt == "Comisii")
                .OrderByDescending(anunt => anunt.DataPublicarii)
                .ToList());
        }

        public bool CreateAnunt(AnuntModel anuntModel)
        {
            var anunt = _mapper.Map<Anunt>(anuntModel);
            anunt.DataPublicarii = DateTime.Now;
            _unitOfWork.Anunturi.Insert(anunt);
            _unitOfWork.SaveChanges();
            return true;
        }

        public AnuntModel GetAnuntById(int id)
        {
            var anunt = _unitOfWork.Anunturi.Get().Where(a => a.Id == id);
            return _mapper.Map<AnuntModel>(anunt);
        }
    }
}