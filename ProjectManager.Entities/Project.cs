using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public  IEnumerable<Task> Tasks { get; set; }
       // public string UserName { get; set; }
    }
}
