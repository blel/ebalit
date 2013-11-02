using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class WorkingReportBll:DataAccessLayer
    {
        /// <summary>
        /// Returns a list of all projects
        /// </summary>
        /// <returns></returns>
        public IList<ProjectProject> GetProjects()
        {
            return EbalitDBContext.ProjectProjects.ToList();
        }

        public ProjectProject GetProject(int id)
        {
            return EbalitDBContext.ProjectProjects.SingleOrDefault(cc => cc.Id == id);
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
        public IList<ProjectTask> GetTasks(ProjectProject project)
        {
            return EbalitDBContext.ProjectTasks.Where(cc => cc.ProjectProject.Id == project.Id).ToList();
        }

        /// <summary>
        /// Returns the working report with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectWorkingReport GetWorkingReport(int id)
        {
            return EbalitDBContext.ProjectWorkingReports.SingleOrDefault(cc => cc.Id == id);
        }

        /// <summary>
        /// returns all working reports
        /// </summary>
        /// <returns></returns>
        public IList<ProjectWorkingReport> GetWorkingReports()
        {
            return EbalitDBContext.ProjectWorkingReports.ToList();
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
                reportToUpdate = workingReport;
            }
            
            EbalitDBContext.SaveChanges();
            UpdateActualWork((int)workingReport.TaskId);
            return reportToUpdate;
        }

        /// <summary>
        /// Delete working report
        /// </summary>
        /// <param name="workingReport"></param>
        public void DeleteWorkingReport(ProjectWorkingReport workingReport)
        {
            var reportToDelete = GetWorkingReport(workingReport.Id);
            EbalitDBContext.ProjectWorkingReports.Remove(reportToDelete);
            
            EbalitDBContext.SaveChanges();
            UpdateActualWork((int)workingReport.TaskId);
        }

        /// <summary>
        /// Create new working report
        /// TODO: validation
        /// </summary>
        /// <param name="workingReport"></param>
        public void CreateWorkingReport(ProjectWorkingReport workingReport)
        {
            EbalitDBContext.ProjectWorkingReports.Add(workingReport);
            EbalitDBContext.SaveChanges();
            UpdateActualWork((int)workingReport.TaskId);

            
        }

        /// <summary>
        /// Calculates the actual work based on all working reports booked on the given task.
        /// </summary>
        /// <param name="task"></param>
        private void UpdateActualWork(int taskId)
        {
            var calculatedWork = (from cc in EbalitDBContext.ProjectWorkingReports
                where cc.TaskId == taskId
                select EntityFunctions.DiffMinutes( cc.From , cc.To)).Sum();
            var task = EbalitDBContext.ProjectTasks.SingleOrDefault(cc => cc.Id == taskId);
            if (task!=null)
            { task.ActualWork = calculatedWork; }
            EbalitDBContext.SaveChanges();


        }
    }
}