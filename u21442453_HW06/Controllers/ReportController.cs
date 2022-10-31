using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21442453_HW06.Models;

namespace u21442453_HW06.Controllers
{
    public class ReportController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();
        // GET: Report
        public ActionResult Index()
        {
            int[] data = new int[12];
            for (int i = 1; i <= 12; i++)
            {
                data[i - 1] = db.order_items.Where(x => x.products.category_id == 6 && x.orders.order_date.Month == i).ToList().Sum(x => x.quantity);
            }
           ViewBag.reportData = data;
            return View();
        }
    }
}