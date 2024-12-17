using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarieModele;
using BusinessLayer.Interface;
using WebAPICore.Data;
using WebAPICore.Models;
using AutoMapper;
using System.Web.Http.Description;
using System.Web.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;
namespace WebAPICore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        MapperConfiguration config=new MapperConfiguration(cfg=>
        {
            cfg.CreateMap<LibrarieModele.Client,WebAPICore.Models.Client>();
            cfg.CreateMap<WebAPICore.Models.Client,LibrarieModele.Client>();
        });

        private readonly ProiectContext _context;

        private IClient clientService;

        public ClientsController(IClient clientService)
        {
            this.clientService = clientService;
        }

        //public ClientsController(ProiectContext context)
        //{
        //    _context = context;
        //}
        // GET: api/Clients1
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<Models.Client> GetClientsAsync()
        {
            List<LibrarieModele.Client> clientFromDb= clientService.GetAllClientsAsync();
            Mapper mapper=new Mapper(config);
            List<Models.Client> clients=mapper
                .Map<List<LibrarieModele.Client>,List<Models.Client>>(clientFromDb);
            return clients;
        }

        // GET: api/Clients1
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        //{
        //  if (_context.Clients == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Clients.ToListAsync();
        //}

        // GET: api/Clients1/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        [ResponseType(typeof(Models.Client))]
        public IHttpActionResult GetClientById(int id)
        {
            LibrarieModele.Client clientFromDB= clientService.GetClientByIdAsync(id);
            if (clientFromDB == null)
            {
                return (IHttpActionResult)NotFound();
            }
            var mapper=new Mapper(config);
            Models.Client client=mapper
                .Map<LibrarieModele.Client,Models.Client>(clientFromDB);
            return (IHttpActionResult)Ok(client);
        }

        // GET: api/Clients1/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Client>> GetClient(int id)
        //{
        //    if (_context.Clients == null)
        //    {
        //        return NotFound();
        //    }
        //    var client = await _context.Clients.FindAsync(id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return client;
        //}

        // [HttpGet("{nume}/clientnume")]
        [Microsoft.AspNetCore.Mvc.HttpGet("{nume}/clientnume")]
        [ResponseType(typeof(Models.Client))]
        public async Task<ActionResult<Models.Client>> GetClientNume(string nume)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Nume == nume);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        //// PUT: api/Clients1/5 // modifica client
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        [ResponseType(typeof(void))]
        public  IHttpActionResult PutClient(int id, Models.Client client)
        {
            if (ModelState.IsValid)
            {
                return (IHttpActionResult)BadRequest(ModelState);
            }
            if(client is null || id!=client.ClientId)
            {
                return (IHttpActionResult)BadRequest();
            }

            Mapper mapper = new Mapper(config);
            LibrarieModele.Client clientFromDB = mapper
                .Map<Models.Client, LibrarieModele.Client>(client);

            bool updateSuccessfull = clientService.UpdateClientAsync(clientFromDB);
            if (!updateSuccessfull)
            {
                return (IHttpActionResult)NotFound();
            }

            return (IHttpActionResult)StatusCode((int)HttpStatusCode.NoContent);
        }

        //// PUT: api/Clients1/5 // modifica client
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClient(int id, Client client)
        //{
        //    if (id != client.ClientId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(client).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
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

        //    return NoContent();
        //}


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ResponseType(typeof(Models.Client))]
        public IHttpActionResult PostClient(Models.Client client)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)BadRequest(ModelState);
            }

            Mapper mapper = new Mapper(config);
            LibrarieModele.Client clientFromDB = mapper.Map<Models.Client, LibrarieModele.Client>(client);

            clientService.AddClientAsync(clientFromDB);

            return (IHttpActionResult)CreatedAtRoute("DefaultApi", new { id = clientFromDB.ClientId }, clientFromDB);
        }


        // POST: api/Clients1    //adauga client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Client>> PostClient(Client client)
        //{
        //    if (_context.Clients == null)
        //    {
        //        return Problem("Entity set 'ProiectContext.Clients'  is null.");
        //    }


        //    var client2 = await _context.Clients.FirstOrDefaultAsync(c => c.Nume == client.Nume);

        //    if(client2 is null)
        //    {
        //        client = new Client()
        //        {
        //            Nume = client.Nume,
        //            Email = client.Email,
        //            Parola = client.Parola,
        //            DataStart = System.DateTime.Now
        //        };
        //        _context.Clients.Add(client);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return Problem("Numele este deja");
        //    }

        //    return CreatedAtAction("GetClientNume", new { nume = client.Nume }, client);
        //}

        // DELETE: api/Clients1/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        [ResponseType(typeof(Models.Client))]
        public IHttpActionResult DeleteCompany(int id)
        {
            LibrarieModele.Client client = clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return (IHttpActionResult)NotFound();
            }

            clientService.DeleteClientAsync(id);

            return (IHttpActionResult)Ok(client);
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClient(int id)
        //{
        //    if (_context.Clients == null)
        //    {
        //        return NotFound();
        //    }
        //    var client = await _context.Clients.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Clients.Remove(client);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
