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
using AutoMapper;
using BusinessLayer.Interface;
using Repository_CodeFirst;
using WebApiNet.Models;

namespace WebApiNet.Controllers
{
    public class TipAbonamentsController : ApiController
    {
        private ProiectContext db = new ProiectContext();

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LibrarieModele.TipAbonament, Models.TipAbonament>();
            cfg.CreateMap<Models.TipAbonament, LibrarieModele.TipAbonament>();
        });

        private ITipAbonament tipAbonamentService;

        public TipAbonamentsController(ITipAbonament tipAbonamentService)
        {
            this.tipAbonamentService = tipAbonamentService;
        }

        // GET: api/Clients
        public List<Models.TipAbonament> GetTipAbonaments()
        {
            List<LibrarieModele.TipAbonament> tipAbonamentFromDB = tipAbonamentService.GetAllTipAbonamentAsync();
            Mapper mapper = new Mapper(config);
            List<Models.TipAbonament> tipAbonament = mapper
                .Map<List<LibrarieModele.TipAbonament>, List<Models.TipAbonament>>(tipAbonamentFromDB);

            return tipAbonament;
        }


        // GET: api/Clients/5
        [ResponseType(typeof(Models.TipAbonament))]
        public IHttpActionResult GetTipAbonament(int id)
        {
            LibrarieModele.TipAbonament tipAbonamentFromDB = tipAbonamentService.GetTipAbonamentByIdAsync(id);

            if (tipAbonamentFromDB == null)
            {
                return NotFound();
            }
            var mapper = new Mapper(config);
            Models.TipAbonament tipAbonament = mapper
                .Map<LibrarieModele.TipAbonament, Models.TipAbonament>(tipAbonamentFromDB);

            return Ok(tipAbonament);
        }

        // POST: api/Clients //adaugare
        [ResponseType(typeof(Models.TipAbonament))]
        public IHttpActionResult PostClient(Models.TipAbonament tipAbonament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            LibrarieModele.TipAbonament tipAbonamentFromDB = mapper.Map<Models.TipAbonament, LibrarieModele.TipAbonament>(tipAbonament);

            //if (GetTipAbonamentName(tipAbonamentFromDB.Tip))
            //{
            //    return BadRequest("Clientul exista deja");
            //}
            //else
            //{
            //    tipAbonamentService.AddTipAbonamentAsync(tipAbonamentFromDB);
            //}

            tipAbonamentService.AddTipAbonamentAsync(tipAbonamentFromDB);

            return CreatedAtRoute("DefaultApi", new { id = tipAbonamentFromDB.AbonamentId }, tipAbonamentFromDB);
        }

        // PUT: api/Clients/5  //modificare
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Models.TipAbonament tipAbonament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipAbonament.AbonamentId || tipAbonament is null)
            {
                return BadRequest();
            }

            Mapper mapper = new Mapper(config);
            LibrarieModele.TipAbonament tipAbonamentFromDB = mapper
                .Map<Models.TipAbonament, LibrarieModele.TipAbonament>(tipAbonament);

            //if (GetTipAbonamentName(tipAbonamentFromDB.Tip))
            //{
            //    return BadRequest("Numele exista deja");
            //}

            bool updateSuccessfull = tipAbonamentService.UpdateTipAbonamentAsync(tipAbonamentFromDB);


            if (!updateSuccessfull)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.Accepted);
        }


        // DELETE: api/Clients/5
        [ResponseType(typeof(TipAbonament))]
        public IHttpActionResult DeleteClient(int id)
        {
            LibrarieModele.TipAbonament tipAbonament = tipAbonamentService.GetTipAbonamentByIdAsync(id);
            if (tipAbonament == null)
            {
                return NotFound();
            }

            tipAbonamentService.DeleteTipAbonamentAsync(id);

            return Ok(tipAbonament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool GetTipAbonamentName(string name)
        {
            return db.TipAbonaments.Count(e => e.Tip == name) > 0;
        }
    }
}