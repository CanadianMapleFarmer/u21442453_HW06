using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21442453_HW06.Models;

namespace u21442453_HW06.Controllers
{
    public class OrdersController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();

        // GET: Orders
        public ActionResult Index()
        {
            var prodTemp = from prod in db.products
                           select prod;
            var oTemp = from order in db.orders
                        select order;
            var oiTemp = db.order_items.Include(x => x.products);
            var sTemp = from stock in db.stocks
                         select stock;

            var orders = (from a in prodTemp
                          join b in sTemp on a.product_id equals b.product_id into temp1
                          from b in temp1.ToList()
                          join c in oiTemp on a.product_id equals c.product_id into temp2
                          from c in temp2.ToList()
                          join d in oTemp on c.order_id equals d.order_id into temp3
                          from d in temp3.ToList()
                          select new OrdersVM
                          {
                              products = a,
                              stocks = b,
                              order_Items = c,
                              orders = d
                          });  

            return View(orders);
        }

        //public ActionResult Search( string searchStrting)
        //{

        //}

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
