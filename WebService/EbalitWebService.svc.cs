using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
//using System.Transactions;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EbalitWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EbalitWebService.svc or EbalitWebService.svc.cs at the Solution Explorer and start debugging.
    public class EbalitWebService : IEbalitWebService
    {
        #region IEbalitWebService Members

        /// <summary>
        /// Returns the list of projects on the mps server
        /// </summary>
        /// <returns></returns>
        public IList<ProjectDto> GetProjects()
        {
            using (var context = new Ebalit_WebFormsEntities())
            {
                return context.ProjectProjects.Select(cc => new ProjectDto
                {
                    Name = cc.Name,
                    UniqueIdentifier = cc.Guid
                }).ToList();
            }
        }


        /// <summary>
        /// Creates or updates the project on the server
        /// </summary>
        /// <param name="project">Project Dto</param>
        public IList<ResourceDto> UpdateProject(ProjectDto project)
        {
            if (!IsProjectExisting(project))
            {
                throw new Exception("Project does not exist on server. Please create the project first and link it afterwards in MS Project.");
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
            //TODO: throws an error if sequence contains no elements
            using (var context = new Ebalit_WebFormsEntities())
            {
                taskDtos.AddRange(context.ProjectProjects.
                    Single(cc => cc.Guid == project.UniqueIdentifier).
                    ProjectTasks.Select(taskEntity => new TaskDto
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
            using (var context = new Ebalit_WebFormsEntities())
            {
                return (from projectEntity in context.ProjectProjects
                        where projectEntity.Guid == project.UniqueIdentifier
                        select projectEntity).Any();
            }
        }

        /// <summary>
        /// Creates a project entity from the dto and saves it to the database
        /// </summary>
        /// <param name="project"></param>
        private IList<ResourceDto> CreateProject(ProjectDto project)
        {
            var resources = project.GetResourcesAsEntities();

            //create new guid foreach resource
            resources.ForEach(cc => cc.Guid = Guid.NewGuid());

            using (var context = new Ebalit_WebFormsEntities())
            {
                //create the project entity
                var projectEntity = new ProjectProject
                {
                    Name = project.Name,
                    Guid = project.UniqueIdentifier,
                    //create the depending resources
                    ProjectResources = resources,
                    //create the depending tasks
                    ProjectTasks = project.GetTasksAsEntities()


                };
                //add project to context
                context.ProjectProjects.Add(projectEntity);

                //create the resource to task assignments

                foreach (var task in project.Tasks)
                {
                    foreach (var resource in task.Resources)
                    {
                        var resourceToTaskAssignment = new ProjectResourceTaskAssignment
                        {
                            ProjectResource = projectEntity.ProjectResources.Single(cc => cc.Guid == resource.Guid),
                            ProjectTask = projectEntity.ProjectTasks.Single(cc => cc.TfsTaskId == task.TfsTaskId)

                        };
                        //add assignment to context
                        context.ProjectResourceTaskAssignments.Add(resourceToTaskAssignment);
                    }
                }

                //save changes
                try
                {
                    context.SaveChanges();
                    //return the resources as dtos
                    return resources.ForEach(cc => cc.ToDto());
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
        }

        /// <summary>
        /// Syncs the project on the server with the project from ms project
        /// TODO: had to remove transaction handling. which is a big risk, but 2005 seems to have problems with it.
        /// </summary>
        /// <param name="project"></param>
        private List<ResourceDto> SyncProjects(ProjectDto project)
        {
            //using (var transaction = new TransactionScope())
            //{
            //    try
            //    {
            var resources = SyncResources(project);

            SyncTasks(project);

            //        transaction.Complete();
            //    }
            //    catch (InvalidOperationException)
            //    {
            //        //todo: exception handling                    
            //    }
            //}
            return resources;
        }

        private List<ResourceDto> SyncResources(ProjectDto project)
        {
            using (var context = new Ebalit_WebFormsEntities())
            {
                //get the project entity corresponding to the project dto
                var projectEntity = context.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier);
                //delete resources
                //todo: this could also be removed
                //var deletedResources = context.ProjectResources.
                //    Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier).
                //    ForEach(cc => cc.ToDto()).Except(project.Resources,
                //    new ResourceDtoEqualityComparer());
                //foreach (var deleteResource in deletedResources)
                //{
                //    var resourceEntity = context.ProjectResources.Single(cc => cc.Guid == deleteResource.Guid);
                //    resourceEntity.IsDeleted = true;

                //    //make sure all ResourceTaskAssignments of this resource are deleted as well
                //    resourceEntity.ProjectResourceTaskAssignments.ForEach(cc => cc.IsDeleted = true);
                //}

                //add new resources
                var addedResources = project.Resources.Except(context.ProjectResources.ForEach(cc => cc.ToDto()),
                    new ResourceDtoEqualityComparer());

                foreach (var addedResource in addedResources)
                {
                    addedResource.Guid = Guid.NewGuid();
                    projectEntity.ProjectResources.Add(new ProjectResource
                    {
                        //the Guid needs to be created here
                        Guid = addedResource.Guid,
                        Name = addedResource.Name,
                    });
                }

                //update equal resources
                var sameResources = project.Resources.Intersect(context.ProjectResources.ForEach(cc => cc.ToDto()),
                    new ResourceDtoEqualityComparer());
                foreach (var sameResource in sameResources)
                {
                    var resourceEntity = context.ProjectResources.Single(cc => cc.Guid == sameResource.Guid);
                    resourceEntity.Name = sameResource.Name;
                }

                //save changes
                try
                {
                    context.SaveChanges();

                    return projectEntity.ProjectResources.ForEach(cc => cc.ToDto()).ToList();
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

        }

        private void SyncTasks(ProjectDto project)
        {
            using (var context = new Ebalit_WebFormsEntities())
            {
                //get the project entity corresponding to the project dto
                var projectEntity = context.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier);

                //get tasks which exist on server but not in ms project
                //implement the comparer
                //var deletedTasks = context.ProjectTasks.
                //    Where(cc => cc.ProjectProject.Guid == project.UniqueIdentifier).
                //    ForEach(cc => cc.ToDto()).Except(project.Tasks,
                //    new TaskDtoEqualityComparer());

                ////mark them as deleted
                //foreach (var taskDto in deletedTasks)
                //{
                //    var taskEntity = context.ProjectTasks.Single(cc => cc.TfsTaskId == taskDto.TfsTaskId);
                //    taskEntity.IsDeleted = true;

                //    //mark the resource assignment as deleted
                //    taskEntity.ProjectResourceTaskAssignments.ForEach(cc => cc.IsDeleted = true);
                //}

                //get the new tasks
                var newTasks = project.Tasks.Except(context.ProjectTasks.ForEach(cc => cc.ToDto()),
                    new TaskDtoEqualityComparer());

                //add them
                foreach (var newTask in newTasks)
                {
                    //create the task Entity
                    var projectTask = new ProjectTask
                    {
                        ActualWork = newTask.ActualWork,
                        Name = newTask.Name,
                        TfsTaskId = newTask.TfsTaskId,
                        ParentTfsTaskId = newTask.ParentTfsTaskId

                    };

                    //Assign the appropriate resources to this task
                    //TODO: Reimplement
                    //CreateTaskResourceAssignments(context, newTask, projectTask);

                    //add the task to the project
                    projectEntity.ProjectTasks.Add(projectTask);
                }

                //same tasks
                var sameTasks = project.Tasks.Intersect(context.ProjectTasks.ForEach(cc => cc.ToDto()),
                    new TaskDtoEqualityComparer());

                //update the server tasks
                foreach (var task in sameTasks)
                {
                    var taskEntity = context.ProjectTasks.Single(cc => cc.TfsTaskId == task.TfsTaskId);
                    taskEntity.Name = task.Name;
                    taskEntity.TfsTaskId = task.TfsTaskId;
                    taskEntity.ParentTfsTaskId = task.ParentTfsTaskId;
                    //to simplify, I delete taskResourceAssignments and create new ones based
                    //on ms project
                    context.ProjectResourceTaskAssignments.RemoveMany(taskEntity.ProjectResourceTaskAssignments);

                    //create the new assignments
                    //TODO: Reimplement
                    //CreateTaskResourceAssignments(context, task, taskEntity);
                }

                //save changes
                try
                {
                    context.SaveChanges();
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
        }

        private void CreateTaskResourceAssignments(Ebalit_WebFormsEntities context, TaskDto newTask, ProjectTask projectTask)
        {
            //Assign the appropriate resources to this task
            foreach (var resource in newTask.Resources)
            {
                //get the Resource Entity
                var resourceEntity = context.ProjectResources.Single(cc => cc.Guid == resource.Guid);

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


    }
}

