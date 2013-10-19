using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.Controlling
{
    public partial class CreateProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if site is really loaded and no postback
            if (!IsPostBack)
            {
                //if there is a "Id" parameter in the url change to edit mode
                if (Request.QueryString["Id"] != null)
                {
                    this.dvwProject.ChangeMode(DetailsViewMode.Edit);
                }
                //otherwise go to insert mode
                else
                {
                    this.dvwProject.ChangeMode(DetailsViewMode.Insert);
                }
            }

        }

        /// <summary>
        /// Seems this helps with POCO and code first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void edsControllingDataSource_ContextCreating(object sender, EntityDataSourceContextCreatingEventArgs e)
        {
            var db = new DataLayer.Controlling.ControllingContext();
            e.Context = (db as IObjectContextAdapter).ObjectContext;
        }
    }
}