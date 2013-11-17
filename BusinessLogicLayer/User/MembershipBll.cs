using System.Collections.Generic;
using System.Linq;
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
        /// Returns a list of all membership users
        /// Only the username is set.
        /// </summary>
        /// <returns></returns>
        public IList<aspnet_Users> GetUsers()
        {
            return Membership.GetAllUsers().Cast<MembershipUser>().ForEach(cc => new aspnet_Users
            {
                UserName = cc.UserName
            });

        }
    }
}