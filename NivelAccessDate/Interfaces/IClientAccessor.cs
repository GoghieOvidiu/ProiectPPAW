using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccessDate.Interfaces
{
    public interface IClientAccessor
    {
        List<Client> GetClients();
        Client GetClientById(int id);
        bool AddClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(int id);
    }
}
