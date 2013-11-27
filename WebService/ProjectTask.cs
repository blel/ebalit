using System;
using System.Linq;
using EbalitWebForms.Common;
using EbalitWebForms.WebService;

namespace EbalitWebForms.DataLayer
{
    /// <summary>
    /// ProjectTask entity.
    /// This partial class contains converters to Dtos
    /// </summary>
    public partial class ProjectTask
    {
        /// <summary>
        /// Convert the entity to the corresponding dto
        /// </summary>
        /// <returns>the task as dto</returns>
        public TaskDto ToDto()
        {
            return new TaskDto
            {
                ActualWork = Convert.ToDouble(ActualWork),
                TfsTaskId = TfsTaskId,
                ParentTfsTaskId = ParentTfsTaskId,
                Name = Name,
                Resources = ProjectResourceTaskAssignments.Select(cc=>cc.ProjectResource).ForEach(ccc=>ccc.ToDto())
            };
        }
    }
}