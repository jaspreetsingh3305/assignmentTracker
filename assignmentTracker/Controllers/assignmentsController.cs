using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using assignmentTracker.Models;
using assignmentTracker.Controllers;


namespace assignmentTracker.Controllers
{
    public class assignmentsController : Controller
    {

        //database connection moved to the test EFAssignmentRepository Model

        //  private AssignmentTrackerModel db = new AssignmentTrackerModel();

        private IMockAssignmentRepository db;
        

        //if no dependency then use the database connection
        public assignmentsController()
        {
            db = new EFAssignmentRepository();
        }

        //if mock data provide--use it instead of database connection
        public assignmentsController(IMockAssignmentRepository mockData)
        {
            db = mockData;
        }


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
                //return RedirectToAction("Index");
                return View("Error");
            }
            //assignment assignment = db.assignments.Find(id);
            assignment assignment = db.assignments.SingleOrDefault(a => a.course_id == id);
            if (assignment == null)
            {
                //return HttpNotFound();
                //return RedirectToAction("Index");
                return View("Error");
            }
            return View("Details",assignment);
        }

        [Authorize]
        // GET: assignments/Create
        public ActionResult Create()
        {
           // ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name");
            return View("Create");
        }

        // POST: assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assignment_id,title")] assignment assignment)
        {
            if (ModelState.IsValid)
            {
                //db.assignments.Add(assignment);
                //db.SaveChanges();
                db.Save(assignment);
                return RedirectToAction("Index");

            }


           // ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name", assignment.course_id);
            return View("Create",assignment);
            }
        [Authorize]
        // GET: assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            // assignment assignment = db.assignments.Find(id);
            assignment assignment = db.assignments.SingleOrDefault(a => a.assignment_id == id);
            if (assignment == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
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
                //db.Entry(assignment).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(assignment);
                return RedirectToAction("Index");
            }
            //ViewBag.course_id = new SelectList(db.courses, "course_id", "course_name", assignment.course_id);

            return View("Edit",assignment);
        }

        [Authorize]
        // GET: assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //assignment assignment = db.assignments.Find(id);

            assignment assignment = db.assignments.SingleOrDefault(a => a.assignment_id == id);

            if (assignment == null)
            {
                // return HttpNotFound();
                return View("Error");
            }
            return View("Delete",assignment);
        }

        // POST: assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //assignment assignment = db.assignments.Find(id);
            //db.assignments.Remove(assignment);
            //db.SaveChanges();
            assignment assignment = db.assignments.SingleOrDefault(a => a.assignment_id == id);
            db.Delete(assignment);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
