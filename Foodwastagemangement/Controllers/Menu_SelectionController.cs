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
    public class Menu_SelectionController : Controller
    {
        private AndroidDBEntities db = new AndroidDBEntities();

        [Authorize]
        // GET: Menu_Selection
        public ActionResult Index()
        {
            var clientname = Session["UserName"];
            var getrecord = db.Menu_Selection.Where(model => model.Event_Organizer_Name == clientname.ToString()).ToList();
            return View(getrecord);
            //return View(db.Menu_Selection.ToList());
        }

        [Authorize]
        // GET: Menu_Selection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Selection menu_Selection = db.Menu_Selection.Find(id);
            if (menu_Selection == null)
            {
                return HttpNotFound();
            }
            return View(menu_Selection);
        }

        [Authorize]
        // GET: Menu_Selection/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu_Selection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_Id,Event_Name,Cat_Name,Member_Name,Event_Organizer_Name")] Menu_Selection menu_Selection)
        {
            if (ModelState.IsValid)
            {                
                string Invitorname = Session["UserName"].ToString();
                //var Data = db.Menu_Selection.Where(m => m.Event_Name == Invitorname).FirstOrDefault();
                //var Data2=db.Event_Creator.Where(m=>m.Event_Organizer_Name==)
                var Data3 = db.Invitation_Member.Where(x => x.Member_Name == Invitorname).FirstOrDefault();
                db.Menu_Selection.Add(menu_Selection);
                //var getdata = db.Invitation_Member.Where(x => x.Ev_Name == Data3.Ev_Name).FirstOrDefault();
                menu_Selection.Member_Name = Invitorname;
                menu_Selection.Event_Name = Data3.Ev_Name;
                menu_Selection.Event_Organizer_Name = Data3.Event_Organizer_Name;
                TempData["Msg"] = "<script>alert('Your Menu Has been Selected')</script>";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu_Selection);
        }

        [Authorize]
        // GET: Menu_Selection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Selection menu_Selection = db.Menu_Selection.Find(id);
            if (menu_Selection == null)
            {
                return HttpNotFound();
            }
            return View(menu_Selection);
        }

        // POST: Menu_Selection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_Id,Event_Name,Cat_Name,Member_Name,Event_Organizer_Name")] Menu_Selection menu_Selection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu_Selection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu_Selection);
        }

        [Authorize]
        // GET: Menu_Selection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Selection menu_Selection = db.Menu_Selection.Find(id);
            if (menu_Selection == null)
            {
                return HttpNotFound();
            }
            return View(menu_Selection);
        }

        // POST: Menu_Selection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu_Selection menu_Selection = db.Menu_Selection.Find(id);
            db.Menu_Selection.Remove(menu_Selection);
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
