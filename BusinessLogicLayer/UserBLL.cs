using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class UserBLL:BusinessLogicLayer.DataAccessLayer
    {
        public DataLayer.User GetUserByUsername(string username)
        {
            return (from cc in base.EbalitDBContext.Users
                    where cc.Username == username
                    select cc).FirstOrDefault();
        }

    }
}