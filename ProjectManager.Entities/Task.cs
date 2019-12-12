using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int TaskID { get; set; }
        public int? ParentID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("ParentID")]
        public  ParentTask ParentTask { get; set; }

        [ForeignKey("ProjectID")]
        public  Project Project { get; set; }
        [ForeignKey("UserID")]
        public  User User { get; set; }

    }
}
