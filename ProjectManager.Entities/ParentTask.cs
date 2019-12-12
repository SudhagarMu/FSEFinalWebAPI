using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    [Table("ParentTasks")]
    public class ParentTask
    {
        [Key]
        public int ParentID { get; set; }
        [Required]
        [Column("ParentTask")]
        public string ParentTaskName { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
