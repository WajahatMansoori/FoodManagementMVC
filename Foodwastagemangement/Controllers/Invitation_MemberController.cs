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
    public class Invitation_MemberController : Controller
    {
        private AndroidDBEntities db = new AndroidDBEntities();

        // GET: Invitation_Member
        public ActionResult Index()
        {            
            var clientname = Session["UserName"];            
            var eventCreator = db.Event_Creator.Where(model => model.Event_Organizer_Name == clientname.ToString()).FirstOrDefault();
            if (eventCreator != null)
            {
                return View(db.Invitation_Member.Where(x => x.Event_Organizer_Name == eventCreator.Event_Organizer_Name).ToList());
            }
            TempData["HideButton"] = "true";
            //Session["NavHide"] = "true";
            var ClientData = db.Invitation_Member.Where(model => model.Member_Name == clientname.ToString()).ToList();
           
            return View(ClientData);
            //return View(db.Invitation_Member.ToList());
        }

        // GET: Invitation_Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation_Member invitation_Member = db.Invitation_Member.Find(id);
            if (invitation_Member == null)
            {
                return HttpNotFound();
            }
            return View(invitation_Member);
        }

        // GET: Invitation_Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invitation_Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Member_Id,Member_Name,Member_Phone,Ev_Name")] Invitation_Member invitation_Member)
        {           
            if (ModelState.IsValid)
            {
                string ClientName = Session["UserName"].ToString();               
                var GeteventName = db.Event_Creator.Where(model => model.Event_Organizer_Name == ClientName).FirstOrDefault();
                if (GeteventName != null)
                {
                    invitation_Member.Ev_Name = GeteventName.Ev_Name;
                }
                invitation_Member.Event_Organizer_Name = ClientName;
                db.Invitation_Member.Add(invitation_Member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invitation_Member);
        }

        // GET: Invitation_Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation_Member invitation_Member = db.Invitation_Member.Find(id);
            if (invitation_Member == null)
            {
                return HttpNotFound();
            }
            return View(invitation_Member);
        }

        // POST: Invitation_Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Member_Id,Member_Name,Member_Phone,Ev_Name")] Invitation_Member invitation_Member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation_Member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invitation_Member);
        }

        // GET: Invitation_Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation_Member invitation_Member = db.Invitation_Member.Find(id);
            if (invitation_Member == null)
            {
                return HttpNotFound();
            }
            return View(invitation_Member);
        }

        // POST: Invitation_Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation_Member invitation_Member = db.Invitation_Member.Find(id);
            db.Invitation_Member.Remove(invitation_Member);
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
