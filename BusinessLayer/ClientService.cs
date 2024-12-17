using BusinessLayer.Interface;

using LibrarieModele;

using Repository_CodeFirst;
using NivelAccessDate.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivelAccessDate;
using BusinessLayer.CoreServices.Interfaces;
using System.Data.Entity.Infrastructure.Interception;
using NLog;
using System.ComponentModel.Design;

namespace BusinessLayer
{
    public class ClientService : IClient
    {
        private ICache cacheManager;
        private IClientAccessor clientAccesor;
        protected Logger logger = LogManager.GetCurrentClassLogger();

        public ClientService(IClientAccessor clientAccessor,ICache cacheManager)//(IProiectDbContext db)
        {
            this.clientAccesor=clientAccessor;
            this.cacheManager=cacheManager;
           // this._context=(ProiectContext)db;
        }

        public List<Client> GetAllClientsAsync()
        {
            string key = "clients_list_all";
            List<Client> clients;

            logger.Debug("Get all clients from db");

            if (cacheManager.IsSet(key))
            {
                clients = cacheManager.Get<List<Client>>(key);
            }
            else
            {
                clients = clientAccesor.GetClients();
                cacheManager.Set(key, clients);
            }

            return clients;

           // return _context.Clients.ToList();
        }

        public Client GetClientByIdAsync(int clientId)
        {
            Client client;
            string key = "clients_" + clientId;

            if (cacheManager.IsSet(key))
            {
                client = cacheManager.Get<Client>(key);
            }
            else
            {
                client = clientAccesor.GetClientById(clientId);
                cacheManager.Set(key, client);
            }

            return client;

            //return _context.Clients.Find(clientId);
        }

        public bool AddClientAsync(Client client)
        {
            bool result = clientAccesor.AddClient(client);
            if (result)
            {
                cacheManager.Remove("clients_list_all");
            }

            return true;

            //_context.Clients.Add(client);
            //_context.SaveChanges();
            //return true;
        }


        public bool UpdateClientAsync(Client client)
        {
            bool result = clientAccesor.UpdateClient(client);
            if (result)
            {
                string individual_key = "clients_" + client.ClientId;
                string list_key = "clients_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
            }

            return true;

            //_context.Entry(client).State = EntityState.Modified;
            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch(DbUpdateConcurrencyException)
            //{
            //    if(!ClientExists(client.ClientId))
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
            //return true;
        }

        public bool DeleteClientAsync(int clientId)
        {
            bool result = clientAccesor.DeleteClient(clientId);

            if (result)
            {
                string individual_key = "clients_" + clientId;
                string list_key = "clients_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
            }

            return true;
            //_context.Clients.Remove(client);
            //_context.SaveChanges();
            //return true;
        }

        //private bool ClientExists(int id)
        //{
        //    return _context.Clients.Count(e=>e.ClientId == id)>0;
        //}

    }
}
