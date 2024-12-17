using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;

using NivelAccessDate.Interfaces;

using NLog;

using Repository_CodeFirst;

namespace NivelAccessDate
{
    public class ClientAccesor : IClientAccessor
    {
        private ProiectContext db;
        protected Logger logger = LogManager.GetCurrentClassLogger();
        public ClientAccesor(IProiectDbContext db)
        {
            this.db=(ProiectContext)db;
        }

        public List<Client> GetClients()
        {
            List<Client> clients;
            try
            {
                clients = db.Clients.AsNoTracking().ToList();
            }
            catch(Exception exception)
            {
                logger.Error(exception, "Error on getting clients from db");
                return null;
            }
           
            return clients;
        }

        public Client GetClientById(int id)
        {
            Client client=db.Clients.AsNoTracking().FirstOrDefault(localClient=>localClient.ClientId==id);
            return client;
        }

        public bool AddClient(Client client)
        {
           
            db.Clients.Add(client);
            int result=db.SaveChanges();
            if(result>0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateClient(Client client)
        {
            var aex = db.Clients.Find(client.ClientId);
            if(aex==null)
            {
                db.Clients.Add(client);
            }
            else
            {
                db.Entry(aex).State = EntityState.Detached;
                db.Clients.Add(client);

                db.Entry(client).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ClientExists(client.ClientId))
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

        public bool DeleteClient(int clientid)
        {
            Client client=db.Clients.Find(clientid);
            db.Clients.Remove(client);
            db.SaveChanges();
            return true;
        }


        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
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

        //public ClientAccesor()
        //{
        //    _pawentities = new ProiectContext();
        //}
        //public List<Client> GetAllClients()
        //{
        //    return _pawentities.Clients.ToList();

        //}
        //public Client GetClientById(decimal clientId)
        //{
        //    return _pawentities.Clients.FirstOrDefault(c => c.ClientId == clientId);
        //}
        //public void AddClient(Client client)
        //{
        //    _pawentities.Clients.Add(client);
        //    _pawentities.SaveChanges();
        //}
        //public Client GetClientByName(string name)
        //{
        //    return _pawentities.Clients.FirstOrDefault(c => c.Nume == name);
        //}
        //public void UpdateClient(Client client)
        //{
        //    var existingClient = _pawentities.Clients.Find(client.ClientId);
        //    if (existingClient != null)
        //    {
        //        existingClient.Nume = client.Nume;
        //        existingClient.Email = client.Email;
        //        existingClient.Parola = client.Parola;
        //        existingClient.DataStart = client.DataStart;
        //        _pawentities.SaveChanges();
        //    }
        //}
        //public void DeleteClient(decimal clientId)
        //{
        //    var client = _pawentities.Clients.Find(clientId);
        //    if (client != null)
        //    {
        //        _pawentities.Clients.Remove(client);
        //        _pawentities.SaveChanges();
        //    }
        //}
    }
}
