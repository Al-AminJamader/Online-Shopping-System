using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SportStore.API.Models;
using System.Web.Http.Cors;

namespace SportStore.API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SportStore.API.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<StockIn>("StockIns");
    builder.EntitySet<Product>("Products"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class StockInsController : ODataController
    {
        private TechShopDbContext db = new TechShopDbContext();

        // GET: odata/StockIns
        [EnableQuery]
        public IQueryable<StockIn> GetStockIns()
        {
            return db.StockIns;
        }

        // GET: odata/StockIns(5)
        [EnableQuery]
        public SingleResult<StockIn> GetStockIn([FromODataUri] int key)
        {
            return SingleResult.Create(db.StockIns.Where(stockIn => stockIn.StockInId == key));
        }

        // PUT: odata/StockIns(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<StockIn> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StockIn stockIn = db.StockIns.Find(key);
            if (stockIn == null)
            {
                return NotFound();
            }
           
            patch.Put(stockIn);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockInExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stockIn);
        }

        // POST: odata/StockIns
        public IHttpActionResult Post(StockIn stockIn)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            var product = db.Products.First(p => p.Id == stockIn.ProductId);
            product.Stocklevel  += stockIn.quantity;
           
            db.StockIns.Add(stockIn);
            db.SaveChanges();
            
            return Created(stockIn);
        }

        // PATCH: odata/StockIns(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<StockIn> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StockIn stockIn = db.StockIns.Find(key);
            if (stockIn == null)
            {
                return NotFound();
            }

            patch.Patch(stockIn);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockInExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stockIn);
        }

        // DELETE: odata/StockIns(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            StockIn stockIn = db.StockIns.Find(key);
            if (stockIn == null)
            {
                return NotFound();
            }
            var stock = db.StockIns.First(x => x.StockInId == key);
            var product = db.Products.First(x => x.Id == stock.ProductId);
            product.Stocklevel -= stock.quantity;
            db.StockIns.Remove(stockIn);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StockIns(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.StockIns.Where(m => m.StockInId == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockInExists(int key)
        {
            return db.StockIns.Count(e => e.StockInId == key) > 0;
        }
    }
}
