using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21442453_HW06.Models;
using PagedList;
using System.Diagnostics;

namespace u21442453_HW06.Controllers
{
    public class ProductsController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();

        // GET: Product
        public ActionResult Index(int? page, string searchText)
        {
            var products = db.products.Include(p => p.brands).Include(p => p.categories);

            if (!String.IsNullOrEmpty(searchText))
            {
                products = products.Where(x => x.product_name.Contains(searchText));
            }else
            {
                products = db.products.Include(p => p.brands).Include(p => p.categories);
            }
            int pageIndex = (page ?? 1);
            return View(products.ToList().ToPagedList(pageIndex, 10));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var prodTemp = from prod in db.products
                           select prod;
            var sTemp = from store in db.stores
                        select store;
            var stTemp = from stock in db.stocks
                         select stock;

            prodTemp = prodTemp.Where(x => x.product_id == id);

            var details =   (from a in prodTemp
                            join b in stTemp on a.product_id equals b.product_id into temp1
                            from b in temp1.ToList()
                            join c in sTemp on b.store_id equals c.store_id into temp2
                            from c in temp2.ToList()
                            select new DetailsVM
                            {
                                productDetails = a,
                                stockDetails = b,
                                storeDetails = c
                            }).ToList();
            

            if (details == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", details);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return PartialView("_Create");
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] products products)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return PartialView("_Edit", products);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", products);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchText)
        {
            var products = db.products.Include(p => p.brands).Include(p => p.categories);

            if (!String.IsNullOrEmpty(searchText))
            {
                products = products.Where(x => x.product_name.Contains(searchText));
            }

            return RedirectToAction("Index", new { searchText = searchText});
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
