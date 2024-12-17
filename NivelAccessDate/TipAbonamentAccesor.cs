using LibrarieModele;

using NivelAccessDate.Interfaces;

using NLog;

using Repository_CodeFirst;

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace NivelAccessDate
{
    public class TipAbonamentAccesor : ITipAbonamentAccessor
    {
        private ProiectContext db;
        protected Logger logger=LogManager.GetCurrentClassLogger();

        public TipAbonamentAccesor(IProiectDbContext db)
        {
            this.db=(ProiectContext)db;
        }

        public List<TipAbonament> GetTipAbonaments()
        {
            List<TipAbonament> tipAbonaments;

            try
            {
                tipAbonaments = db.TipAbonaments.AsNoTracking().ToList();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Error on getting tipAbonaments from db");

                return null;
            }

            return tipAbonaments;
        }

        public TipAbonament GetTipAbonamentById(int id)
        {
            TipAbonament tipAbonament = db.TipAbonaments.AsNoTracking().FirstOrDefault(localCompany => localCompany.AbonamentId == id);

            return tipAbonament;
        }

        public bool AddTipAbonament(TipAbonament tipAbonament)
        {
            db.TipAbonaments.Add(tipAbonament);
            int result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateTipAbonament(TipAbonament tipAbonament)
        {

            var aex = db.TipAbonaments.Find(tipAbonament.AbonamentId);
            if (aex == null)
            {
                db.TipAbonaments.Add(tipAbonament);
            }
            else
            {
                db.Entry(aex).State = EntityState.Detached;
                db.TipAbonaments.Add(tipAbonament);

                db.Entry(tipAbonament).State = EntityState.Modified;
            }


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipAbonamentExists(tipAbonament.AbonamentId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool DeleteTipAbonament(int id)
        {
            TipAbonament tipAbonament = db.TipAbonaments.Find(id);

            db.TipAbonaments.Remove(tipAbonament);
            db.SaveChanges();

            return true;
        }


        private bool TipAbonamentExists(int id)
        {
            return db.TipAbonaments.Count(e => e.AbonamentId == id) > 0;
        }


        // method injection
        public void SetDependency(IProiectDbContext db)
        {
            this.db = (ProiectContext)db;
        }

        //property injection
        public IProiectDbContext DbContextProperty
        {
            set
            {
                this.db = (ProiectContext)value;
            }
            get
            {
                if (db == null)
                {
                    throw new Exception("DbContext is not initialized");
                }
                else
                {
                    return db;
                }
            }
        }



        //private readonly ProiectContext _pawentities;
        //public TipAbonamentAccesor()
        //{
        //    _pawentities = new ProiectContext();
        //}
        //public List<TipAbonament> GetAllAbonament()
        //{
        //    return _pawentities.TipAbonaments.ToList();
        //}
        //public TipAbonament GetAbonametById(decimal abonamentId)
        //{
        //    return _pawentities.TipAbonaments.FirstOrDefault(c => c.AbonamentId == abonamentId);
        //}
        //public void AddAbonament(TipAbonament abonament)
        //{
        //    _pawentities.TipAbonaments.Add(abonament);
        //    _pawentities.SaveChanges();
        //}
        //public void UpdateAbonament(TipAbonament abonament)
        //{
        //    var existingClient = _pawentities.TipAbonaments.Find(abonament.AbonamentId);
        //    if (existingClient != null)
        //    {
        //        existingClient.Tip = abonament.Tip;
        //        existingClient.PretLuna = abonament.PretLuna;
        //        existingClient.PretAn = abonament.PretAn;
        //        existingClient.NrUtilizari = abonament.NrUtilizari;
        //        _pawentities.SaveChanges();
        //    }
        //}
        //public void DeleteAbonament(decimal abonamentId)
        //{
        //    var abonament = _pawentities.TipAbonaments.Find(abonamentId);
        //    if (abonament != null)
        //    {
        //        _pawentities.TipAbonaments.Remove(abonament);
        //        _pawentities.SaveChanges();
        //    }
        //}

    }
}
