namespace assignmentTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("courses")]
    public partial class cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cours()
        {
            assignments = new HashSet<assignment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int course_id { get; set; }

        [Required]
        [StringLength(30)]
        [Display (Name = "Course Name")]
        public string course_name { get; set; }

        [StringLength(10)]
        [Display(Name = "Course Code")]
        public string course_code { get; set; } 

        [StringLength(50)]
        [Display(Name = "Faculty")]
        public string teacher_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignment> assignments { get; set; }
    }
}
