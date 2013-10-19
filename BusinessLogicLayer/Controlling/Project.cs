using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EbalitWebForms.BusinessLogicLayer.Controlling
{
    /// <summary>
    /// The root of a PSP is always a project (parent is null)
    /// 
    /// </summary>
    public class Project : ControllingElement
    {
        #region ----Fields.----

        private string _code;

        private string _name;

        #endregion

        #region ----Properties----

        /// <summary>
        /// Project Code
        /// </summary>
        public override string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Name of the project
        /// </summary>
        public override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// State of the project
        /// TODO: not implmemented yet
        /// </summary>
        public ProjectState ProjectState
        {
            get { throw new System.NotImplementedException(); }
            set { }
        }

        #endregion

        #region ----Constructors----

        /// <summary>
        /// When creating a project, the parent is automatically set to null
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        public Project(string name, string code)
        {
            _name = name;
            _code = code;
            _children = new HierarchicalList<PSPElement>();
            _parent = null;
        }

        #endregion

        #region ----Methods----

        #endregion


    }
}