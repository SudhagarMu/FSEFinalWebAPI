using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Service.Models
{
    public class TaskDetails
    {
        public int TaskID { get; set; }
        public int? ParentID { get; set; }
        public string ParentTaskName { get; set; }
        public int ProjectID { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public int UserID { get; set; }
    }
}