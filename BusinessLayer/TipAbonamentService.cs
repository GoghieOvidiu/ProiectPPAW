using BusinessLayer.CoreServices.Interfaces;
using BusinessLayer.Interface;
using LibrarieModele;

using NivelAccessDate;
using NivelAccessDate.Interfaces;

using NLog;

using Repository_CodeFirst;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TipAbonamentService : ITipAbonament
    {
        private ICache cacheManager;
        private ITipAbonamentAccessor tipAbonamentAccesor;
        protected Logger logger = LogManager.GetCurrentClassLogger();

        public TipAbonamentService(ITipAbonamentAccessor tipAbonamentAccesor, ICache cacheManager)
        {
            this.tipAbonamentAccesor = tipAbonamentAccesor;
            this.cacheManager = cacheManager;
        }

        public List<TipAbonament> GetAllTipAbonamentAsync()
        {
           // string key2 = "tipAbonament_list_all";
            List<TipAbonament> tipAbonaments;

           // logger.Debug("Get all tipAbonament from db");

            //if (cacheManager.IsSet(key2))
            //{
            //    tipAbonaments = cacheManager.Get<List<TipAbonament>>(key2);
            //}
            //else
            //{
                tipAbonaments = tipAbonamentAccesor.GetTipAbonaments();
            //    cacheManager.Set(key2, tipAbonaments);
            //}

            return tipAbonaments;
        }

        public TipAbonament GetTipAbonamentByIdAsync(int tipAbonamentId)
        {
            TipAbonament tipAbonament;
            //string key2 = "tipAbonaments_" + tipAbonamentId;

            //if (cacheManager.IsSet(key2))
            //{
            //    tipAbonament = cacheManager.Get<TipAbonament>(key2);
            //}
            //else
            //{
                tipAbonament = tipAbonamentAccesor.GetTipAbonamentById(tipAbonamentId);
            //    cacheManager.Set(key2, tipAbonament);
            //}

            return tipAbonament;
        }

        public bool AddTipAbonamentAsync(TipAbonament tipAbonament)
        {
            bool result = tipAbonamentAccesor.AddTipAbonament(tipAbonament);
            //if (result)
            //{
            //    cacheManager.Remove("tipAbonaments_list_all");
            //}

            return true;
        }


        public bool UpdateTipAbonamentAsync(TipAbonament tipAbonament)
        {
            bool result = tipAbonamentAccesor.UpdateTipAbonament(tipAbonament);
            //if (result)
            //{
            //    string individual_key = "tipAbonaments_" + tipAbonament.AbonamentId;
            //    string list_key = "tipAbonaments_list";
            //    cacheManager.Remove(individual_key);
            //    cacheManager.RemoveByPattern(list_key);
            //}

            return true;
        }

        public bool DeleteTipAbonamentAsync(int tipAbonamentId)
        {
            bool result = tipAbonamentAccesor.DeleteTipAbonament(tipAbonamentId);

            //if (result)
            //{
            //    string individual_key = "tipAbonaments_" + tipAbonamentId;
            //    string list_key = "tipAbonaments_list";
            //    cacheManager.Remove(individual_key);
            //    cacheManager.RemoveByPattern(list_key);
            //}

            return true;
        }


    }
}
