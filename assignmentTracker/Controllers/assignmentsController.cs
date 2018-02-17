using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using assignmentTracker.Models;

namespace assignmentTracker.Controllers
{
    public class assignmentsController : Controller
    {
        private AssignmentTrackerModel db = new AssignmentTrackerModel();

        // GET: assignments
        public ActionResult Index()
        {
            var assignments = db.assignments.Include(a => a.cours);
            return View(assignments.ToList());
        }

        // GET: assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            assignment assignment = db.assignments.Find(id);
            if (assignment == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            return View(assignment);
        }

        // GET: assignments/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name");
            return View();
        }

        // POST: assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assignment_id,title,description,due_date,course_id")] assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name", assignment.course_id);
            return View(assignment);
        }

        // GET: assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            assignment assignment = db.assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name", assignment.course_id);
            return View(assignment);
        }

        // POST: assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "assignment_id,title,description,due_date,course_id")] assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name", assignment.course_id);
            return View(assignment);
        }

        // GET: assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            assignment assignment = db.assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            assignment assignment = db.assignments.Find(id);
            db.assignments.Remove(assignment);
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
