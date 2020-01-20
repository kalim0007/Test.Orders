using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Core;
using Test.DataAccess.Sql;
using Test.Services;

namespace Test.OrderViewer.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Orders
        public ActionResult Index(string sortOrder)
        {
            var orders = from o in db.Orders select o;
            ViewData["DateSort"] = sortOrder == "date_desc" ? "date_asc" : "date_desc";
            switch (sortOrder)
            {
                case "date_asc":
                    orders = orders.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    orders = orders.OrderBy(s => s.CreatedAt);
                    break;
            }
            return View(orders);
        }

        public async Task<ActionResult> ConsumeOrders()
        {
            Task task = new Task(OrderServices.DeSerializeAllOrders);
            task.Start();
            await task;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> GenerateOrders()
        {
            OrderServices.StopGenerating = false;
            Task task = new Task(OrderServices.StartOders);
            task.Start();
            await task;
            return RedirectToAction(nameof(Index));
        }
        public ActionResult StopGeneratingOrders()
        {
            OrderServices.StopGenerating = true;
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
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
