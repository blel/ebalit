using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EbalitWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EbalitWebService.svc or EbalitWebService.svc.cs at the Solution Explorer and start debugging.
    public class EbalitWebService : IEbalitWebService
    {

        public void UpdateProject(ProjectDto project)
        {
            if (IsProjectExisting(project))
            {
                //master is ms project
                //set initial actual work as from ms project
                //synchronize tasks
                //--deactivate tasks which do not exist anymore
                //--create tasks which do not exist yet
                //--update tasks which are equal
                //synchronize resources
                //--deactivate resources which do not exist anymore
                //--create resources which do not exist yet
                //--update resources which are equal
                //synchronize resource to task assignment
                //--deactivate resource to task assignment which do not exist anymore
                //--create resource to task assignment which do not exist yet
                //--update resource to task assignment which are equal
                //--make sure acutal work is right!
                //transaction handling
                SyncProjects(project);
            }
            else
            {
                CreateProject(project);
            }
        }

        /// <summary>
        /// returns the actual work from server to the service
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public IList<TaskDto> GetActualWork(ProjectDto project)
        {
            var taskDtos = new List<TaskDto>();

            using (var context = new Ebalit_WebFormsEntities())
            {
                taskDtos.AddRange(context.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier).ProjectTasks.Select(taskEntity => new TaskDto
                {
                    ActualWork = taskEntity.ActualWork != null ? (double) taskEntity.ActualWork : 0.0,
                    Guid = taskEntity.Guid
                }));
            }

            return taskDtos;
        }

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
        private void CreateProject(ProjectDto project)
        {
            using (var context = new Ebalit_WebFormsEntities())
            {
                //create the project entity
                var projectEntity = new ProjectProject
                {
                    Name = project.Name,
                    Guid = project.UniqueIdentifier,
                    //create the depending resources
                    ProjectResources = GetResources(project),
                    //create the depending tasks
                    ProjectTasks = GetTasks(project)
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
                            ProjectTask = projectEntity.ProjectTasks.Single(cc => cc.Guid == task.Guid)

                        };
                        //add assignment to context
                        context.ProjectResourceTaskAssignments.Add(resourceToTaskAssignment);
                    }
                }

                //save changes
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Syncs the project on the server with the project from ms project
        /// </summary>
        /// <param name="project"></param>
        private void SyncProjects(ProjectDto project)
        {
            using (var context = new Ebalit_WebFormsEntities())
            {
                //get the project entity corresponding to the project dto
                var projectEntity = context.ProjectProjects.Single(cc => cc.Guid == project.UniqueIdentifier);

                SyncResources(context, project, projectEntity);
                
                
                //get tasks which exist on server but not in ms project
                //implement the comparer
                var deletedTasks = context.ProjectTasks.ForEach(cc => cc.ToDto()).Except(project.Tasks,
                    new TaskDtoEqualityComparer());

                //mark them as deleted
                foreach (var taskDto in deletedTasks)
                {
                    var taskEntity = context.ProjectTasks.Single(cc => cc.Guid == taskDto.Guid);
                    taskEntity.IsDeleted = true;
                    //mark the resource assignment as deleted
                    foreach (var resourceAssignment in taskEntity.ProjectResourceTaskAssignments)
                    {
                        resourceAssignment.IsDeleted = true;
                    }
                }

                //get the new tasks
                var newTasks = project.Tasks.Except(context.ProjectTasks.ForEach(cc => cc.ToDto()), new TaskDtoEqualityComparer());

                //add them
                foreach (var newTask in newTasks)
                {
                    var projectTask = new ProjectTask

                    {
                        ActualWork = newTask.ActualWork,
                        Guid = newTask.Guid,
                        Parent = newTask.ParentGuid  ,
                        Name = newTask.Name
                    };

                    foreach (var resource in newTask.Resources)
                    {
                        var taskResource = new ProjectResourceTaskAssignment
                        {
                            ProjectTask = projectTask,
                            ProjectResource = new ProjectResource
                            {

                            }
                        };
                    }
                    projectEntity.ProjectTasks.Add(projectTask);
                }

                //same tasks
                var sameTasks = project.Tasks.Intersect(context.ProjectTasks.ForEach(cc => cc.ToDto()),
                    new TaskDtoEqualityComparer());

                //update the server tasks
                foreach (var task in sameTasks)
                {
                    var taskEntity = context.ProjectTasks.Single(cc => cc.Guid == task.Guid);
                    taskEntity.Name = task.Name;
                    taskEntity.Parent = task.ParentGuid;
                    
                    //TODO: resources
                }
                //save changes

                context.SaveChanges();
            }
        }

        private void SyncResources(Ebalit_WebFormsEntities context, ProjectDto project, ProjectProject projectEntity )
        {
            //delete resource
            var deletedResources = context.ProjectResources.ForEach(cc => cc.ToDto()).Except(project.Resources, new ResourceDtoEqualityComparer());
            foreach (var deleteResource in deletedResources)
            {
                var resourceEntity = context.ProjectResources.Single(cc => cc.Guid == deleteResource.Guid);
                resourceEntity.IsDeleted = true;
            }

            //add new resources
            var addedResources = project.Resources.Except(context.ProjectResources.ForEach(cc => cc.ToDto()), new ResourceDtoEqualityComparer());
            foreach (var addedResource in addedResources)
            {
                projectEntity.ProjectResources.Add(new ProjectResource
                {
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

        }

        /// <summary>
        /// Get the resources from the project as entities
        /// Each resource is added only once
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IList<ProjectResource> GetResources(ProjectDto project)
        {
            var resources = new List<ProjectResource>();
            foreach (var task in project.Tasks)
            {
                foreach (var resourceDto in task.Resources)
                {
                    if (!resources.Any(cc => cc.Guid == resourceDto.Guid))
                    {
                        resources.Add(new ProjectResource
                        {
                            Name = resourceDto.Name,
                            Guid = resourceDto.Guid,
                        });
                    }
                }
            }
            return resources;
        }

        /// <summary>
        /// Get the tasks from teh project dto as entities 
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IList<ProjectTask> GetTasks(ProjectDto project)
        {
            return project.Tasks.Select(task => new ProjectTask
            {
                Name = task.Name,
                Guid = task.Guid,
                Parent = task.ParentGuid,
                ActualWork = task.ActualWork
            }).ToList();
        }
    }
}

