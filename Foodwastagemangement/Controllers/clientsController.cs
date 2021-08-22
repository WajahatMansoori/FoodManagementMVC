using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Foodwastagemangement.Models;

namespace Foodwastagemangement.Controllers
{
    public class clientsController : Controller
    {
        private AndroidDBEntities db = new AndroidDBEntities();

        [Authorize]
        // GET: clients
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                string clientname = Session["UserName"].ToString();
                Session["UName"] = clientname;
            }
            
            var LoginInfo = db.clients.Where(model => model.C_Name == Session["UName"].ToString()).ToList();
            return View(LoginInfo);
        }

        [Authorize]
        // GET: clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize]
        // GET: clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_Id,C_Name,C_Email,C_Password,C_Phone")] client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        [Authorize]
        // GET: clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_Id,C_Name,C_Email,C_Password,C_Phone")] client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [Authorize]
        // GET: clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client client = db.clients.Find(id);
            db.clients.Remove(client);
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(client client, string ReturnUrl)
        {
            //var passDecoder = p.Encrypt(admin.Password);
            var ClientLogin = db.clients.Where(model => model.C_Name == client.C_Name && model.C_Password == client.C_Password).FirstOrDefault();
            if (ClientLogin != null)
            {
                FormsAuthentication.SetAuthCookie(client.C_Name, false);
                Session["UserName"] = client.C_Name.ToString();                
                if (ReturnUrl != null)
                {
                    TempData["AdminLogin"] = "<script>alert('Login Successfully')</script>";
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("LandingPage", "clients");
                }
            }
            else
            {
                TempData["AdminfailLogin"] = "<script>alert('Incorrect UserName or Password')</script>";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "clients");
        }

        public ActionResult LandingPage()
        {
            var clientname = Session["UserName"];
            var eventCreator = db.Event_Creator.Where(model => model.Event_Organizer_Name == clientname.ToString()).FirstOrDefault();
            if (eventCreator != null)
            {
                Session["ShowNav"] = "true";
            }
            else
            {
                Session["ShowNav"] = "false";
            }
            return View();
        }
    }
}
