using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;
using System.Web.Security;
using EbalitWebForms.BusinessLogicLayer;
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
            }
            else
            {
                CreateProject(project);
            }
        }

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

