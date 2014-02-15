﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Derby.Models;
using Derby.ViewModels;

namespace Derby.Controllers
{
    public class HeatController : Controller
    {
        private DerbyDb db = new DerbyDb();

        // GET: /Heat/
        public ActionResult Index()
        {
            return View(db.Heats.ToList());
        }

        // GET: /Heat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heat heat = db.Heats.Find(id);
            if (heat == null)
            {
                return HttpNotFound();
            }
            return View(heat);
        }

        // GET: /Heat/Create
        public ActionResult Create(int raceId)
        {
            Race race = db.Races.Find(raceId);
            Competition competition = db.Competitions.Find(race.CompetitionId);

            ModifyHeatViewModel view = new ModifyHeatViewModel();
            view.RaceId = raceId;
            view.Racers = new Collection<RacerContestantViewModel>();
            view.Competition = competition;
            view.CurrentHeats = db.Heats.Where(x => x.RaceId == raceId).ToList();

            List<Scout> scouts = new List<Scout>(db.Scouts.Where(x => x.PackId == competition.PackId));
            if (scouts.Any())
            {
                foreach (var item in db.Racers.Where(x => x.DenId == race.DenId).ToList())
                {
                    var racer = new RacerContestantViewModel(item);
                    racer.ScoutName = scouts.FirstOrDefault(x => x.Id == item.ScoutId).Name;

                    view.Racers.Add(racer);
                }
            }

            view.HeatsNeeded = Infrastructure.HeatGenerator.GenerateHeatCount(competition.LaneCount, view.Racers.Count());

            return View(view);
        }

        // POST: /Heat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RaceId,Racers")] ModifyHeatViewModel heat)
        {
            if (ModelState.IsValid)
            {
                Heat _heat = new Heat();
                _heat.CreatedDate = DateTime.Now;
                _heat.RaceId = heat.RaceId;

                db.Heats.Add(_heat);
                db.SaveChanges();

                Race race = db.Races.Find(heat.RaceId);
                var _competitionId = race.CompetitionId;

                return RedirectToAction("Index", "Race", new { competitionId = _competitionId });
            }

            return View(heat);
        }

        // GET: /Heat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heat heat = db.Heats.Find(id);
            if (heat == null)
            {
                return HttpNotFound();
            }
            return View(heat);
        }

        // POST: /Heat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RaceId")] Heat heat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(heat);
        }

        // GET: /Heat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heat heat = db.Heats.Find(id);
            if (heat == null)
            {
                return HttpNotFound();
            }
            return View(heat);
        }

        // POST: /Heat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Heat heat = db.Heats.Find(id);
            db.Heats.Remove(heat);
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
