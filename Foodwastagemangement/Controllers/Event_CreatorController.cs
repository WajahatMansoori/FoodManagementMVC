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
    public class Event_CreatorController : Controller
    {
        private AndroidDBEntities db = new AndroidDBEntities();

        [Authorize]
        // GET: Event_Creator
        public ActionResult Index()
        {
            var clientname=Session["UserName"];
            var ClientData = db.Event_Creator.Where(model => model.Event_Organizer_Name == clientname.ToString()).ToList();
            return View(ClientData);
            //return View(db.Event_Creator.ToList());
        }

        [Authorize]
        // GET: Event_Creator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Creator event_Creator = db.Event_Creator.Find(id);
            if (event_Creator == null)
            {
                return HttpNotFound();
            }
            return View(event_Creator);
        }
        [Authorize]
        // GET: Event_Creator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event_Creator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ev_Id,Ev_Name,Total_Members,Venue,DressCode,Event_Organizer_Name,Event_Organizer_Email")] Event_Creator event_Creator)
        {
            if (ModelState.IsValid)
            {
                string ClientName = Session["UserName"].ToString();
                var ClientInfo = db.clients.Where(model => model.C_Name == ClientName).FirstOrDefault();
                db.Event_Creator.Add(event_Creator);                
                event_Creator.Event_Organizer_Name = ClientName;
                event_Creator.Event_Organizer_Email = ClientInfo.C_Email;
                db.SaveChanges();
                Session["EventName"] = event_Creator.Ev_Name;
                return RedirectToAction("create","Invitation_Member");
            }

            return View(event_Creator);
        }

        [Authorize]
        // GET: Event_Creator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Creator event_Creator = db.Event_Creator.Find(id);
            if (event_Creator == null)
            {
                return HttpNotFound();
            }
            return View(event_Creator);
        }

        // POST: Event_Creator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ev_Id,Ev_Name,Total_Members,Venue,DressCode,Event_Organizer_Name,Event_Organizer_Email")] Event_Creator event_Creator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(event_Creator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(event_Creator);
        }

        [Authorize]
        // GET: Event_Creator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Creator event_Creator = db.Event_Creator.Find(id);
            if (event_Creator == null)
            {
                return HttpNotFound();
            }
            return View(event_Creator);
        }

        // POST: Event_Creator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event_Creator event_Creator = db.Event_Creator.Find(id);
            db.Event_Creator.Remove(event_Creator);
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
