using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEbalitWebService" in both code and config file together.
    [ServiceContract]
    public interface IEbalitWebService
    {
        /// <summary>
        /// returns a list of all tasks with the actual work assigned.
        /// TaskDto will contain only Guid and actual work.
        /// Parameter project: only guid is mandatory.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<TaskDto> GetActualWork(ProjectDto project);

        /// <summary>
        /// Updates the project on the server with info from dto.
        /// </summary>
        /// <param name="project"></param>
        [OperationContract]
        ProjectDto UpdateProject(ProjectDto project);

        /// <summary>
        /// Returns a list of all available projects
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<ProjectDto> GetProjects();





    }
}
