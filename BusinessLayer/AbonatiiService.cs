using BusinessLayer.CoreServices.Interfaces;
using BusinessLayer.Interface;
using LibrarieModele;
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
    public class AbonatiiService :IAbonatii
    {
        private ICache cacheManager;
        private IAbonatiiAccessor abonatiiAccesor;
        protected Logger logger = LogManager.GetCurrentClassLogger();

        public AbonatiiService(IAbonatiiAccessor abonatiiAccesor, ICache cacheManager)
        {
            this.abonatiiAccesor = abonatiiAccesor;
            this.cacheManager = cacheManager;
        }

        public List<Abonatii> GetAllAbonatiiAsync()
        {
            cacheManager.Clear();
            string key = "Abonatiit_list_all";
            List<Abonatii> abonatiis;

            logger.Debug("Get all Abonatii from db");

            if (cacheManager.IsSet(key))
            {
                abonatiis = cacheManager.Get<List<Abonatii>>(key);
            }
            else
            {
                abonatiis = abonatiiAccesor.GetAbonatiis();
                cacheManager.Set(key, abonatiis);
            }

            return abonatiis;
        }

        public Abonatii GetAbonatiiByIdAsync(int abonatiiId)
        {
            Abonatii abonatii;
            string key = "Abonatii_" + abonatiiId;

            if (cacheManager.IsSet(key))
            {
                abonatii = cacheManager.Get<Abonatii>(key);
            }
            else
            {
                abonatii = abonatiiAccesor.GetAbonatiiById(abonatiiId);
                cacheManager.Set(key, abonatii);
            }

            return abonatii;
        }

        public bool AddAbonatiiAsync(Abonatii abonatii)
        {
            bool result = abonatiiAccesor.AddAbonatii(abonatii);
            if (result)
            {
                cacheManager.Remove("Abonatii_list_all");
                return true;
            }

            return false;
        }


        public bool UpdateAbonatiiAsync(Abonatii abonatii)
        {
            bool result = abonatiiAccesor.UpdateAbonatii(abonatii);
            if (result)
            {
                string individual_key = "Abonatii_" + abonatii.AbonatiiId;
                string list_key = "Abonatii_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
            }

            return true;
        }

        public bool DeleteAbonatiiAsync(int abonatiiId)
        {
            bool result = abonatiiAccesor.DeleteAbonatii(abonatiiId);

            if (result)
            {
                string individual_key = "Abonatii_" + abonatiiId;
                string list_key = "Abonatii_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
            }

            return true;
        }
    }
}
