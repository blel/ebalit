using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EbalitWebForms.BusinessLogicLayer.Controlling
{
    /// <summary>
    /// PSPElement cannot be root but can be aggregated elements or leaves
    /// </summary>
    public class PSPElement : ControllingElement
    {
        #region ----Fields----

        private string _code;

        private string _name;

        #endregion

        #region ----Properties----

        public override string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        #endregion

        #region ----Constructors----

        public PSPElement(string name)
        {
            _name = name;
            _children = new HierarchicalList<PSPElement>();
        }

        #endregion

        #region ----Methods----

        #endregion
    }
}