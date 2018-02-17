namespace assignmentTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class assignment
    {
        [Key]
        public int assignment_id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Due Date")]
        public DateTime due_date { get; set; }

        [Display (Name ="Course Name")]
        public int? course_id { get; set; }

        public virtual cours cours { get; set; }
    }
}
