using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assignmentTracker.Models
{
    public class EFAssignmentRepository : IMockAssignmentRepository
    {
        private AssignmentTrackerModel db = new AssignmentTrackerModel();
        public IQueryable<assignment> assignments => db.assignments;

        public void Delete(assignment a)
        {
            throw new NotImplementedException();
        }

        public assignment Save(assignment assignment)
        {
            if (assignment.assignment_id == null)
            {
                db.assignments.Add(assignment);
            }
            else
            {
                db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return assignment;
        }
    }
}