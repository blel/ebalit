using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.Common;
using EbalitWebForms.WebService;

namespace EbalitWebForms.DataLayer
{
    /// <summary>
    /// converts teh entity to the dto
    /// </summary>
    public partial class ProjectTask
    {
        public TaskDto ToDto()
        {
            return new TaskDto
            {
                ActualWork = Convert.ToDouble(this.ActualWork),
                Guid = this.Guid,
                Name = this.Name,
                ParentGuid = this.Parent.GetValueOrDefault(),
                Resources = this.ProjectResourceTaskAssignments.Select(cc=>cc.ProjectResource).ForEach(ccc=>ccc.ToDto())
            };
        }
    }
}