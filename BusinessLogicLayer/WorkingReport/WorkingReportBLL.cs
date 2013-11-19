using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using System.Web.Security;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class WorkingReportBll:DataAccessLayer
    {
        /// <summary>
        /// returns a list of all resources assigned to the project wiht id = projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IList<ProjectResource> GetResources(int projectId)
        {
            return EbalitDbContext.ProjectResources.Where(cc => cc.ProjectId == projectId && !cc.IsDeleted).ToList();
        }


        /// <summary>
        /// Returns a list of all projects
        /// </summary>
        /// <returns></returns>
        public IList<ProjectProject> GetProjects()
        {
            return EbalitDbContext.ProjectProjects.ToList();
        }

        public ProjectProject GetProject(int id)
        {
            return EbalitDbContext.ProjectProjects.SingleOrDefault(cc => cc.Id == id);
        }

        /// <summary>
        /// returns a list of all projects of the given memberhsipt user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<ProjectProject> GetProjects(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a list of all tasks of the project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public IList<ProjectTask> GetTasks(ProjectProject project=null)
        {
            return project == null ? EbalitDbContext.ProjectTasks.ToList() :
                EbalitDbContext.ProjectTasks.Where(cc => cc.ProjectProject.Id == project.Id && !cc.IsDeleted).ToList();
        }

        /// <summary>
        /// Returns a list of all tasks of the project.
        /// The IsDeleted Flag of the tasks must be "false"
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <returns></returns>
        public IList<ProjectTask> GetTasks(int projectId)
        {
            return EbalitDbContext.ProjectTasks.Where(cc => (cc.ProjectId == projectId && !cc.IsDeleted)).ToList();
        }


        /// <summary>
        /// Returns the id of the task with given guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int GetTaskIdByGuid(Guid guid)
        {
            return EbalitDbContext.ProjectTasks.Single(cc => cc.Guid == guid).Id;
        }


        /// <summary>
        /// Returns the working report with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectWorkingReport GetWorkingReport(int id)
        {
            return EbalitDbContext.ProjectWorkingReports.
                SingleOrDefault(cc => cc.Id == id);
        }

        /// <summary>
        /// returns all working reports
        /// </summary>
        /// <returns></returns>
        public IList<ProjectWorkingReport> GetWorkingReports()
        {
            return EbalitDbContext.ProjectWorkingReports.
                Include("ProjectProject").
                Include("ProjectTask").
                Include("ProjectResource").ToList();
        }

        /// <summary>
        /// Updates a given working report
        /// TODO: Check if this really works.
        /// </summary>
        /// <param name="workingReport"></param>
        /// <returns></returns>
        public ProjectWorkingReport UpdateWorkingReport(ProjectWorkingReport workingReport)
        {
            var reportToUpdate = GetWorkingReport(workingReport.Id);
            if (reportToUpdate != null)
            {
                reportToUpdate.Notes = workingReport.Notes;
                reportToUpdate.ResourceId = workingReport.ResourceId;
                reportToUpdate.TaskId = workingReport.TaskId;
                reportToUpdate.To = workingReport.To;
                reportToUpdate.From = workingReport.From;
                reportToUpdate.Total = workingReport.Total;
            }
            
            EbalitDbContext.SaveChanges();
            UpdateActualWork((int)workingReport.TaskId);
            return reportToUpdate;
        }

        /// <summary>
        /// Delete working report
        /// TODO: had to remove transaction scope due to sql2005
        /// see: http://pieterderycke.wordpress.com/2012/01/22/transactionscope-transaction-escalation-behavior/ for a solution
        /// </summary>
        /// <param name="workingReport"></param>
        public void DeleteWorkingReport(ProjectWorkingReport workingReport)
        {
            var reportToDelete = GetWorkingReport(workingReport.Id);

            //using (var transaction = new TransactionScope())
            //{
            //    try
            //    {
                    var taskId = Convert.ToInt32(reportToDelete.TaskId);

                    EbalitDbContext.ProjectWorkingReports.Remove(reportToDelete);

                    EbalitDbContext.SaveChanges();

                    UpdateActualWork(taskId);

                //    transaction.Complete();
                //}
                //catch (InvalidOperationException)
                //{
                //    //todo: exception handling
                //}
            //}

        }

        /// <summary>
        /// Create new working report
        /// TODO: validation
        /// TODO: uncommented transactions as not working on ebalit.
        /// </summary>
        /// <param name="workingReport"></param>
        public void CreateWorkingReport(ProjectWorkingReport workingReport)
        {
            //using (var transaction = new TransactionScope())
            //{
            //    try
            //    {
                    EbalitDbContext.ProjectWorkingReports.Add(workingReport);
                 
                    EbalitDbContext.SaveChanges();
                    
                    UpdateActualWork(Convert.ToInt32(workingReport.TaskId));

                //    transaction.Complete();
                //}
                //catch (InvalidOperationException)
                //{
                //    //todo: exception handling
                //}
            //}
        }

        /// <summary>
        /// Calculates the actual work based on all working reports booked on the given task.
        /// The total is important, not the difference between to and from!
        /// </summary>
        /// <param name="task"></param>
        private void UpdateActualWork(int taskId)
        {
            //must be * 60 since project interprets digits as minutes.
            var calculatedWork = (from cc in EbalitDbContext.ProjectWorkingReports
                where cc.TaskId == taskId
                select cc.Total).Sum() * 60;
            var task = EbalitDbContext.ProjectTasks.SingleOrDefault(cc => cc.Id == taskId);
            if (task!=null)
            { task.ActualWork = Convert.ToDouble(calculatedWork); }
            EbalitDbContext.SaveChanges();
        }


        public bool IsWorkingReportUpdateable(int workingReportId)
        {
            var workingReport = EbalitDbContext.ProjectWorkingReports.Single(cc => cc.Id == workingReportId);
            if (workingReport != null)
            {
                //todo probably needs to be reviewed if resource to task assignment is limited to
                //resources which are assigned to the task in ms project
                return !(workingReport.ProjectTask.IsDeleted ||
                  workingReport.ProjectResource.IsDeleted);
            }
            return false;
        }

        public IEnumerable<ProjectResource> GetAvailableResources(int projectId)
        {
            var project = EbalitDbContext.ProjectProjects.Include("ProjectResources").SingleOrDefault(cc => cc.Id == projectId);
            if (project != null)
            {
                return project.ProjectResources.
                    Except(
                        EbalitDbContext.ProjectUserAssignments.Include("ProjectResources")
                            .Select(cc => cc.ProjectResource)).ToList();
            }
            return null;
        }

        public string GetTaskPath(string taskGuid)
        {
            var taskRealGuid = Guid.Parse(taskGuid);
            var task = EbalitDbContext.ProjectTasks.Include("ProjectProject").Single(cc => cc.Guid == taskRealGuid);
            if (task.Parent == task.ProjectProject.Guid)
            {
                return task.Name;
            }
            return GetTaskPath(task.Parent.ToString()) + "/"+ task.Name;
        }

    }
}