using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopBridge;
using ShopBridge.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace ShopBridge.Controllers
{
    public class ShopViewController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        static readonly HttpClient client = new HttpClient();

        // GET: ShopView
        public async Task<ActionResult> Index()
        {
            return View(await db.productinfos.ToListAsync());
        }

        // GET: ShopView/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ShopFields shopFields = await db.productinfos.FindAsync(id);
            HttpResponseMessage response = await client.GetAsync("http://localhost:57902/api/ShopFields/GetShopFields/"+id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ShopFields>(responseBody);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: ShopView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ProductName,ProductPrice,ProductDesp")] ShopFields shopFields)
        {
            if (ModelState.IsValid)
            {
                db.productinfos.Add(shopFields);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shopFields);
        }

        // GET: ShopView/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          //  ShopFields shopFields = await db.productinfos.FindAsync(id);
            HttpResponseMessage response = await client.GetAsync("http://localhost:57902/api/ShopFields/GetShopFields/" + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ShopFields>(responseBody);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: ShopView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ProductName,ProductPrice,ProductDesp")] ShopFields shopFields)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(shopFields).State = EntityState.Modified;
                // await db.SaveChangesAsync();
                var objAsJson = new JavaScriptSerializer().Serialize(shopFields);
                var stringContent = new StringContent(objAsJson, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("http://localhost:57902/api/ShopFields/PutShopFields/" + shopFields.ProductId, stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ShopFields>(responseBody);
              
                return RedirectToAction("Index");
            }
            return View(shopFields);
        }

        // GET: ShopView/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          // ShopFields shopFields = await db.productinfos.FindAsync(id);
            HttpResponseMessage response = await client.GetAsync("http://localhost:57902/api/ShopFields/GetShopFields/" + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
           
            var result = JsonConvert.DeserializeObject<ShopFields>(responseBody);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: ShopView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           // ShopFields shopFields = await db.productinfos.FindAsync(id);
           // db.productinfos.Remove(shopFields);
           // await db.SaveChangesAsync();
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:57902/api/ShopFields/DeleteShopFields/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
