using ProjectManager.Api.Service.CustomFilter;
using ProjectManager.Api.Service.Models;
using ProjectManager.BusinessLayer;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ProjectManager.Api.Service.Controllers
{
    public class ProjectManagerController : ApiController
    {
        ProjectManagerBusiness businessObj = new ProjectManagerBusiness();

        [Route("api/AddUser")]
        [HttpPost]
        [CustomeExceptionFilter]
        public List<User> AddUser(User item)
        {
             return businessObj.AddUser(item);
        }

        [Route("api/DeleteUser/{id}")]
        [HttpDelete]
        [CustomeExceptionFilter]
        public ResponseModel DeleteUser(int id)
        {
            int noOfProject = 0;
            int noOfTask = 0;
            ResponseModel response = new ResponseModel();

            var projects = businessObj.GetAllProject();
            var tasks = businessObj.GetAllTask();
           
            if(projects.Count > 0)
            {
                noOfProject = projects.Where(x => x.UserID == id).Count();
            }
            if(tasks.Count >0)
            {
                noOfTask = tasks.Where(x => x.UserID == id).Count();
            }

            if(noOfProject ==0 && noOfTask ==0)
            {
                response.IsSuccess = true;
                response.UserList =  businessObj.DeleteUser(id);
            }
            else
            {
                response.IsSuccess = false;
            }

            return response;
        }

        [Route("api/UpdateUser")]
        [HttpPut]
        [CustomeExceptionFilter]
        public IHttpActionResult UpdateUser(User item)
        {
           businessObj.UpdateUser(item);

            return Ok("Record Update");
        }

        [Route("api/GetAllUser")]
        [HttpGet]
        [CustomeExceptionFilter]
        public List<User>GetAllUser()
        {
             return businessObj.GetAllUser();
        }

        [Route("api/AddProject")]
        [HttpPost]
        [CustomeExceptionFilter]
        public List<Project> AddProject(Project item)
        {
             return businessObj.AddProject(item);
        }

        [Route("api/DeleteProject/{id}")]
        [HttpDelete]
        [CustomeExceptionFilter]
        public IHttpActionResult DeleteProject(int id)
        {
            var response =  businessObj.DeleteProject(id);

            return Content(HttpStatusCode.OK, response);
        }

        [Route("api/UpdateProject")]
        [HttpPut]
        [CustomeExceptionFilter]
        public IHttpActionResult UpdateProject(Project item)
        {
            businessObj.UpdateProject(item);

            return Content(HttpStatusCode.Accepted, item);
        }

        [Route("api/GetAllProject")]
        [HttpGet]
        [CustomeExceptionFilter]
        public List<Project> GetAllProject()
        {
             return businessObj.GetAllProject();
        }
               
        [Route("api/AddTask")]
        [HttpPost]
        [CustomeExceptionFilter]
        public IHttpActionResult AddTask(Task task)
        {
            task.Status = true;
            businessObj.AddTask(task);
            return Ok();
        }
               
        [HttpPut]
        [Route("api/UpdateTask")]
        [CustomeExceptionFilter]
        public IHttpActionResult UpdateTask(Task task)
        {
            businessObj.UpdateTask(task);

            return Content(HttpStatusCode.Accepted, task);
        }

        [HttpPut]
        [Route("api/EndTask")]
        [CustomeExceptionFilter]
        public IHttpActionResult EndTask(Task task)
        {
            var response = businessObj.EndTask(task.TaskID);
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, response);
        }
               
        [Route("api/DeleteTask/{id}")]
        [HttpDelete]
        [CustomeExceptionFilter]
        public IHttpActionResult DeleteTask(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid order number");

            var response = businessObj.DeleteTask(id);
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, response);
        }
        
        [Route("api/GetAllTask")]
        [CustomeExceptionFilter]
        public List<TaskDetails> GetAllTask()
        {
            return BuildTaskList(businessObj.GetAllTask());
        }
               
        [Route("api/GetTask/{id}")]
        [HttpGet]
        [CustomeExceptionFilter]
        public IHttpActionResult GetById(int id)
        {
            Task task = businessObj.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
               
        [Route("api/GetTaskByProject/{id}")]
        [HttpGet]
        [CustomeExceptionFilter]
        public List<TaskDetails> GetTaskByProjectId(int id)
        {
            List<Task> list = new List<Task>();
            if(id > 0)
            {
                list = businessObj.GetTaskByProjectId(id);
            }
            else
            {
                list =  businessObj.GetAllTask();
            }
            return BuildTaskList(list);
        }

        [Route("api/GetByTaskName/{taskname}")]
        [HttpGet]
        [CustomeExceptionFilter]
        public Task GetByTaskName(string TaskName)
        {   
            return businessObj.GetByTaskName(TaskName);
        }

        [Route("api/GetAllParentTask")]
        [HttpGet]
        [CustomeExceptionFilter]
        public List<ParentTask> GetAllParentTask()
        {  
            return businessObj.GetAllParentTask();
        }

        [Route("api/AddParentTask")]
        [HttpPost]
        [CustomeExceptionFilter]
        public IHttpActionResult AddParentTask(ParentTask parenttask)
        {
            businessObj.AddParentTask(parenttask);
            var list = businessObj.GetAllParentTask();

            return Ok(list);
        }
        private List<TaskDetails> BuildTaskList(List<Task> taskResp)
        {
            List<TaskDetails> tasks = new List<TaskDetails>();
            var parentList = businessObj.GetAllParentTask();
            try
            {
                foreach (var item in taskResp)
                {
                    TaskDetails taskDetail = new TaskDetails();
                    taskDetail.TaskID = item.TaskID;
                    taskDetail.TaskName = item.TaskName;
                    taskDetail.ParentID = item.ParentID;
                    taskDetail.ProjectID = item.ProjectID;
                    if (taskDetail.ParentID != null)
                    {
                        var resp = parentList.FirstOrDefault(x => x.ParentID == item.ParentID);
                        taskDetail.ParentTaskName = resp != null ? resp.ParentTaskName : null;
                    }
                    taskDetail.StartDate = item.StartDate;
                    taskDetail.EndDate = item.EndDate;
                    taskDetail.Priority = item.Priority;
                    taskDetail.Status = item.Status;
                    taskDetail.UserID = item.UserID;

                    tasks.Add(taskDetail);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }

            return tasks;
        }
    }
}
