using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EbalitWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EbalitWebService.svc or EbalitWebService.svc.cs at the Solution Explorer and start debugging.
    public class EbalitWebService : IEbalitWebService, IDisposable
    {
        private readonly Ebalit_WebFormsEntities _ebalitContext;

        private readonly bool _deleteResources;

        private readonly bool _deleteTasks;

        /// <summary>
        /// Ctor creates a context and assigns it to field
        /// </summary>
        public EbalitWebService()
        {
            _ebalitContext = new Ebalit_WebFormsEntities();

            //Get the configuration from config file
            var config = (EbalitConfig)System.Configuration.ConfigurationManager.GetSection("ebalit");

            _deleteResources = config.DeleteResourcesRemovedFromMsProject;

            _deleteTasks = config.DeleteTasksRemovedFromMsProject;

        }

        #region IEbalitWebService Members

        /// <summary>
        /// Returns the list of projects on the mps server
        /// </summary>
        /// <returns></returns>
        public IList<ProjectDto> GetProjects()
        {
            return _ebalitContext.ProjectProjects.Select(cc => new ProjectDto
            {
                Name = cc.Name,
                UniqueIdentifier = cc.Guid
            }).ToList();
        }


        /// <summary>
        /// Creates or updates the project on the server
        /// </summary>
        /// <param name="project">Project Dto</param>
        public ProjectDto UpdateProject(ProjectDto project)
        {
            if (!IsProjectExisting(project))
            {
                throw new Exception(
                    "Project does not exist on server. Please create the project first and link it afterwards in MS Project.");
            }

            return SyncProjects(project);
        }

        /// <summary>
        /// returns the actual work from server to the service
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public IList<TaskDto> GetActualWork(ProjectDto project)
        {
            var taskDtos = new List<TaskDto>();

            var projectEntity = _ebalitContext.ProjectProjects.
                SingleOrDefault(cc => cc.Guid == project.UniqueIdentifier);
            if (projectEntity != null)
            {

                taskDtos.AddRange(projectEntity.ProjectTasks.Select(taskEntity => new TaskDto
                    {
                        ActualWork = taskEntity.ActualWork != null ? (double)taskEntity.ActualWork : 0.0,
                        TfsTaskId = taskEntity.TfsTaskId
                    }));

            }
            return taskDtos;
        }

        #endregion

        /// <summary>
        /// Checks whether there is a project in the database with 
        /// same guid as dto
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private bool IsProjectExisting(ProjectDto project)
        {

            return (from projectEntity in _ebalitContext.ProjectProjects
                    where projectEntity.Guid == project.UniqueIdentifier
                    select projectEntity).Any();
        }

        /// <summary>
        /// Syncs the project on the server with the project from ms project
        /// TODO: had to remove transaction handling. which is a big risk, but 2005 seems to have problems with it.
        /// </summary>
        /// <param name="project"></param>
        private ProjectDto SyncProjects(ProjectDto project)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    SyncResources(project);

                    SyncTasks(project);

                    transaction.Complete();
                }
                catch (InvalidOperationException)
                {
                    //todo: exception handling                    
                }
            }
            return project;
        }

        private void SyncResources(ProjectDto project)
        {
            //get the project entity corresponding to the project dto
            var projectEntity = _ebalitContext.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier);
            
            //delete resource only if configured accordingly
            if (_deleteResources)
            {
                var deletedResources = _ebalitContext.ProjectResources.
                    Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier).
                    ForEach(cc => cc.ToDto()).Except(project.Resources,
                    new ResourceDtoEqualityComparer());
                foreach (var deleteResource in deletedResources)
                {
                    var resourceEntity = _ebalitContext.ProjectResources.Single(cc => cc.Guid == deleteResource.MpsServerGuid);
                    resourceEntity.IsDeleted = true;

                    //make sure all ResourceTaskAssignments of this resource are deleted as well
                    resourceEntity.ProjectResourceTaskAssignments.ForEach(cc => cc.IsDeleted = true);
                }
            }

            //add new resources
            var addedResources = project.Resources.Except(_ebalitContext.ProjectResources.ForEach(cc => cc.ToDto()),
                new ResourceDtoEqualityComparer());

            //create the entities
            foreach (var addedResource in addedResources)
            {
                //assign here the new mpsserver guid
                addedResource.MpsServerGuid = Guid.NewGuid();
                projectEntity.ProjectResources.Add(new ProjectResource
                {
                    //the Guid needs to be created here
                    Guid = addedResource.MpsServerGuid,
                    Name = addedResource.Name,
                });
            }

            //update the resources assigned to tasks with appropriate guids
            foreach (var task in project.Tasks)
            {
                foreach (var addedResource in addedResources)
                {
                    var resourceToUpdate =
                        task.Resources.SingleOrDefault(cc => cc.MsProjectGuid == addedResource.MsProjectGuid);
                    if (resourceToUpdate != null)
                    {
                        resourceToUpdate.MpsServerGuid = addedResource.MpsServerGuid;
                    }
                }
            }

            //update equal resources
            var sameResources = project.Resources.Intersect(_ebalitContext.ProjectResources.ForEach(cc => cc.ToDto()),
                new ResourceDtoEqualityComparer());
            foreach (var sameResource in sameResources)
            {
                var resourceEntity = _ebalitContext.ProjectResources.Single(cc => cc.Guid == sameResource.MpsServerGuid);
                resourceEntity.Name = sameResource.Name;
            }

            //save changes
            try
            {
                _ebalitContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }


        private void SyncTasks(ProjectDto project)
        {

            //get the project entity corresponding to the project dto
            var projectEntity = _ebalitContext.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier);

            //get tasks which exist on server but not in ms project
            if (_deleteTasks)
            {
                var deletedTasks = _ebalitContext.ProjectTasks.
                    Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier).
                    ForEach(cc => cc.ToDto()).Except(project.Tasks,
                    new TaskDtoEqualityComparer());

                //mark them as deleted
                foreach (var taskDto in deletedTasks)
                {
                    var taskEntity = _ebalitContext.ProjectTasks.Single(cc => cc.TfsTaskId == taskDto.TfsTaskId);
                    taskEntity.IsDeleted = true;

                    //mark the resource assignment as deleted
                    taskEntity.ProjectResourceTaskAssignments.ForEach(cc => cc.IsDeleted = true);
                }
            }

            //get the new tasks
            var newTasks =
                project.Tasks.Except(
                    _ebalitContext.ProjectTasks.Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier)
                        .ForEach(cc => cc.ToDto()),
                    new TaskDtoEqualityComparer());

            //add them
            foreach (var newTask in newTasks)
            {
                //create the task Entity
                var taskEntity = new ProjectTask
                {
                    ActualWork = newTask.ActualWork,
                    Name = newTask.Name,
                    TfsTaskId = newTask.TfsTaskId,
                    ParentTfsTaskId = newTask.ParentTfsTaskId

                };

                //Assign the appropriate resources to this task
                //TODO: Reimplement
                CreateTaskResourceAssignments(newTask, taskEntity);

                //add the task to the project
                projectEntity.ProjectTasks.Add(taskEntity);
            }

            //same tasks
            var sameTasks =
                project.Tasks.Intersect(
                    _ebalitContext.ProjectTasks.Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier)
                        .ForEach(cc => cc.ToDto()),
                    new TaskDtoEqualityComparer());

            //update the server tasks
            foreach (var task in sameTasks)
            {
                var taskEntity =
                    _ebalitContext.ProjectTasks.Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier)
                        .Single(cc => cc.TfsTaskId == task.TfsTaskId);
                taskEntity.Name = task.Name;
                taskEntity.TfsTaskId = task.TfsTaskId;
                taskEntity.ParentTfsTaskId = task.ParentTfsTaskId;
                //to simplify, I delete taskResourceAssignments and create new ones based
                //on ms project
                _ebalitContext.ProjectResourceTaskAssignments.RemoveMany(taskEntity.ProjectResourceTaskAssignments);

                //create the new assignments
                //TODO: Reimplement
                CreateTaskResourceAssignments(task, taskEntity);
            }

            //save changes
            try
            {
                _ebalitContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void CreateTaskResourceAssignments(TaskDto newTask, ProjectTask projectTask)
        {
            //Assign the appropriate resources to this task
            foreach (var resource in newTask.Resources)
            {
                //get the Resource Entity
                var resourceEntity = _ebalitContext.ProjectResources.Single(cc => cc.Guid == resource.MpsServerGuid);

                //create the assignment
                var taskResourceAssignment = new ProjectResourceTaskAssignment
                {
                    ProjectTask = projectTask,
                    ProjectResource = resourceEntity
                };

                //add it to the task
                projectTask.ProjectResourceTaskAssignments.Add(taskResourceAssignment);
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Implementation of IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Implementation of IDisposable
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ebalitContext.Dispose();
            }
        }

        #endregion
    }
}

