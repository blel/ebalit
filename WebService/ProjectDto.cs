using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.WebService
{
    /// <summary>
    /// The dto for project data
    /// This is the project summary task in ms project
    /// </summary>
    [DataContract]
    public class ProjectDto
    {
        /// <summary>
        /// The project name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for the project
        /// </summary>
        [DataMember]
        public Guid UniqueIdentifier { get; set; }

        /// <summary>
        /// The list of tasks of that project
        /// </summary>
        [DataMember]
        public IList<TaskDto> Tasks { get; set; }

        /// <summary>
        /// All resources of teh project
        /// </summary>
        [DataMember]
        public IList<ResourceDto> Resources { get; set; }

        /// <summary>
        /// Returns the tasks of the project as entities
        /// </summary>
        /// <returns></returns>
        public IList<ProjectTask> GetTasksAsEntities()
        {
            return this.Tasks.Select(task => new ProjectTask
            {
                Name = task.Name,
                Guid = task.Guid,
                Parent = task.ParentGuid,
                ActualWork = task.ActualWork
            }).ToList();
        }

        /// <summary>
        /// Returns the resources of the ProjectDto as a list of resource entities
        /// </summary>
        /// <returns></returns>
        public IList<ProjectResource> GetResourcesAsEntities()
        {
            var resources = new List<ProjectResource>();
            foreach (var resourceDto in 
                from task in Tasks 
                from resourceDto in task.Resources 
                where resources.All(cc => cc.Guid != resourceDto.Guid) 
                select resourceDto)
            {
                resources.Add(new ProjectResource
                {
                    Name = resourceDto.Name,
                    Guid = resourceDto.Guid,
                });
            }
            return resources;
        }

    }
}