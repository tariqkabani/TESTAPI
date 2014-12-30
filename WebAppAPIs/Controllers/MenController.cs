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
using WebAppAPIs.Models;

namespace WebAppAPIs.Controllers
{
    public class MenController : ApiController
    {
        private SupportEntities db = new SupportEntities();

        // GET: api/Men
        public IQueryable<Men> GetMen()
        {
            return db.Men;
             //.Include(b => b.ApplicantsType);
        }

        // GET: api/Men/5
        [ResponseType(typeof(Men))]
        public IHttpActionResult GetMen(string id)
        {
            Men men = db.Men.Find(id);
            if (men == null)
            {
                return NotFound();
            }

            return Ok(men);
        }

        // PUT: api/Men/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMen(string id, Men men)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != men.ID)
            {
                return BadRequest();
            }

            db.Entry(men).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Men
        [ResponseType(typeof(Men))]
        public IHttpActionResult PostMen(Men men)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Men.Add(men);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MenExists(men.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = men.ID }, men);
        }

        // DELETE: api/Men/5
        [ResponseType(typeof(Men))]
        public IHttpActionResult DeleteMen(string id)
        {
            Men men = db.Men.Find(id);
            if (men == null)
            {
                return NotFound();
            }

            db.Men.Remove(men);
            db.SaveChanges();

            return Ok(men);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenExists(string id)
        {
            return db.Men.Count(e => e.ID == id) > 0;
        }
    }
}