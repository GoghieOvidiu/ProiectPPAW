using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ITipAbonament
    {
        List<TipAbonament> GetAllTipAbonamentAsync();
        TipAbonament GetTipAbonamentByIdAsync(int tipAbonamentId);
        bool AddTipAbonamentAsync(TipAbonament tipAbonament);
        bool UpdateTipAbonamentAsync(TipAbonament tipAbonament);
        bool DeleteTipAbonamentAsync(int tipAbonamentId);
    }
}
