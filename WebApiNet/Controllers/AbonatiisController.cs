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
    public class AbonatiisController : ApiController
    {
        private ProiectContext db = new ProiectContext();

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LibrarieModele.Abonatii, Models.Abonatii>();
            cfg.CreateMap<Models.Abonatii, LibrarieModele.Abonatii>();
        });

        private IAbonatii abonatiiService;

        public AbonatiisController(IAbonatii abonatiiService)
        {
            this.abonatiiService = abonatiiService;
        }

        // GET: api/Clients
        public List<Models.Abonatii> GetAbonatiis()
        {
            List<LibrarieModele.Abonatii> abonatiiFromDB = abonatiiService.GetAllAbonatiiAsync();
            Mapper mapper = new Mapper(config);
            List<Models.Abonatii> abonatii = mapper
                .Map<List<LibrarieModele.Abonatii>, List<Models.Abonatii>>(abonatiiFromDB);

            return abonatii;
        }


        // GET: api/Clients/5
        [ResponseType(typeof(Models.Abonatii))]
        public IHttpActionResult GetAbonatiiById(int id)
        {
            LibrarieModele.Abonatii abonatiiFromDB = abonatiiService.GetAbonatiiByIdAsync(id);

            if (abonatiiFromDB == null)
            {
                return NotFound();
            }
            var mapper = new Mapper(config);
            Models.Abonatii abonatii = mapper
                .Map<LibrarieModele.Abonatii, Models.Abonatii>(abonatiiFromDB);

            return Ok(abonatii);
        }

        // POST: api/Clients //adaugare
        [ResponseType(typeof(Models.Abonatii))]
        public IHttpActionResult PostAbonatii(Models.Abonatii abonatii)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            LibrarieModele.Abonatii abonatiiFromDB = mapper.Map<Models.Abonatii, LibrarieModele.Abonatii>(abonatii);

            //if (GetTipAbonamentName(tipAbonamentFromDB.Tip))
            //{
            //    return BadRequest("Clientul exista deja");
            //}
            //else
            //{
            //    tipAbonamentService.AddTipAbonamentAsync(tipAbonamentFromDB);
            //}
            if(abonatiiService.AddAbonatiiAsync(abonatiiFromDB))
            {
                return CreatedAtRoute("DefaultApi", new { id = abonatiiFromDB.AbonatiiId }, abonatiiFromDB);
            }
            else
            {
                return BadRequest("Clientul sau tipul de abonament nu exista");
            }
            //abonatiiService.AddAbonatiiAsync(abonatiiFromDB);

            //return CreatedAtRoute("DefaultApi", new { id = abonatiiFromDB.AbonatiiId }, abonatiiFromDB);
        }

        // PUT: api/Clients/5  //modificare
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbonatii(int id, Models.Abonatii abonatii)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abonatii.AbonatiiId || abonatii is null)
            {
                return BadRequest();
            }

            Mapper mapper = new Mapper(config);
            LibrarieModele.Abonatii abonatiiFromDB = mapper
                .Map<Models.Abonatii, LibrarieModele.Abonatii>(abonatii);

            //if (GetTipAbonamentName(tipAbonamentFromDB.Tip))
            //{
            //    return BadRequest("Numele exista deja");
            //}

            bool updateSuccessfull = abonatiiService.UpdateAbonatiiAsync(abonatiiFromDB);


            if (!updateSuccessfull)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.Accepted);
        }


        // DELETE: api/Clients/5
        [ResponseType(typeof(Abonatii))]
        public IHttpActionResult DeleteAbonatii(int id)
        {
            LibrarieModele.Abonatii abonatii = abonatiiService.GetAbonatiiByIdAsync(id);
            if (abonatii == null)
            {
                return NotFound();
            }

            abonatiiService.DeleteAbonatiiAsync(id);

            return Ok(abonatii);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        private bool GetTipAbonamentName(string name)
        {
            return db.TipAbonaments.Count(e => e.Tip == name) > 0;
        }
    }
}