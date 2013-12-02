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
        /// The guid which is used to check whether it is 
        /// the same resource in ms project as on the mps server
        /// </summary>
        [DataMember]
        public Guid MpsServerGuid { get; set; }

        /// <summary>
        /// The MsProject Guid. This is to make sure resources are correctly
        /// identified as long as no mps server guid is existing
        /// </summary>
        [DataMember]
        public Guid MsProjectGuid { get; set; }

    }
}