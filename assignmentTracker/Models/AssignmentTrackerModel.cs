namespace assignmentTracker.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AssignmentTrackerModel : DbContext
    {
        public AssignmentTrackerModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<assignment> assignments { get; set; }
        public virtual DbSet<cours> courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<assignment>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<assignment>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<cours>()
                .Property(e => e.course_name)
                .IsUnicode(false);

            modelBuilder.Entity<cours>()
                .Property(e => e.course_code)
                .IsFixedLength();

            modelBuilder.Entity<cours>()
                .Property(e => e.teacher_name)
                .IsUnicode(false);
        }
    }
}
