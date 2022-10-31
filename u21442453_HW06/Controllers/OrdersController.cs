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

namespace u21442453_HW06.Controllers
{
    public class OrdersController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();

        // GET: Orders
        public ActionResult Index(int? page)
        {
            var orders = db.order_items.Include(x => x.products).Include(x => x.orders).ToList();
            int pageSize = 20;
            int pageIndex = (page ?? 1);
            return View(orders.ToPagedList(pageIndex, pageSize));
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
