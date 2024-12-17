using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccessDate.Interfaces
{
    public interface IAbonatiiAccessor
    {
        List<Abonatii> GetAbonatiis();
        Abonatii GetAbonatiiById(int id);
        bool AddAbonatii(Abonatii abonatii);
        bool UpdateAbonatii(Abonatii abonatii);
        bool DeleteAbonatii(int id);
    }
}
