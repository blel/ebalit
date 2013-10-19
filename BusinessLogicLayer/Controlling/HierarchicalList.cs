using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EbalitWebForms.BusinessLogicLayer.Controlling
{
    /// <summary>
    /// The treeview control needs the data as an Implementation of the IHierarchicalEnumerable interface
    /// Basetype is a normal list, the returned item is casted to IHierarchyData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HierarchicalList<T> : List<T>, IHierarchicalEnumerable where T : class
    {
        public HierarchicalList() : base() { }

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return enumeratedItem as IHierarchyData;
        }
    }
}