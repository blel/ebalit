using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.Common;
using EbalitWebForms.WebService;

namespace EbalitWebForms.DataLayer
{
    public partial class ProjectProject
    {
        /// <summary>
        /// Maps the Entity to a Dto
        /// </summary>
        /// <returns></returns>
        public ProjectDto ToDto()
        {
            return new ProjectDto
            {
                Name = Name,
                UniqueIdentifier = Guid,
                Tasks = ProjectTasks.ForEach(cc=>(cc.ToDto()))
            };
        }
    }
}