using System;
using System.Collections.Generic;
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
    }
}