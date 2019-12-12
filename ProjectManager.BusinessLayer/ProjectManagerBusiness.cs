using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ProjectManager.DataLayer;
using ProjectManager.Entities;

namespace ProjectManager.BusinessLayer
{
    public class ProjectManagerBusiness
    {
        public void AddParentTask(ParentTask item)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                dbcontext.ParentTasks.Add(item);
                dbcontext.SaveChanges();
            }
        }
        public List<ParentTask> GetAllParentTask()
        {

            using (ProjectContext dbcontext = new ProjectContext())
            {

                return dbcontext.ParentTasks.ToList();
            }
        }
        public List<User> AddUser(User item)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                dbcontext.Users.Add(item);
                dbcontext.SaveChanges();

                return dbcontext.Users.ToList();
            }
        }

        public void UpdateUser(User item)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                var context = dbcontext.Users.SingleOrDefault(x => x.UserID == item.UserID);
                //context.UserID = item.UserID;
                context.EmployeeID = item.EmployeeID;
                context.FirstName = item.FirstName;
                context.LastName = item.LastName;

                dbcontext.SaveChanges();
            }
        }

        public List<User> DeleteUser(int id)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                // dbcontext.Database.ExecuteSqlCommand("delete from Task where ParentId ={0}", id);

                var task = dbcontext.Users
                                    .Where(x => x.UserID == id)
                                    .FirstOrDefault();

                dbcontext.Entry(task).State = EntityState.Deleted;
                dbcontext.SaveChanges();

                return dbcontext.Users.ToList();
            }
        }

        public List<User> GetAllUser()
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                return dbcontext.Users.ToList();
            }
        }


        public List<Project> AddProject(Project item)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                dbcontext.Projects.Add(item);
                dbcontext.SaveChanges();

                return dbcontext.Projects.ToList();
            }
        }

        public List<Project> DeleteProject(int id)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                dbcontext.Database.ExecuteSqlCommand("delete from Tasks where ProjectID ={0}", id);

                var task = dbcontext.Projects
                                    .Where(x => x.ProjectID == id)
                                    .FirstOrDefault();

                dbcontext.Entry(task).State = EntityState.Deleted;
                dbcontext.SaveChanges();

                return dbcontext.Projects.ToList();
            }
        }
        public void UpdateProject(Project item)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                var context = dbcontext.Projects.SingleOrDefault(x => x.ProjectID == item.ProjectID);
                context.ProjectName = item.ProjectName;
                context.StartDate = item.StartDate;
                context.EndDate = item.EndDate;
                context.Priority = item.Priority;
                context.UserID = item.UserID;

                dbcontext.SaveChanges();
            }
        }

        public List<Project> GetAllProject()
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                //return dbcontext.Projects.ToList();
                //var res = from prj in dbcontext.Projects                          
                //          select new
                //          {
                //              prj. = cust.name,
                //              oId = d.order_id == null ? -1 : d.order_id
                //          };
                //var query = from p in dbcontext.Projects
                //            select new
                //            {
                //                ProjectID = p.ProjectID,
                //                ProjectName = p.ProjectName,
                //                StartDate = p.StartDate,
                //                EndDate = p.EndDate,
                //                Priority = p.Priority,
                //                UserID = p.UserID,
                //                UserName = (from usr in dbcontext.Users
                //                            where usr.UserID == p.UserID
                //                            select usr.FirstName)
                //            };
                //var query1 = query;
                //return query.ToList();

                return dbcontext.Projects.ToList();
            }
        }


        public void AddTask(Task task)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                dbcontext.Tasks.Add(task);
                dbcontext.SaveChanges();
            }
        }

        public void UpdateTask(Task task)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                var context = dbcontext.Tasks.SingleOrDefault(x => x.TaskID == task.TaskID);
                context.ParentID = task.ParentID;
                context.StartDate = task.StartDate;
                context.EndDate = task.EndDate;
                context.Priority = task.Priority;

                dbcontext.SaveChanges();
            }
        }

        public List<Task> EndTask(int taskId)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                var context = dbcontext.Tasks.Find(taskId);
                context.EndDate = DateTime.Now;
                context.Status = false;

                dbcontext.SaveChanges();

                return dbcontext.Tasks.ToList();
            }
        }

        public List<Task> DeleteTask(int id)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
               // dbcontext.Database.ExecuteSqlCommand("delete from Task where ParentId ={0}", id);

                var task = dbcontext.Tasks
                                    .Where(x => x.TaskID == id)
                                    .FirstOrDefault();

                dbcontext.Entry(task).State = EntityState.Deleted;
                dbcontext.SaveChanges();

                return dbcontext.Tasks.ToList();
            }

        }

        public Task GetTaskById(int taskId)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                return dbcontext.Tasks.Find(taskId);
            }
        }
        public List<Task> GetTaskByProjectId(int id)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                return dbcontext.Tasks.Where(x =>x.ProjectID == id ).ToList();
            }
        }

        public Task GetByTaskName(string taskName)
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                return dbcontext.Tasks.SingleOrDefault(x => x.TaskName.ToUpper() == taskName.ToUpper());
            }
        }
        public List<Task> GetAllTask()
        {
            using (ProjectContext dbcontext = new ProjectContext())
            {
                return dbcontext.Tasks.ToList();
            }
        }

       
    }
}
