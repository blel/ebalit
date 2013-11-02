using System;
using System.Runtime.Serialization;

namespace EbalitWebForms.WebService
{
    /// <summary>
    /// The work resource dto
    /// </summary>
    [DataContract]
    public class ResourceDto
    {
        /// <summary>
        /// Resource name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Resource guid
        /// </summary>
        [DataMember]
        public Guid Guid { get; set; }
    }
}