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
        /// returns a list of all resources assigned to the project wiht id = projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IList<ProjectResource> GetResources(int projectId)
        {
            return EbalitDBContext.ProjectResources.Where(cc => cc.ProjectId == projectId && !cc.IsDeleted).ToList();
        }


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
        public IList<ProjectTask> GetTasks(ProjectProject project=null)
        {
            return project == null ? EbalitDBContext.ProjectTasks.ToList() :
                EbalitDBContext.ProjectTasks.Where(cc => cc.ProjectProject.Id == project.Id && !cc.IsDeleted).ToList();
        }

        /// <summary>
        /// Returns a list of all tasks of the project.
        /// The IsDeleted Flag of the tasks must be "false"
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <returns></returns>
        public IList<ProjectTask> GetTasks(int projectId)
        {
            return EbalitDBContext.ProjectTasks.Where(cc => (cc.ProjectId == projectId && !cc.IsDeleted)).ToList();
        }


        /// <summary>
        /// Returns the id of the task with given guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int GetTaskIdByGuid(Guid guid)
        {
            return EbalitDBContext.ProjectTasks.Single(cc => cc.Guid == guid).Id;
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
            return EbalitDBContext.ProjectWorkingReports.Include("ProjectProject").Include("ProjectTask").ToList();
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


        public bool IsWorkingReportUpdateable(int workingReportId)
        {
            var workingReport = EbalitDBContext.ProjectWorkingReports.Single(cc => cc.Id == workingReportId);
            if (workingReport != null)
            {
                //todo probably needs to be reviewed if resource to task assignment is limited to
                //resources which are assigned to the task in ms project
                return !(workingReport.ProjectTask.IsDeleted ||
                  workingReport.ProjectResource.IsDeleted);
            }
            return false;
        }
    }
}