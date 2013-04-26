using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class CategoryBrowser : System.Web.UI.UserControl
    {
        public string BlogTopic { get; set; }

        public event CommandEventHandler LinkButtonPressed;

        protected void Page_Init(object sender, EventArgs e)
        {
            BlogCategoryDAL categoryDAL = new BlogCategoryDAL();
            BlogEntryDAL entryDAL = new BlogEntryDAL();

            //setup the accordion
            this.Accordion.Panes.Clear();
            foreach (BlogCategory category in categoryDAL.ReadBlogCategory(BlogTopic))
            {
                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + category.Id;

                LiteralControl header = new LiteralControl(string.Format("<div class = \"MenuHeader\"><b>{0}</b></div>", category.Category));

                pane.HeaderContainer.Controls.Add(header);
                pane.ContentContainer.Controls.Add(new LiteralControl("<ul style='margin-top:0; margin-bottom:0'>"));
                foreach (BlogEntry entry in entryDAL.GetBlogEntries(category.Id))
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.Subject;
                    linkButton.CommandArgument = entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(linkButton_Command);
                    linkButton.CausesValidation = false;
                    linkButton.CssClass = "MenuButton";
                    pane.ContentContainer.Controls.Add(new LiteralControl("<li>"));
                    pane.ContentContainer.Controls.Add(linkButton);
                    pane.ContentContainer.Controls.Add(new LiteralControl("</li>"));
                }
                pane.ContentContainer.Controls.Add(new LiteralControl("</ul>"));
                this.Accordion.Panes.Add(pane);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //make sure the selected accordion pane is selected again on postback
                if (!string.IsNullOrEmpty(hdfSelectedPane.Value))
                    Accordion.SelectedIndex = Convert.ToInt32(hdfSelectedPane.Value);
            }

           
        }

        void linkButton_Command(object sender, CommandEventArgs e)
        {
            if (LinkButtonPressed != null)
                LinkButtonPressed(sender, e);
        }
    }
}