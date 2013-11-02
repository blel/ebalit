using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EbalitWebForms.WebService
{
    /// <summary>
    /// Data Transfer Object for tasks from ms-project to server.
    /// </summary>
    [DataContract]
    public class TaskDto
    {

        /// <summary>
        /// The task name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The parent task - if this is a top level task, use project as parent.
        /// </summary>
        [DataMember]
        public Guid ParentGuid { get; set; }

        /// <summary>
        /// the unique identifier of the task
        /// </summary>
        [DataMember]
        public Guid Guid{ get; set; }

        /// <summary>
        /// the work resources of the task.
        /// </summary>
        [DataMember]
        public IList<ResourceDto> Resources { get; set; } 

        /// <summary>
        /// The actual work of the task
        /// </summary>
        [DataMember]
        public double ActualWork { get; set; }

    }
}