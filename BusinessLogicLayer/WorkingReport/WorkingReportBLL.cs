using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer.CsvFileImport;
using EbalitWebForms.BusinessLogicLayer.DTO;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer.WorkingReport
{
    public class WorkingReportBll : DataAccessLayer
    {
        /// <summary>
        /// returns a list of all resources assigned to the project wiht id = projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IList<ProjectResource> GetResources(int projectId)
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                if (projectId > 0)
                {
                    return EbalitDbContext.ProjectResources.
                        Where(cc => cc.ProjectId == projectId && !cc.IsDeleted).
                        Intersect(EbalitDbContext.ProjectUserAssignments.
                            Where(cc => cc.MembershipUserName == membershipUser.UserName).
                            Select(cc => cc.ProjectResource)).ToList();
                }
            }

            return null;

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
        public IList<ProjectTask> GetTasks(ProjectProject project = null)
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
        public IList<ProjectWorkingReport> GetWorkingReports(WorkingReportFindDto findDto = null)
        {
            var resultList =
            EbalitDbContext.ProjectWorkingReports.
                Include("ProjectProject").
                Include("ProjectTask").
                Include("ProjectResource").AsQueryable();

            if (findDto != null)
            {
                resultList = resultList.
                    ConditionalWhere(cc=> cc.From >= findDto.From, () => findDto.From !=null).
                    ConditionalWhere(cc=> cc.To <= findDto.To, () => findDto.To != null).
                    ConditionalWhere(cc=> cc.ProjectId == findDto.ProjectId, () => findDto.ProjectId != null && findDto.ProjectId !=0).
                    ConditionalWhere(cc=> cc.ResourceId == findDto.ResourceId, () => findDto.ResourceId !=null && findDto.ResourceId !=0).
                    ConditionalWhere(cc=> cc.ProjectTask.Guid == Guid.Parse(findDto.TaskGuid), ()=> !string.IsNullOrWhiteSpace(findDto.TaskGuid));
                //todo search for child tasks


        
            }
            return resultList.ToList();
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
            if (workingReport.TaskId != null) UpdateActualWork((int)workingReport.TaskId);
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
        /// <param name="taskId"></param>
        private void UpdateActualWork(int taskId)
        {
            //must be * 60 since project interprets digits as minutes.
            var calculatedWork = (from cc in EbalitDbContext.ProjectWorkingReports
                                  where cc.TaskId == taskId
                                  select cc.Total).Sum() * 60;
            var task = EbalitDbContext.ProjectTasks.SingleOrDefault(cc => cc.Id == taskId);
            if (task != null)
            { task.ActualWork = Convert.ToDouble(calculatedWork); }
            EbalitDbContext.SaveChanges();
        }

        /// <summary>
        /// Checks whether the current working report can be updated
        /// </summary>
        /// <param name="workingReportId"></param>
        /// <returns></returns>
        public bool IsWorkingReportUpdateable(int workingReportId)
        {
            var workingReport = EbalitDbContext.ProjectWorkingReports.Single(cc => cc.Id == workingReportId);
            if (workingReport == null) return false;

            //check whether the current user contains the resource the current working report is assigned to
            if (!GetAssignedResources(Membership.GetUser().UserName).Contains(workingReport.ProjectResource))
            {
                return false;
            }

            //todo probably needs to be reviewed if resource to task assignment is limited to
            //resources which are assigned to the task in ms project
            return !(workingReport.ProjectTask.IsDeleted ||
                     workingReport.ProjectResource.IsDeleted);
        }

        /// <summary>
        /// returns the available resources of a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the resources assigned to the given user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<ProjectResource> GetAssignedResources(string userName)
        {
            if (userName != null)
            {
                return EbalitDbContext.ProjectUserAssignments.
                    Where(cc => cc.MembershipUserName == userName).Select(cc => cc.ProjectResource).ToList();
            }
            return null;
        }

        /// <summary>
        /// Create new assignent with given userId and resourceId
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="resourceId"></param>
        public void AssignUser(string userName, int resourceId)
        {
            var userAssignment = new ProjectUserAssignment
            {
                ResourceId = resourceId,
                MembershipUserName = userName
            };

            EbalitDbContext.ProjectUserAssignments.Add(userAssignment);

            try
            {
                EbalitDbContext.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                //todo error handling

                throw;
            }

        }

        /// <summary>
        /// remove assignment with given userid and resourceid
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="resourceId"></param>
        public void RemoveUser(string userName, int resourceId)
        {
            var userAssignment = EbalitDbContext.ProjectUserAssignments.
                SingleOrDefault(cc => cc.MembershipUserName == userName && cc.ResourceId == resourceId);

            if (userAssignment != null)
            {
                EbalitDbContext.ProjectUserAssignments.Remove(userAssignment);

                try
                {
                    EbalitDbContext.SaveChanges();
                }
                catch (InvalidOperationException)
                {
                    //todo error handling

                    throw;
                }
            }
        }

        /// <summary>
        /// Returns the full text path to the task with given guid
        /// </summary>
        /// <param name="taskGuid"></param>
        /// <returns></returns>
        public string GetTaskPath(string taskGuid)
        {
            if (!string.IsNullOrWhiteSpace(taskGuid))
            {
                var taskRealGuid = Guid.Parse(taskGuid);
                var task = EbalitDbContext.ProjectTasks.Include("ProjectProject").Single(cc => cc.Guid == taskRealGuid);
                if (task.Parent == task.ProjectProject.Guid)
                {
                    return task.Name;
                }
                return GetTaskPath(task.Parent.ToString()) + "/" + task.Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// Creates working reports for each working report in the file.
        /// </summary>
        /// <exception cref="WorkingReportBatchImportException"></exception>
        /// <param name="workingreportsCsvFile"></param>
        public void InsertManyWorkingReports(List<WorkingReportCsvFile> workingreportsCsvFile)
        {
            foreach (var workingReport in workingreportsCsvFile)
            {
                var projectEntity =
                    EbalitDbContext.ProjectProjects.SingleOrDefault(cc => cc.Name == workingReport.ProjectName);
                if (projectEntity == null)
                {
                    throw new WorkingReportBatchImportException(string.Format("The project {0} could not be found.", workingReport.ProjectName));
                }

                var resourceEntity =
                    EbalitDbContext.ProjectResources.SingleOrDefault(cc => cc.ProjectProject.Id == projectEntity.Id &&
                                                                           cc.Name == workingReport.ResourceName);
                if (resourceEntity == null)
                {
                    throw new WorkingReportBatchImportException(string.Format("The resource {0} could not be found.", workingReport.ResourceName));
                }

                var taskEntity =
                    EbalitDbContext.ProjectTasks.SingleOrDefault(cc => cc.TfsTaskId == workingReport.TfsTaskName);

                if (taskEntity == null)
                {
                    throw new WorkingReportBatchImportException(string.Format("The task {0} could not be found.", workingReport.TfsTaskName));
                }

                var workingReportEntity = new ProjectWorkingReport
                {
                    ProjectProject = projectEntity,
                    ProjectResource = resourceEntity,
                    ProjectTask = taskEntity,
                    Notes = workingReport.Description,
                    Total = workingReport.WorkingTime
                };
                EbalitDbContext.ProjectWorkingReports.Add(workingReportEntity);
            }
            EbalitDbContext.SaveChanges();
        }
    }
}