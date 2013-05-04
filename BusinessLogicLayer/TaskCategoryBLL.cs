using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskCategoryBLL:DataAccessLayer
    {
        public IList<TaskCategory> GetTaskCategories()
        {
            return (from cc in base.EbalitDBContext.TaskCategories
                    select cc).ToList();
        }

        public int Create(TaskCategory taskCategory)
        {
            base.EbalitDBContext.TaskCategories.Add(taskCategory);
            base.EbalitDBContext.SaveChanges();
            return taskCategory.Id;
        }

        public void Update(TaskCategory taskCategory)
        {
            var originalRecord = base.EbalitDBContext.TaskCategories.Find(taskCategory.Id);
            if (originalRecord != null)
            {
                base.EbalitDBContext.Entry(originalRecord).CurrentValues.SetValues(taskCategory);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public void Delete(TaskCategory taskCategory)
        {
            var originalRecord = base.EbalitDBContext.TaskCategories.Find(taskCategory.Id);
            if (originalRecord != null)
            {
                base.EbalitDBContext.TaskCategories.Remove(originalRecord);
                base.EbalitDBContext.SaveChanges();
            }
        }


        

    }
}