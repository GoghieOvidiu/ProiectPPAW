using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAbonatii
    {
        List<Abonatii> GetAllAbonatiiAsync();
        Abonatii GetAbonatiiByIdAsync(int abonatiiId);
        bool AddAbonatiiAsync(Abonatii abonatii);
        bool UpdateAbonatiiAsync(Abonatii abonatii);
        bool DeleteAbonatiiAsync(int abonatiiId);
    }
}
