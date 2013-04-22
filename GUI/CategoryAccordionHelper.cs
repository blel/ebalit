using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.BusinessLogicLayer;
using EbalitWebForms.DataLayer;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EbalitWebForms.GUI
{
    public delegate void LinkButtonCommand(object sender, CommandEventArgs e);
    
    public class CategoryAccordionHelper
    {
        #region fields
        private AjaxControlToolkit.Accordion _accordion;
        #endregion

        #region constructors
        public CategoryAccordionHelper(AjaxControlToolkit.Accordion accordion)
        {
            _accordion = accordion;
        }
        #endregion

        public AjaxControlToolkit.Accordion CreateAccordion(string blogTopic, LinkButtonCommand command)
        {
            BlogCategoryDAL categoryDAL = new BlogCategoryDAL();
            BlogTopicDAL topicDAL = new BlogTopicDAL();
            BlogEntryDAL entryDAL = new BlogEntryDAL();

            //setup the accordion
            _accordion.Panes.Clear();
            foreach (BlogCategory category in categoryDAL.ReadBlogCategory(topicDAL.GetBlogTopicId(blogTopic)))
            {
                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + category.Id;

                LiteralControl header = new LiteralControl(string.Format("<div class = \"MenuHeader\"><b>{0}</b></div>", category.Category));

                pane.HeaderContainer.Controls.Add(header);
                foreach (BlogEntry entry in entryDAL.GetBlogEntries(category.Id))
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.Subject;
                    linkButton.CommandArgument = entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(command);
                    linkButton.CausesValidation = false;
                    linkButton.CssClass = "MenuButton";
                    pane.ContentContainer.Controls.Add(linkButton);
                    pane.ContentContainer.Controls.Add(new LiteralControl("<br>"));
                }
                _accordion.Panes.Add(pane);
            }
            return _accordion;
        }

    }
}