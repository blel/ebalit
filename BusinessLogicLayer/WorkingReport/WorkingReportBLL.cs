using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
            return project == null
                ? EbalitDbContext.ProjectTasks.ToList()
                : EbalitDbContext.ProjectTasks.Where(cc => cc.ProjectProject.Id == project.Id && !cc.IsDeleted).ToList();
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
        /// Returns the id of the task with given tfsId
        /// </summary>
        /// <param name="tfsId"></param>
        /// <returns></returns>
        public int GetTaksIdByTfsId(string tfsId, string projectId)
        {
            var projectIdInt = Convert.ToInt32(projectId);
            return EbalitDbContext.ProjectTasks.Single(cc => cc.TfsTaskId == tfsId && cc.ProjectId == projectIdInt).Id;
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
                    ConditionalWhere(cc => cc.From >= findDto.From, () => findDto.From != null).
                    ConditionalWhere(cc => cc.To <= findDto.To, () => findDto.To != null).
                    ConditionalWhere(cc => cc.ProjectId == findDto.ProjectId,
                        () => findDto.ProjectId != null && findDto.ProjectId != 0).
                    ConditionalWhere(cc => cc.ResourceId == findDto.ResourceId,
                        () => findDto.ResourceId != null && findDto.ResourceId != 0).
                    ConditionalWhere(cc => cc.ProjectTask.TfsTaskId == findDto.TaskTfsId,
                        () => !string.IsNullOrWhiteSpace(findDto.TaskTfsId));
                //todo search for child tasks
            }
            return resultList.ToList();
        }

        /// <summary>
        /// Updates a given working report
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
        /// TODO: check whether transactionhandling works now
        /// see: http://pieterderycke.wordpress.com/2012/01/22/transactionscope-transaction-escalation-behavior/ for a solution
        /// </summary>
        /// <param name="workingReport"></param>
        public void DeleteWorkingReport(ProjectWorkingReport workingReport)
        {
            var reportToDelete = GetWorkingReport(workingReport.Id);

            using (var transaction = new TransactionScope())
            {

                var taskId = Convert.ToInt32(reportToDelete.TaskId);

                EbalitDbContext.ProjectWorkingReports.Remove(reportToDelete);

                EbalitDbContext.SaveChanges();

                UpdateActualWork(taskId);

                transaction.Complete();

            }

        }

        /// <summary>
        /// Create new working report
        /// </summary>
        /// <param name="workingReport"></param>
        public void CreateWorkingReport(ProjectWorkingReport workingReport)
        {
            using (var transaction = new TransactionScope())
            {

                EbalitDbContext.ProjectWorkingReports.Add(workingReport);

                EbalitDbContext.SaveChanges();

                UpdateActualWork(Convert.ToInt32(workingReport.TaskId));

                transaction.Complete();
            }

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
            {
                task.ActualWork = Convert.ToDouble(calculatedWork);
            }
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
            var project =
                EbalitDbContext.ProjectProjects.Include("ProjectResources").SingleOrDefault(cc => cc.Id == projectId);
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

            EbalitDbContext.SaveChanges();
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

                //error handling has to be done by client
                EbalitDbContext.SaveChanges();

            }
        }

        /// <summary>
        /// Returns the full text path to the task with given guid
        /// </summary>
        /// <param name="taskTfsId"></param>
        /// <returns></returns>
        public string GetTaskPath(string taskTfsId, int projectId)
        {
            if (!string.IsNullOrWhiteSpace(taskTfsId))
            {

                var task =
                    EbalitDbContext.ProjectTasks.Include("ProjectProject")
                        .Single(cc => cc.TfsTaskId == taskTfsId && cc.ProjectId == projectId);
                if (string.IsNullOrWhiteSpace(task.ParentTfsTaskId))
                {
                    return task.Name;
                }
                return GetTaskPath(task.ParentTfsTaskId, projectId) + "/" + task.Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// Creates working reports for each working report in the file.
        /// </summary>
        /// <exception cref="WorkingReportBatchImportException"></exception>
        /// <param name="workingreportsCsvFile"></param>
        public List<WorkingReportCsvFile> InsertManyWorkingReports(List<WorkingReportCsvFile> workingreportsCsvFile)
        {
            var erroneousRecords = new List<WorkingReportCsvFile>();
            if (workingreportsCsvFile == null)
            {
                return erroneousRecords;
            }

            foreach (var workingReport in workingreportsCsvFile)
            {
                var projectEntity =
                    EbalitDbContext.ProjectProjects.SingleOrDefault(cc => cc.Name == workingReport.ProjectName);


                var resourceEntity =
                    EbalitDbContext.ProjectResources.SingleOrDefault(cc => cc.ProjectProject.Id == projectEntity.Id &&
                                                                           cc.Name == workingReport.ResourceName);


                var taskEntity =
                    EbalitDbContext.ProjectTasks.SingleOrDefault(cc => cc.TfsTaskId == workingReport.TfsTaskName);



                if (projectEntity == null || resourceEntity == null || taskEntity == null)
                {
                    var erroneousRecord = new WorkingReportCsvFile
                    {
                        Date = workingReport.Date,
                        Description = workingReport.Description,
                        ProjectName = projectEntity == null ? string.Empty : projectEntity.Name,
                        ResourceName = resourceEntity == null ? string.Empty : resourceEntity.Name,
                        TfsTaskName = taskEntity == null ? string.Empty : taskEntity.Name,
                        WorkingTime = workingReport.WorkingTime
                    };
                    erroneousRecords.Add(erroneousRecord);
                }
                else
                {
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

            }
            EbalitDbContext.SaveChanges();
            return erroneousRecords;
        }
    }
}
