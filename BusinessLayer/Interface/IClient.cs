using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IClient
    {
        List<Client>GetAllClientsAsync();
        Client GetClientByIdAsync(int clientId);
        bool AddClientAsync(Client client);
        bool UpdateClientAsync(Client client);
        bool DeleteClientAsync(int clientId);
    }
}
