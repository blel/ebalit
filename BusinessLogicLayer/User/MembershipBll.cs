using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using EbalitWebForms.Common;
using EbalitWebForms.DataLayer;
using System.Web.Security;

namespace EbalitWebForms.BusinessLogicLayer.User
{
    /// <summary>
    /// Provides access to membership objects
    /// </summary>
    public class MembershipBll:DataAccessLayer
    {
        /// <summary>
        /// Returns a list of the names of all membership users.
        /// </summary>
        /// <returns></returns>
        public IList<MembershipUser> GetUsers()
        {
            return Membership.GetAllUsers().Cast<MembershipUser>().ToList();
        }
    }
}