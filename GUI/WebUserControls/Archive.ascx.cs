using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.BusinessLogicLayer;
using System.Reflection;
using System.Collections;




namespace EbalitWebForms.GUI.WebUserControls
{
    
    
    public partial class Archive : System.Web.UI.UserControl
    {
        private Type _itemsBusinessLayerObject;
        private Type _itemType;
        private MethodInfo _selectMethod;

        public string ItemsBusinessLayerObject
        {
            get { return _itemsBusinessLayerObject.ToString(); }
            set { _itemsBusinessLayerObject = Type.GetType(value); }
        }

        public string ItemType
        {
            get { return _itemType.ToString(); }
            set { _itemType = Type.GetType(value); }
        }

        public string SelectMethod
        {
            get { return _selectMethod.ToString(); }
            set
            {
                Type[] parameters = new Type[0];
                _selectMethod = _itemsBusinessLayerObject.GetMethod(value, parameters);
            }
        }

        public event CommandEventHandler LinkButtonPressed;
        public string DateFieldName { get; set; }
        public string DisplayField { get; set; }


        protected void Page_Init(object sender, EventArgs e)
        {


        }

       void linkButton_Command(object sender, CommandEventArgs e)
        {
            if (LinkButtonPressed != null)
                LinkButtonPressed(this,e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //var genericList = typeof(List<>);

            //var returnTypeList = genericList.MakeGenericType(_itemType);

            

            IEnumerable<IGrouping<DateTime, EbalitWebForms.DataLayer.BlogEntry>> result =
                (IEnumerable<IGrouping<DateTime, EbalitWebForms.DataLayer.BlogEntry>>)_selectMethod.Invoke(Activator.CreateInstance(_itemsBusinessLayerObject), null);


            int idCounter = 0;
            Accordion.Panes.Clear();
            foreach (IGrouping<DateTime, EbalitWebForms.DataLayer.BlogEntry> key in result)
            {

                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + idCounter;
                idCounter += 1;

                LiteralControl header = new LiteralControl(string.Format("<div class = \"MenuHeader\" id=\"{0}\"><b>{1:MMMM yyyy}</b></div>","header"+idCounter, key.Key));

                pane.HeaderContainer.Controls.Add(header);
                foreach (EbalitWebForms.DataLayer.BlogEntry entry in key)
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.Subject;
                    linkButton.CommandArgument = entry.BlogCategory.BlogTopic.PageName + "?BlogEntryID=" + entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(linkButton_Command);
                    linkButton.CausesValidation = false;
                    linkButton.CssClass = "MenuButton";
                    pane.ContentContainer.Controls.Add(linkButton);
                    pane.ContentContainer.Controls.Add(new LiteralControl("<br>"));
                }
                Accordion.Panes.Add(pane);
            }
        }
    }



}