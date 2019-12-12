using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Service.Models
{
    public class ResponseModel
    {
        public bool HasError { get; set; }
        public bool IsSuccess { get; set; }
        public List<User> UserList { get; set; }
    }
}