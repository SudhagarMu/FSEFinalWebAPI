using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;


namespace ProjectManager.DataLayer
{
    public class ProjectContext:DbContext
    {
        public ProjectContext():base("name = DBConnString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ParentTask> ParentTasks { get; set; }
    }
}
