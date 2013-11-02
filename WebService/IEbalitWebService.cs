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
        /// returns a list of all projects and depending items.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<ProjectDto> GetProjects();

        /// <summary>
        /// Updates the project on the server with info from dto.
        /// </summary>
        /// <param name="project"></param>
        [OperationContract]
        void UpdateProject(ProjectDto project);


    }
}
