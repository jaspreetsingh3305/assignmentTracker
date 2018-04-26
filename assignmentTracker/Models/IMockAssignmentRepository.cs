using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignmentTracker.Models
{
    public interface IMockAssignmentRepository
    {
        IQueryable<assignment>assignments { get; }
        void Delete(assignment a);
        assignment Save(assignment assignment);
        
    }
}
