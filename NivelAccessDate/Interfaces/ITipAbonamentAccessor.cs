using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccessDate.Interfaces
{
    public interface ITipAbonamentAccessor
    {
        List<TipAbonament> GetTipAbonaments();
        TipAbonament GetTipAbonamentById(int id);
        bool AddTipAbonament(TipAbonament tipAbonament);
        bool UpdateTipAbonament(TipAbonament tipAbonament);
        bool DeleteTipAbonament(int id);
    }
}
