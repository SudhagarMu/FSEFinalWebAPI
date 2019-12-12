using System;
using System.Collections.Generic;
using System.Web.Http;
using NUnit.Framework;
using ProjectManager.Api.Service.Controllers;
using ProjectManager.Entities;
using System.Linq;
using System.Web.Http.Results;
using System.Net;
using ProjectManager.Api.Service.Models;

namespace ProjectManager.Api.Tests
{
    [TestFixture]
    public class TestWebApi 
    {
        ProjectManagerController projectObj = null;
        int taskId = 0;
        int parentCount = 0;
        int userId = 0;
        int projectId = 0;

        [SetUp]
        public void Setup()
        {
            projectObj = new ProjectManagerController();
        }

        [Test]
        [Order(1)]
        public void TestWebAPIParentTaskGetAll()
        {  
            //Act
            int actual = projectObj.GetAllParentTask().Count;
            parentCount = actual;

            //Assert
            Assert.Greater(actual, 0);
        }

        [Test]
        [Order(2)]
        public void TestWebAPIAddParentTask()
        {
            // Arrange
            ParentTask item = new ParentTask();
            item.ParentTaskName = "ParentTaskTestWebAPI"+ parentCount;
            //Act
            projectObj.AddParentTask(item);
            
            //Assert
            int actual = projectObj.GetAllParentTask().Count;
            Assert.Greater(actual, parentCount);
        }

        [Test]
        [Order(3)]
        public void TestWebApiAddUser()
        {
            // Arrange
            User item = new User();
            item.FirstName = "Anand";
            item.LastName = "vardhan";
            item.EmployeeID = 5001;

            //Act
            var list = projectObj.AddUser(item);
            userId = list.FirstOrDefault(x => x.EmployeeID == 5001).UserID;

            //Assert
            Assert.AreNotEqual(userId, 0);
           // Assert.IsInstanceOf(typeof(OkResult), actionResult);
        }

        [Test]
        [Order(4)]
        public void TestWebApiGetAllUser()
        {
            List<User> users = projectObj.GetAllUser();
            Assert.Greater(users.Count(), 0);
        }

        [Test]
        [Order(5)]
        public void TestWebApiUpdateUser()
        {
            // Arrange
            User item = new User();
            item.UserID = userId;
            item.FirstName = "Anand";
            item.LastName = "Sharma";
            item.EmployeeID = 5001;
            //Act
            IHttpActionResult actionResult =  projectObj.UpdateUser(item);
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [Test]
        [Order(18)]
        public void TestWebApiDeleteUser()
        {
            var response = projectObj.DeleteUser(userId);
            Assert.AreEqual(true, response.IsSuccess);
        }

        [Test]
        [Order(6)]
        public void TestWebApiAddProject()
        {
            // Arrange
            Project item = new Project();
            item.ProjectName = "TaskApiTest1";
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(2);
            item.Priority = 12;
            item.UserID = userId;

            //Act
            var list = projectObj.AddProject(item);
            var projectDetail = list.FirstOrDefault(x => x.ProjectName.ToLower() == item.ProjectName.ToLower());
            projectId = projectDetail.ProjectID;
                   
            //Assert
            Assert.AreNotEqual(projectId, 0);
        }
                
        [Test]
        [Order(7)]
        public void TestWebApiUpdateProject()
        {
            // Arrange
            Project item = new Project();
            item.ProjectName = "TaskApiTest1";
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(4);
            item.Priority = 15;
            item.UserID = 1;
            item.ProjectID = projectId;

            //Act
            IHttpActionResult actionResult = projectObj.UpdateProject(item);
            var contentResult = actionResult as NegotiatedContentResult<Project>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(15, contentResult.Content.Priority);
        }

        [Test]
        [Order(8)]
        public void TestGetAllProject()
        {
            int actual = projectObj.GetAllProject().Count;
            Assert.Greater(actual, 0);
        }

        [Test]
        [Order(17)]
        public void TestWebApiDeleteProject()
        {
            //Act
            IHttpActionResult actionResult = projectObj.DeleteProject(projectId);
            var contentResult = actionResult as NegotiatedContentResult<List<Project>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [Test]
        [Order(9)]
        public void TestWebApiAddTask()
        {
            // Arrange
            Task item = new Task();
            item.TaskName = "TaskApiTest1";
            item.ProjectID = projectId;
            item.ParentID = 1;
            item.Priority = 10;
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now;
            item.Status = true;
            item.UserID = userId;
                       
            //Act
            IHttpActionResult actionResult = projectObj.AddTask(item);
            Task test = projectObj.GetByTaskName("TaskApiTest1");
            taskId = test.TaskID;

            //Assert
            Assert.IsInstanceOf(typeof(OkResult), actionResult);
            // Assert.AreEqual("TaskTest2", test.TaskName);
        }

        [Test]
        [Order(10)]
        public void TestWebApiGetAllTask()
        {
            List<TaskDetails> tasks = projectObj.GetAllTask();
            Assert.Greater(tasks.Count(), 0);
        }

        [Test]
        [Order(11)]
        public void TestWebAPIGetByTaskId()
        {
            IHttpActionResult actionResult = projectObj.GetById(taskId);
            var contentResult = actionResult as OkNegotiatedContentResult<Task>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(taskId, contentResult.Content.TaskID);
        }

        [Test]
        [Order(12)]
        public void TestGetByIdReturnsNotFound()
        {
            // Act
            IHttpActionResult actionResult = projectObj.GetById(10005);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), actionResult);
        }
        [Test]
        [Order(13)]
        public void TestWebApiUpdateTask()
        {
            Task item = new Task();
            item.TaskID = taskId;
            item.TaskName = "TaskApiTest1";
            item.Priority = 15;
            item.ParentID = 1;
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(5);
            item.Status = true;
            
            IHttpActionResult actionResult = projectObj.UpdateTask(item);
            var contentResult = actionResult as NegotiatedContentResult<Task>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(item.Priority, contentResult.Content.Priority);
        }

        [Test]
        [Order(14)]
        public void TestWebApiEndTask()
        {
            // Arrange
            Task item = new Task();
            item.TaskID = taskId;
            item.TaskName = "TaskApiTest1";
            item.Priority = 10;
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now;
            item.Status = false;

            //Act
            IHttpActionResult actionResult = projectObj.EndTask(item);
            var contentResult = actionResult as NegotiatedContentResult<List<Task>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }
        [Test]
        [Order(15)]
        public void TestWebApiDeleteTask()
        {
            //Act
            IHttpActionResult actionResult = projectObj.DeleteTask(taskId);
            var contentResult = actionResult as NegotiatedContentResult<List<Task>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }
        [Test]
        [Order(16)]
        public void TestDeleteBadRequest()
        {
            //Act
            IHttpActionResult actionResult = projectObj.DeleteTask(0);

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), actionResult);
        }
        //protected virtual void Dispose(bool disposing)
        //{
        //    Dispose();
        //}
        //public void Dispose()
        //{
        //    // Dispose of unmanaged resources.
        //    Dispose(true);
        //    // Suppress finalization.
        //    GC.SuppressFinalize(this);
        //}
    }
}
