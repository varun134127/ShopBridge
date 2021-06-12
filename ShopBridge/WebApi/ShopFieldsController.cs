using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShopBridge;
using ShopBridge.Models;

namespace ShopBridge.WebApi
{
    public class ShopFieldsController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: api/ShopFields
        public IQueryable<ShopFields> Getproductinfos()
        {
            return db.productinfos;
        }

        // GET: api/ShopFields/5
        [ResponseType(typeof(ShopFields))]
        public async Task<IHttpActionResult> GetShopFields(int id)
        {
            ShopFields shopFields = await db.productinfos.FindAsync(id);
            if (shopFields == null)
            {
                return NotFound();
            }

            return Ok(shopFields);
        }

        // PUT: api/ShopFields/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShopFields(int id, ShopFields shopFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopFields.ProductId)
            {
                return BadRequest();
            }

            db.Entry(shopFields).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopFieldsExists(id))
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

        // POST: api/ShopFields
        [ResponseType(typeof(ShopFields))]
        public async Task<IHttpActionResult> PostShopFields(ShopFields shopFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.productinfos.Add(shopFields);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = shopFields.ProductId }, shopFields);
        }

        // DELETE: api/ShopFields/5
        [ResponseType(typeof(ShopFields))]
        public async Task<IHttpActionResult> DeleteShopFields(int id)
        {
            ShopFields shopFields = await db.productinfos.FindAsync(id);
            if (shopFields == null)
            {
                return NotFound();
            }

            db.productinfos.Remove(shopFields);
            await db.SaveChangesAsync();

            return Ok(shopFields);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopFieldsExists(int id)
        {
            return db.productinfos.Count(e => e.ProductId == id) > 0;
        }
    }
}