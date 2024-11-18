using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.MeciLiveService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.MeciLiveService
{
    public class MeciLiveService : BaseService
    {
        public MeciLiveService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<MeciLiveModel> GetAllMeciuriLive()
        {
            var meciuri = UnitOfWork.MeciuriLive.Get()
                .Select(m => new MeciLiveModel
                {
                    IdMeciLive = m.IdMeciLive,
                    EchipaGazda = m.EchipaGazda,
                    EchipaOaspete = m.EchipaOaspete,
                    ScorGazda = m.ScorGazda,
                    ScorOaspete = m.ScorOaspete,
                    Status = m.Status,
                    DataOra = m.DataOra,
                    Minut = m.Minut,
                    StartTime = m.StartTime,
                    IsSecondHalf = m.IsSecondHalf
                }).ToList();

            return meciuri;
        }

        public MeciLive GetMeciLiveEntityById(int idMeciLive)
        {
            return UnitOfWork.MeciuriLive.Get().FirstOrDefault(m => m.IdMeciLive == idMeciLive);
        }

        public MeciLiveModel GetMeciLiveById(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci == null)
                return null;

            return new MeciLiveModel
            {
                IdMeciLive = meci.IdMeciLive,
                EchipaGazda = meci.EchipaGazda,
                EchipaOaspete = meci.EchipaOaspete,
                ScorGazda = meci.ScorGazda,
                ScorOaspete = meci.ScorOaspete,
                Status = meci.Status,
                DataOra = meci.DataOra,
                Minut = meci.Minut,
                StartTime = meci.StartTime,
                IsSecondHalf = meci.IsSecondHalf
            };
        }

        public bool CreateMeciLive(MeciLiveModel model)
        {
            var meci = new MeciLive
            {
                EchipaGazda = model.EchipaGazda,
                EchipaOaspete = model.EchipaOaspete,
                ScorGazda = model.ScorGazda,
                ScorOaspete = model.ScorOaspete,
                Status = model.Status,
                DataOra = model.DataOra,
                Minut = model.Minut,
                StartTime = model.StartTime,
                IsSecondHalf = model.IsSecondHalf
            };

            UnitOfWork.MeciuriLive.Insert(meci);
            UnitOfWork.SaveChanges();
            return true;
        }

        public void UpdateMinut(MeciLive meci)
        {
            if (meci.Status == "InDesfasurare" && meci.StartTime.HasValue)
            {
                var elapsedTime = DateTime.Now - meci.StartTime.Value;
                int minute = (int)elapsedTime.TotalMinutes;

                if (meci.IsSecondHalf)
                {
                    meci.Minut = 45 + minute;
                    if (meci.Minut >= 90)
                    {
                        meci.Minut = 90;
                        meci.Status = "Finalizat";
                    }
                }
                else
                {
                    meci.Minut = minute;
                    if (meci.Minut >= 45)
                    {
                        meci.Minut = 45;
                        meci.Status = "Pauza";
                    }
                }
                UnitOfWork.MeciuriLive.Update(meci);
                UnitOfWork.SaveChanges();
            }
        }

        public void StartMeci(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci != null)
            {
                if (meci.Status == "Programat" || meci.Status == "Pauza")
                {
                    meci.Status = "InDesfasurare";
                    meci.StartTime = DateTime.Now;
                    meci.IsSecondHalf = meci.Status == "Pauza";
                    UnitOfWork.MeciuriLive.Update(meci);
                    UnitOfWork.SaveChanges();
                }
            }
        }

        public void PauzaMeci(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci != null && meci.Status == "InDesfasurare")
            {
                meci.Status = "Pauza";
                meci.Minut = 45;
                UnitOfWork.MeciuriLive.Update(meci);
                UnitOfWork.SaveChanges();
            }
        }

        public void FinalizeazaMeci(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci != null)
            {
                meci.Status = "Finalizat";
                meci.Minut = 90;
                UnitOfWork.MeciuriLive.Update(meci);
                UnitOfWork.SaveChanges();
            }
        }

        public void GolGazde(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci != null && meci.Status == "InDesfasurare")
            {
                meci.ScorGazda += 1;
                UnitOfWork.MeciuriLive.Update(meci);
                UnitOfWork.SaveChanges();
            }
        }

        public void GolOaspeti(int idMeciLive)
        {
            var meci = GetMeciLiveEntityById(idMeciLive);
            if (meci != null && meci.Status == "InDesfasurare")
            {
                meci.ScorOaspete += 1;
                UnitOfWork.MeciuriLive.Update(meci);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
