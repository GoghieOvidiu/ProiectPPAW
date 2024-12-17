using LibrarieModele;

using Repository_CodeFirst;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NivelAccessDate.Interfaces;
using NLog;
using System.Data.Entity.Infrastructure;

namespace NivelAccessDate
{
    public class AbonatiiAccesor : IAbonatiiAccessor
    {
        private ClientAccesor clientAccesor;
        private TipAbonamentAccesor tipAbonamentAccesor;
        private ProiectContext db;
        protected Logger logger = LogManager.GetCurrentClassLogger();

        public AbonatiiAccesor(IProiectDbContext db)
        {
            this.db = (ProiectContext)db;
        }

        public List<Abonatii> GetAbonatiis()
        {
            List<Abonatii> abonatiis;

            try
            {
                abonatiis = db.Abonatiis.AsNoTracking().ToList();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Error on getting abonatiis from db");

                return null;
            }

            return abonatiis;
        }

        public Abonatii GetAbonatiiById(int id)
        {
            Abonatii abonatii = db.Abonatiis.AsNoTracking().FirstOrDefault(localCompany => localCompany.AbonatiiId == id);

            return abonatii;
        }

        public bool AddAbonatii(Abonatii abonatii)
        {
            //object client=clientAccesor.GetClientById(abonatii.ClientId);
            //object tipAbonament = tipAbonamentAccesor.GetTipAbonamentById(abonatii.AbonamentId);
            //Client client1;
            if (db.Clients.Count(s => s.ClientId == abonatii.ClientId) >0 &&
                db.TipAbonaments.Count(s => s.AbonamentId == abonatii.AbonamentId) >0)
            {
                db.Abonatiis.Add(abonatii);
                db.SaveChanges();
                return true;
                
            }
            else
            {
                return false;
            }
            //db.Abonatiis.Add(abonatii);
            //int result = db.SaveChanges();
            //if (result > 0)
            //{
            //    return true;
            //}

            //return false;
        }

        public bool UpdateAbonatii(Abonatii abonatii)
        {

            var aex = db.Abonatiis.Find(abonatii.AbonatiiId);
            if (aex == null)
            {
                db.Abonatiis.Add(abonatii);
            }
            else
            {
                db.Entry(aex).State = EntityState.Detached;
                db.Abonatiis.Add(abonatii);

                db.Entry(abonatii).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonatiiExists(abonatii.AbonatiiId))
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
        public bool DeleteAbonatii(int id)
        {
            Abonatii abonatii = db.Abonatiis.Find(id);

            db.Abonatiis.Remove(abonatii);
            db.SaveChanges();

            return true;
        }


        private bool AbonatiiExists(int id)
        {
            return db.Abonatiis.Count(e => e.AbonatiiId == id) > 0;
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
        //public AbonatiiAccesor()
        //{
        //    _pawentities = new ProiectContext();
        //}
        //public List<Abonatii> GetAbonatiLazy(int x)
        //{
        //    List<Abonatii> z = _pawentities.Abonatiis.ToList();

        //    foreach (var v in z)
        //    {
        //        v.Client = _pawentities.Clients.FirstOrDefault(c => c.ClientId == v.ClientId);
        //        //  Console.WriteLine(v);
        //        Console.WriteLine($"ID: {v.AbonatiiId}, Client id: {v.ClientId}, " +
        //            $"Abonament id: {v.AbonamentId}, Data start: {v.DataStart}, " +
        //            $"Data end: {v.DataEnd}, Nr utilizari: {v.NrUtilizari}, Data utilizarii {v.DataUtilizarii}");

        //    }
        //    return _pawentities.Abonatiis.Where(c => c.ClientId == 2).ToList();


        //    //List<Abonatii> z=_pawentities.Abonatiis.ToList();
        //    //foreach (var v in z)
        //    //{
        //    //    v.Tip_Abonament = _pawentities.Abonatiis.Where(u => u.ABONAMENT_ID == v.ABONAMENT_ID).ToList();
        //    //}
        //    ////var s=_pawentities.Abonatiis.First();
        //    ////var p = s.Client;
        //    ////var r = s.Tip_Abonament;
        //    //var rr =_pawentities.Abonatiis.FirstOrDefault(c=>c.CLIENT_ID==Client.CLIENT_ID);
        //    // return _pawentities.Abonatiis.Include(x => x.Client).Include(x => x.Tip_Abonament).ToList(); 

        //}
        //public List<Abonatii> GetAllAbonatii()
        //{
        //    //return _pawentities.Abonatiis.ToList();  // lazy loading

        //    return _pawentities.Abonatiis.Include(x => x.Client).Include(x => x.TipAbonament).ToList(); // eager loading
        //}
        //public Abonatii GetAbonatiitById(decimal abonatiiID)
        //{
        //    return _pawentities.Abonatiis.FirstOrDefault(c => c.AbonatiiId == abonatiiID);
        //}
        //public void AddAbonatii(Abonatii abonatii)
        //{
        //    _pawentities.Abonatiis.Add(abonatii);
        //    _pawentities.SaveChanges();
        //}
    }
}
