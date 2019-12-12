using System;
using NUnit.Framework;
using ProjectManager.BusinessLayer;
using ProjectManager.Entities;
using System.Linq;

namespace ProjectManager.BusinessLayer.Test
{
    [TestFixture]
    public class TestBusiness
    {
        ProjectManagerBusiness businessObj = null;
        int taskId = 0;
        int parentCount = 0;
        int userId = 0;
        int projectId = 0;

        [SetUp]
        public void Setup()
        {
            businessObj = new ProjectManagerBusiness();
        }

        [Test]
        [Order(1)]
        public void TestAddParentTask()
        {  
            ParentTask item = new ParentTask();
            item.ParentTaskName = "SBA Project Test";
            businessObj.AddParentTask(item);

            int actual = businessObj.GetAllParentTask().Count;
            parentCount = actual;
            Assert.Greater(actual, 0);
        }

        [Test]
        [Order(2)]
        public void TestGetAllParentTask()
        {   
            int actual = businessObj.GetAllParentTask().Count;
            Assert.AreEqual(parentCount, actual);
           
        }

        [Test]
        [Order(3)]
        public void TestGetAllUser()
        {  
            int actual = businessObj.GetAllUser().Count;
            Assert.Greater(actual, 0);
        }

        [Test]
        [Order(4)]
        public void TestAddUser()
        {  
            User item = new User();
            item.FirstName = "Sudhesh";
            item.LastName = "vardhan";
            item.EmployeeID = 4001;
           var list =  businessObj.AddUser(item);

            userId = list.FirstOrDefault(x => x.EmployeeID == 4001).UserID;

            Assert.AreNotEqual(userId, 0);
        }

        [Test]
        [Order(5)]
        public void TestUpdateUser()
        {
            User item = new User();
            item.UserID = userId;
            item.FirstName = "Sudhesh";
            item.LastName = "Sharma";
            item.EmployeeID = 4001;
            businessObj.UpdateUser(item);

            string lastName = businessObj.GetAllUser().FirstOrDefault(x => x.UserID == userId).LastName;

            Assert.AreEqual(item.LastName, lastName);
        }

        //[Test]
        //[Order(17)]
        public void TestDeleteUser()
        {
            userId = 1008;
            businessObj.DeleteUser(userId);

            User item = businessObj.GetAllUser().FirstOrDefault(x => x.UserID == userId);
            Assert.AreEqual(null, item);
        }

        [Test]
        [Order(6)]
        public void TestAddProject()
        {
            Project item = new Project();
            item.ProjectName = "ProjectManager App Test Project";
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(2);
            item.Priority = 12;
            item.UserID = userId;
            var list = businessObj.AddProject(item);

            var projectDetail = list.FirstOrDefault(x =>x.ProjectName.ToLower() == item.ProjectName.ToLower());
            projectId = projectDetail.ProjectID;

            Assert.AreNotEqual(projectId, 0);
           // Assert.AreEqual(item.Priority, projectDetail.Priority);
        }

        [Test]
        [Order(7)]
        public void TestGetAllProject()
        {  
            int actual = businessObj.GetAllProject().Count;
            Assert.Greater(actual, 0);
        }
        
        [Test]
        [Order(8)]
        public void TestUpdateProject()
        {   
            Project item = new Project();
            item.ProjectName = "ProjectManager App Test Project";
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(4);
            item.Priority = 15;
            item.UserID = 1;
            item.ProjectID = projectId;
            businessObj.UpdateProject(item);

            var projectDetails = businessObj.GetAllProject().FirstOrDefault(x =>x.ProjectID == projectId);
            int actual = projectDetails.Priority;

            Assert.AreEqual(item.Priority,actual);
        }
        [Test]
        [Order(16)]
        public void TestDeleteProject()
        {
            businessObj.DeleteProject(projectId);

            Project item = businessObj.GetAllProject().FirstOrDefault(x => x.ProjectID == projectId);
            Assert.AreEqual(null, item);
        }

        [Test]
        [Order(9)]
        public void TestGetAllTask()
        {
            int actual = businessObj.GetAllTask().Count;
            Assert.Greater(actual, 0);
        }
        //[Test]
        //[Order(10)]
        public void TestAddTask()
        {
            Task item = new Task();
            item.ProjectID = 1003;
            item.TaskName = "TestInfra Management";
            item.ParentID = 1;
            item.Priority = 70;
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now;
            item.Status = true;
            item.UserID = 1;
            businessObj.AddTask(item);

            Task test = businessObj.GetByTaskName(item.TaskName);
            taskId = test.TaskID;
            Assert.AreEqual(item.TaskName, test.TaskName);
        }
        [Test]
        [Order(11)]
        public void TestGetByTaskId()
        {
            taskId = 4;
            Task item = businessObj.GetTaskById(taskId);
            Assert.AreEqual(taskId, item.TaskID);
        }

        [Test]
        [Order(12)]
        public void TestGetTaskByProjectId()
        {
            var item = businessObj.GetTaskByProjectId(1);

            Assert.Greater(item.Count, 0);
        }

        [Test]
        [Order(13)]
        public void TestUpdateTask()
        {
            taskId = 2;
            Task item = new Task();
            item.TaskID = taskId;
            item.ParentID = 1;
            item.TaskName = "TestTask1000";
            item.Priority = 40;
            item.StartDate = DateTime.Now;
            item.EndDate = DateTime.Now.AddDays(2);
            item.Status = true;

            businessObj.UpdateTask(item);
            Task itemafterupdate = businessObj.GetByTaskName("TestTask1000");
            if(itemafterupdate != null)
                Assert.AreEqual(item.Priority, itemafterupdate.Priority);
        }

        //[Test]
        //[Order(14)]
        public void TestEndTask()
        {
            taskId = 2;
            businessObj.EndTask(taskId);

            Task itemafterupdate = businessObj.GetTaskById(taskId);
            Assert.AreEqual(false, itemafterupdate.Status);
        }
        //[Test]
        //[Order(15)]
        public void TestDeleteTask()
        {
            taskId = 3;
            businessObj.DeleteTask(taskId);

            Task item = businessObj.GetTaskById(taskId);
            Assert.AreEqual(null, item);
        }

    }
}
