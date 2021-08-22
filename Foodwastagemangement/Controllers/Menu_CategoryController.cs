using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Foodwastagemangement.Models;

namespace Foodwastagemangement.Controllers
{
    public class Menu_CategoryController : Controller
    {
        private AndroidDBEntities db = new AndroidDBEntities();

        [Authorize]
        // GET: Menu_Category
        public ActionResult Index()
        {
            var clientname = Session["UserName"];
            var eventCreator = db.Event_Creator.Where(model => model.Event_Organizer_Name == clientname.ToString()).FirstOrDefault();
            if (eventCreator != null)
            {
                return View(db.Menu_Category.Where(model => model.Event_Name == eventCreator.Ev_Name).ToList());
            }
            else
            {
                TempData["eventCreate"] = "<script>alert('First Create Your Event')</script>";
                return RedirectToAction("create", "Event_Creator");
            }
            //return View(db.Menu_Category.ToList());
        }

        [Authorize]
        // GET: Menu_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Category menu_Category = db.Menu_Category.Find(id);
            if (menu_Category == null)
            {
                return HttpNotFound();
            }
            return View(menu_Category);
        }

        [Authorize]
        // GET: Menu_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cat_Id,Cat_Name,Event_Name")] Menu_Category menu_Category)
        {
            if (ModelState.IsValid)
            {
                db.Menu_Category.Add(menu_Category);
                var clientname = Session["UserName"];
                var eventCreator = db.Event_Creator.Where(model => model.Event_Organizer_Name == clientname.ToString()).FirstOrDefault();
                if (eventCreator != null)
                {
                    menu_Category.Event_Name = eventCreator.Ev_Name;
                }
                
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(menu_Category);
        }

        [Authorize]
        // GET: Menu_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Category menu_Category = db.Menu_Category.Find(id);
            if (menu_Category == null)
            {
                return HttpNotFound();
            }
            return View(menu_Category);
        }

        // POST: Menu_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cat_Id,Cat_Name,Event_Name")] Menu_Category menu_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu_Category);
        }

        [Authorize]
        // GET: Menu_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Category menu_Category = db.Menu_Category.Find(id);
            if (menu_Category == null)
            {
                return HttpNotFound();
            }
            return View(menu_Category);
        }

        // POST: Menu_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu_Category menu_Category = db.Menu_Category.Find(id);
            db.Menu_Category.Remove(menu_Category);
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
