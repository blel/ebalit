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
        /// the work resources of the task.
        /// </summary>
        [DataMember]
        public IList<ResourceDto> Resources { get; set; } 

        /// <summary>
        /// The actual work of the task
        /// </summary>
        [DataMember]
        public double ActualWork { get; set; }

        /// <summary>
        /// The TFS taks id.
        /// This will replace the Guid
        /// </summary>
        [DataMember]
        public string TfsTaskId { get; set; }

        /// <summary>
        /// The parent tfs task id.
        /// </summary>
        [DataMember]
        public string ParentTfsTaskId { get; set; }

    }
}