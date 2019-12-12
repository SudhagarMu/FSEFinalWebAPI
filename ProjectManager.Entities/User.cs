using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int EmployeeID { get; set; }

        public virtual IEnumerable<Task> Tasks { get; set; }
        public virtual IEnumerable<Project> Projects { get; set; }

    }
}
