using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using RentMgt.Models;

namespace RentMgt.Controllers
{
    [Authorize]
    public class HouseManagerController : Controller
    {
        private RentMgtEntities db = new RentMgtEntities();

        // GET: HouseManager
        public ActionResult Index()
        {            
            string umail = User.Identity.GetUserName().ToString();
            var items = db.Items.Include(i => i.Category).Include(i => i.Producer).Where(i=>i.Email == umail);
            return View(items.ToList());
        }

        
        public ActionResult Booking(int id)
        {
            var items = db.BookDetails.Where(i => i.ItemId == id).Where(i=>i.Status==null); 
            
            return View(items.ToList());
        }

        public ActionResult Accept(int? id)
        {
            var items = db.BookDetails.Single(course => course.BookDetailId == id);
            items.Status = "Accepted";
            db.SaveChanges();

            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail item = db.BookDetails.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Reject(int? id)
        {
            var items = db.BookDetails.Single(course => course.BookDetailId == id);
            items.Status = "Rejected";
            db.SaveChanges();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail item = db.BookDetails.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult AcceptList()
        {
            string umail = User.Identity.GetUserName().ToString();
            var items = db.BookDetails.Where(i => i.Creator == umail).Where(i=>i.Status=="Accepted");
            return View(items.ToList());
        }

        // GET: HouseManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: HouseManager/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name");
            return View();
        }

        // POST: HouseManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CategoryId,ProducerId,Email,Title,Description,Price,ItemArtUrl,Features,City,Address,Area,TotalFloor,Flooring,PropertyAge")] Item item)
        {
            if (ModelState.IsValid)
            {                
                db.Items.Add(item);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

        // GET: HouseManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

        // POST: HouseManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId,ProducerId,Title,Description,Price,ItemArtUrl,City,Address,Area,TotalFloor,Flooring,PropertyAge")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

        // GET: HouseManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: HouseManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
