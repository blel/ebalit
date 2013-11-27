using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.Common
{
    public class HierarchyTaskData : IHierarchyData
    {
        private readonly ProjectTask _task;

        private readonly bool _hasChildren;

        private readonly string _path;

        private readonly object _item;

        private readonly string _type;

        public HierarchyTaskData(ProjectTask task)
        {
            _task = task;
            _hasChildren = GetChildrenAsList().Count > 0;
            _item = task;
            _type = "Task";
            _path = task.TfsTaskId;
        }

        /// <summary>
        /// todo: put this logic in bll
        /// </summary>
        /// <returns></returns>
        private IList<ProjectTask> GetChildrenAsList()
        {
            IList<ProjectTask> childrenTasks;

            using (var context = new Ebalit_WebFormsEntities())
            {
                childrenTasks = (from cc in context.ProjectTasks
                                 where cc.ParentTfsTaskId == _task.TfsTaskId && !cc.IsDeleted 
                                 select cc).ToList();
            }
            return childrenTasks;
        }


        public IHierarchicalEnumerable GetChildren()
        {
            return new HierarchicalTaskEnumerable(GetChildrenAsList(), _task.Name);
        }

        public IHierarchyData GetParent()
        {
            ProjectTask parent;

            //Todo: put this logic in bll
            using (var context = new Ebalit_WebFormsEntities())
            {
                parent = context.ProjectTasks.SingleOrDefault(cc => cc.TfsTaskId == _task.ParentTfsTaskId);
            }
            if (parent != null && string.IsNullOrWhiteSpace(parent.TfsTaskId))
            {
                return null;
            }
            return new HierarchyTaskData(parent);
        }

        public bool HasChildren
        {
            get { return _hasChildren; }
        }
        public string Path { get { return _path; } }

        public object Item
        {
            get
            {
                return
                    _item;
            }
        }

        public string Type { get { return _type; } }
    }
}