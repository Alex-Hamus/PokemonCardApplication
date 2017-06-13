using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokemonCards.Models;
using Microsoft.AspNet.Identity;

namespace PokemonCards.Controllers
{
    [Authorize]
    public class CardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cards
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var cards = db.Cards.Where(x => x.ApplicationUserID == currentUser);
            return View(cards);
            //return View(db.Cards.ToList());
        }

        // GET: Cards/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cards card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            PopulateDDropDownList();
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Type,Weakness,Rarity,HitPoints,Attack,EvolutionStage,Quantity,ApplicationUserID")] Cards card)
        {
            if (ModelState.IsValid)
            {
                //db.Cards.Add(card);
                // db.SaveChanges();
                card.ApplicationUserID = User.Identity.GetUserId();
                db.Cards.Add(card);
                db.SaveChanges();
                PopulateDDropDownList(card.Name);
                return RedirectToAction("Index");
            }

            return View(card);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cards card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            PopulateDDropDownList(card.Name);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Type,Weakness,Rarity,HitPoints,Attack,EvolutionStage,Quantity,ApplicationUserID")] Cards card)
        {
            if (ModelState.IsValid)
            {
                 db.Entry(card).State = EntityState.Modified;
                 db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cards card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cards card = db.Cards.Find(id);
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateDDropDownList(object selected = null)
        {
            var cardsQuery = from d in db.Cards
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(cardsQuery, "ApplicationUserID", "Name", selected);
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
