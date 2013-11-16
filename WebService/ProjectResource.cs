using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.WebService;

namespace EbalitWebForms.DataLayer
{
    public partial class ProjectResource
    {
        /// <summary>
        /// returns the entity as a dto
        /// </summary>
        /// <returns></returns>
        public ResourceDto ToDto()
        {
            return new ResourceDto
            {
                Guid = Guid,
                Name = Name
            };
        }
    }
}