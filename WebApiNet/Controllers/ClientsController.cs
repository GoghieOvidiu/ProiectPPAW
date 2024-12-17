using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Repository_CodeFirst;
using WebApiNet.Models;
using AutoMapper;
using BusinessLayer.Interface;

namespace WebApiNet.Controllers
{
    public class ClientsController : ApiController
    {
        private ProiectContext db = new ProiectContext();

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LibrarieModele.Client, Models.Client>();
            cfg.CreateMap<Models.Client, LibrarieModele.Client>();
        });

        private IClient clientService;

        public ClientsController(IClient clientService)
        {
            this.clientService = clientService;
        }

        // GET: api/Clients
        public List<Models.Client> GetClients()
        {
            List<LibrarieModele.Client> clientFromDB = clientService.GetAllClientsAsync();
            Mapper mapper = new Mapper(config);
            List<Models.Client> clients = mapper
                .Map<List<LibrarieModele.Client>, List<Models.Client>>(clientFromDB);

            return clients;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Models.Client))]
        public IHttpActionResult GetClient(int id)
        {
            LibrarieModele.Client clientFromDB = clientService.GetClientByIdAsync(id);

            if (clientFromDB == null)
            {
                return NotFound();
            }
            var mapper = new Mapper(config);
            Models.Client client = mapper
                .Map<LibrarieModele.Client, Models.Client>(clientFromDB);

            return Ok(client);
        }

        //// GET: api/Clients/5
        //[ResponseType(typeof(Models.Client))]
        //public IHttpActionResult GetClient(int id)
        //{
        //    LibrarieModele.Client clientFromDB = db.Clients.Find(id);

        //    if (clientFromDB == null)
        //    {
        //        return NotFound();
        //    }
        //    var mapper = new Mapper(config);
        //    Models.Client client = mapper
        //        .Map<LibrarieModele.Client, Models.Client>(clientFromDB);

        //    return Ok(client);
        //}

        // PUT: api/Clients/5  //modificare
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Models.Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId || client is null)
            {
                return BadRequest();
            }

            Mapper mapper = new Mapper(config);
            LibrarieModele.Client clientFromDB = mapper
                .Map<Models.Client, LibrarieModele.Client>(client);

            //if (GetClientName(clientFromDB.Nume))
            //{
            //    return BadRequest("Numele exista deja");
            //}
            
            bool updateSuccessfull = clientService.UpdateClientAsync(clientFromDB);
            

            if (!updateSuccessfull)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        //// PUT: api/Clients/5  //modificare
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutClient(int id, Models.Client client)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != client.ClientId || client is null)
        //    {
        //        return BadRequest();
        //    }

        //    Mapper mapper = new Mapper(config);
        //    LibrarieModele.Client clientFromDB = mapper
        //        .Map<Models.Client, LibrarieModele.Client>(client);

        //    db.Entry(clientFromDB).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Clients //adaugare
        [ResponseType(typeof(Models.Client))]
        public IHttpActionResult PostClient(Models.Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            LibrarieModele.Client clientFromDB = mapper.Map<Models.Client, LibrarieModele.Client>(client);

            //if(GetClientName(clientFromDB.Nume))
            //{
            //    return BadRequest("Clientul exista deja");
            //}
            //else
            //{
            //    clientService.AddClientAsync(clientFromDB);
            //}

            clientService.AddClientAsync(clientFromDB);

            return CreatedAtRoute("DefaultApi", new { id = clientFromDB.ClientId }, clientFromDB);
        }

        //// POST: api/Clients //adaugare
        //[ResponseType(typeof(Models.Client))]
        //public IHttpActionResult PostClient(Models.Client client)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    Mapper mapper = new Mapper(config);
        //    LibrarieModele.Client clientFromDB = mapper.Map<Models.Client, LibrarieModele.Client>(client);

        //    if (GetClientName(clientFromDB.Nume))
        //    {
        //        return BadRequest("Clientul exista deja");
        //    }
        //    else
        //    {
        //        db.Clients.Add(clientFromDB);
        //        db.SaveChanges();
        //    }



        //    return CreatedAtRoute("DefaultApi", new { id = clientFromDB.ClientId }, clientFromDB);
        //}

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            LibrarieModele.Client client = clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            clientService.DeleteClientAsync(id);

            return Ok(client);
        }

        //// DELETE: api/Clients/5
        //[ResponseType(typeof(Client))]
        //public IHttpActionResult DeleteClient(int id)
        //{
        //    LibrarieModele.Client client = db.Clients.Find(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Clients.Remove(client);
        //    db.SaveChanges();

        //    return Ok(client);
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
        }

        private bool GetClientName(string name)
        {
            return db.Clients.Count(e=>e.Nume==name)>0;
        }
    }
}