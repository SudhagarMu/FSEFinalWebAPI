using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBench;
using ProjectManager.BusinessLayer;
using ProjectManager.Entities;

namespace ProjectManager.PerformanceTest
{
    public class TestPerformance
    {
        ProjectManagerBusiness obj = null;
        public TestPerformance()
        {
            obj = new ProjectManagerBusiness();
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                       NumberOfIterations = 1, RunMode = RunMode.Throughput,RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.LessThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkAddUpdate()
        {
            Task task = new Task();
            task.TaskName = "Test444";
            task.ParentID = 5;
            task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now;
            task.ProjectID = 1;
            task.Status = true;
            task.Priority = 12;
            task.UserID = 3;
            
            obj.AddTask(task);
            obj.UpdateTask(task);
           
        }
        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                       NumberOfIterations = 1, RunMode = RunMode.Throughput,RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.LessThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkAddUpdateUser()
        {
            User user = new User();
            user.FirstName = "PerFirstName";
            user.LastName = "PerLastName";
            user.EmployeeID = 6001;
            
            obj.AddUser(user);
            obj.UpdateUser(user);
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                      NumberOfIterations = 1, RunMode = RunMode.Throughput, RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.LessThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkAddUpdateProject()
        {
            Project project = new Project();
            project.ProjectName = "PerProjectName";
            project.Priority = 12;
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now;
            project.UserID = 3;
                        
            obj.AddProject(project);
            obj.UpdateProject(project);
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                       NumberOfIterations = 1, RunMode = RunMode.Throughput, RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.LessThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkAddParentTask()
        {
            ParentTask ptask = new ParentTask();
            ptask.ParentTaskName = "PerTask";
           
            obj.AddParentTask(ptask);
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                       NumberOfIterations = 1, RunMode = RunMode.Throughput,RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkView()
        {  
            obj.GetAllTask();
            obj.GetAllUser();
            obj.GetAllParentTask();
            obj.GetAllProject();
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                       NumberOfIterations = 1, RunMode = RunMode.Throughput,RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkGetByTaskId()
        {   
            obj.GetTaskById(1);
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
                      NumberOfIterations = 1, RunMode = RunMode.Throughput, RunTimeMilliseconds = 15, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 70000000)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 4.0d)]
        public void BenchmarkEndTask()
        {
            obj.EndTask(15);
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
