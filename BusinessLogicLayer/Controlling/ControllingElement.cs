using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EbalitWebForms.BusinessLogicLayer.Controlling
{
    public abstract class ControllingElement : HierarchicalDataSourceView, IHierarchyData
    {
        #region ----Fields----

        protected HierarchicalList<PSPElement> _children;

        protected ControllingElement _parent;

        #endregion

        #region ----Properties----

        public abstract string Name { get; set; }

        public abstract string Code { get; set; }

        public string Display
        {
            get
            {
                return this.Path + " " + this.Name;
            }
        }

        public bool HasChildren
        {
            get { return _children.Count > 0; }
        }

        public object Item
        {
            get { return this; }
        }

        public string Path
        {
            get
            {
                if (_parent == null)
                {
                    return this.Code;
                }
                else
                {
                    return _parent.Path + "." + this.Code;
                }
            }
        }

        public string Type
        {
            get { return this.ToString(); }
        }

        #endregion

        #region ----Methods----
        /// <summary>
        /// Adds a pspelement to the current element
        /// Automatically sets the code.
        /// </summary>
        /// <param name="element"></param>
        public void Add(PSPElement element)
        {
            element._parent = this;
            if (element._parent.GetChildren().Cast<ControllingElement>().Count() == 0)
            {
                element.Code = "1";
            }
            else
            {
                int maxCode = (from ControllingElement cc in element._parent.GetChildren()
                               select Convert.ToInt32(cc.Code)).Max();
                element.Code = (maxCode + 1).ToString();
            }
            _children.Add(element);
        }

        public void Remove(PSPElement PSPElement)
        {
            throw new System.NotImplementedException();
        }

        public IHierarchicalEnumerable GetChildren()
        {
            return _children;
        }

        public IHierarchyData GetParent()
        {
            return _parent;
        }

        public override IHierarchicalEnumerable Select()
        {
            return new HierarchicalList<ControllingElement>() { this };
        }

        #endregion


    }
}