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

    /// <summary>
    /// The archive user control displays an accordion. Each header displays the month&year, and 
    /// the content shows a list of links belonging to that year.
    /// In order to make this work
    /// </summary>
    public partial class Archive : System.Web.UI.UserControl
    {
        private Type _itemsBusinessLayerObject;
        private Type _itemType;
        private MethodInfo _selectMethod;

        /// <summary>
        /// Biz Layer Object, which contains the Select method
        /// </summary>
        public string ItemsBusinessLayerObject
        {
            get { return _itemsBusinessLayerObject.ToString(); }
            set { _itemsBusinessLayerObject = Type.GetType(value); }
        }

        /// <summary>
        /// Type which is returned by the select method
        /// The ItemType must contain a property Id.
        /// </summary>
        public string ItemType
        {
            get { return _itemType.ToString(); }
            set { _itemType = Type.GetType(value); }
        }

        /// <summary>
        /// Name of the select method
        /// The select method must return a IEnumerable<IGrouping<DateTime, ItemType>> object
        /// (such objects are returned by the Linq GroupBy function.)
        /// </summary>
        public string SelectMethod
        {
            get { return _selectMethod.ToString(); }
            set
            {
                Type[] parameters = new Type[0];
                _selectMethod = _itemsBusinessLayerObject.GetMethod(value, parameters);
            }
        }

        /// <summary>
        /// Event executed when the link button is pressed
        /// </summary>
        public event CommandEventHandler LinkButtonPressed;

        /// <summary>
        /// Field to display as links. This must be a property of the ItemType.
        /// </summary>
        public string DisplayField { get; set; }

        /// <summary>
        /// When the link Button is pressed, this event is fired.
        /// The method just calls the LinkButtonPressed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkButton_Command(object sender, CommandEventArgs e)
        {
            if (LinkButtonPressed != null)
                LinkButtonPressed(this, e);
        }


        /// <summary>
        /// In the page load, the accordion is created. Content to display is retrieved by using reflection and dynamic programming
        /// from the properties that the user setup declaratively in the markup.
        /// TODO: some selftest would be good (are the properties set correctly...)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            var genericList = typeof(Dictionary<,>);

            var groupingType = genericList.MakeGenericType(typeof(DateTime), _itemType);

            var genericGroupings = typeof(List<>);

            var groupings = genericGroupings.MakeGenericType(groupingType);

            dynamic result = Activator.CreateInstance(groupings);

            result = _selectMethod.Invoke(Activator.CreateInstance(_itemsBusinessLayerObject), null);

            int idCounter = 0;
            Accordion.Panes.Clear();
            foreach (dynamic key in result)
            {

                AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                pane.ID = "pane" + idCounter;
                idCounter += 1;

                LiteralControl header = new LiteralControl(string.Format("<div class = \"MenuHeader\" id=\"{0}\"><b>{1:MMMM yyyy}</b></div>", "header" + idCounter,
                    key.GetType().GetProperty("Key").GetValue(key)));

                pane.HeaderContainer.Controls.Add(header);

                pane.ContentContainer.Controls.Add(new LiteralControl("<ul style='margin-top:0; margin-bottom:0'>"));
                foreach (dynamic entry in key)
                {
                    LinkButton linkButton = new LinkButton();
                    linkButton.ID = "linkButton" + entry.Id;
                    linkButton.Text = entry.GetType().GetProperty(DisplayField).GetValue(entry);
                    linkButton.CommandArgument = entry.Id.ToString();
                    linkButton.Command += new CommandEventHandler(linkButton_Command);
                    linkButton.CausesValidation = false;
                    //linkButton.CssClass = "MenuButton";
                    pane.ContentContainer.Controls.Add(new LiteralControl("<li>"));
                    pane.ContentContainer.Controls.Add(linkButton);
                    pane.ContentContainer.Controls.Add(new LiteralControl("</li>"));
                }
                pane.ContentContainer.Controls.Add(new LiteralControl("</ul>"));
                Accordion.Panes.Add(pane);
            }
        }
    }



}